using BackendApi.Data;
using BackendApi.Dto.MovimentacaoDTO;
using BackendApi.Enums;
using BackendApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendApi.Services.MovimentacaoService
{
    public class MovimentacaoService : IMovimentacaoService
    {
        private readonly AppDbContext _context;

        public MovimentacaoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Response<string>> CriarMovimentacao(int apiarioId, MovimentacaoCreateDTO dto)
        {
            var response = new Response<string>();

            try
            {
                // 1) Validar apiário e dono
                var apiario = await _context.Apiarios
                    .FirstOrDefaultAsync(a => a.Id == apiarioId && a.DeletionDate == null);

                if (apiario == null)
                {
                    response.Status = false;
                    response.Mensage = "Apiário não encontrado.";
                    return response;
                }

                if (dto.QuantidadeKg <= 0)
                {
                    response.Status = false;
                    response.Mensage = "A quantidade (Kg) deve ser maior que zero.";
                    return response;
                }

                // 2) Buscar produção consolidada do apiário
                var producao = await _context.ProducaoApiarios
                    .FirstOrDefaultAsync(p => p.Apiario.Id == apiarioId && p.DeletionDate == null);

                if (producao == null)
                {
                    response.Status = false;
                    response.Mensage = "Produção do apiário não encontrada. Verifique se ela é criada ao criar o apiário.";
                    return response;
                }

                // (Opcional) regra de valor: se for venda, valor não pode ser negativo
                if (dto.Valor < 0)
                {
                    response.Status = false;
                    response.Mensage = "O valor não pode ser negativo.";
                    return response;
                }

                // Saídas: validar estoque
                var isEntrada = dto.Tipo == TipoMovimentoMelEnum.Colheita;
                if (!isEntrada && producao.EstoqueAtualKg < dto.QuantidadeKg)
                {
                    response.Status = false;
                    response.Mensage = $"Estoque insuficiente. Estoque atual: {producao.EstoqueAtualKg} Kg.";
                    return response;
                }

                if(isEntrada && dto.Valor > 0)
                {
                    response.Status = false;
                    response.Mensage = "A colheita não aceita o campo valor pois esta apenas colhendo!";
                    return response;
                }

                var movimento = new MovimentoMel
                {
                    Apiario = apiario,
                    Tipo = dto.Tipo,
                    QuantidadeKg = dto.QuantidadeKg,
                    Valor = dto.Valor,
                    Data = dto.Data,
                    Observacao = dto.Observacao,
                    CreationDate = DateTime.Now
                };

                _context.MovimentacaoMel.Add(movimento);

                       // Atualiza consolidado
                if (dto.Tipo == TipoMovimentoMelEnum.Colheita)
                {
                    producao.TotalProduzidoKg += dto.QuantidadeKg;
                    producao.EstoqueAtualKg += dto.QuantidadeKg;
                }
                else
                {
                    producao.EstoqueAtualKg -= dto.QuantidadeKg;
                }
                await _context.SaveChangesAsync();

                response.Status = true;
                response.Mensage = "Movimentação registrada com sucesso.";
                return response;

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensage = "Erro ao registrar movimentação: " + ex.Message;
                return response;
            }
        }
    }
}
