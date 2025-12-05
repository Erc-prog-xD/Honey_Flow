using BackendApi.Data;
using System;
using BackendApi.Dto;
using BackendApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendApi.Services.ProducaoService
{
    public class ProducaoService : IProducaoService
    {
        private readonly AppDbContext _context;

        public ProducaoService(AppDbContext context)
        {
            _context = context;
        }

       public async Task<Response<string>> RegistrarProducaoDeMel(ProducaoRegisterDTO dto, int apiarioId)
        {
            var response = new Response<string>();

            try
            {
                var apiario = await _context.Apiario.FirstOrDefaultAsync(u => u.Id == apiarioId);

                if (apiario == null)
                {
                    response.Status = false;
                    response.Mensage = "Apiário não encontrado.";
                    return response;
                }

                var producao = new Producao
                {
                    Apiario = apiario,
                    User = dto.User,
                    Data = dto.Data,
                    VolumeTotal = dto.VolumeTotal,
                    CreationDate = DateTime.Now
                };

                _context.Producoes.Add(producao);
                await _context.SaveChangesAsync();

                response.Dados = null;
                response.Mensage = "Operação realizada com sucesso.";
                response.Status = true;
            }

            catch (Exception ex)
            {
                response.Status = false;
                response.Mensage = "Erro ao registrar essa produção de mel: " + ex.Message;
            }

            return response;
        }

        public async Task<List<Producao>> BuscarProducoesDoApiario(int apiarioId)
        {
            return await _context.Producoes
                .Where(a => a.Apiario.Id == apiarioId && a.DeletionDate == null)
                .ToListAsync();
        }
    }
}
