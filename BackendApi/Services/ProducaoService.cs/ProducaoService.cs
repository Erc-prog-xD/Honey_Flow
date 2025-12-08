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

       public async Task<Response<string>> RegistrarProducaoDeMel(ProducaoRegisterDTO dto, int apiarioId, int userId)
        {
            var response = new Response<string>();

            try
            {
                var apiarioExiste = await _context.Apiarios.AnyAsync(u => u.Id == apiarioId);
                var userExiste = await _context.Users.AnyAsync(u => u.Id == userId);

                if (!apiarioExiste){
                    response.Status = false;
                    response.Mensage = "Apiário não encontrado.";
                    return response;
                }

                if (!userExiste){
                    response.Status = false;
                    response.Mensage = "Usuário não encontrado.";
                    return response;
                }

                if (dto.VolumeTotal < 0){
                    response.Status = false;
                    response.Mensage = "O volume inválido.";
                    return response;
                }

                var producao = new Producao
                {
                    ApiarioId = apiarioId,
                    UserId = userId,
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

        public async Task<List<Producao>> BuscarProducoesDoApiario(int apiarioId){
            return await _context.Producoes
                .Where(a => a.Apiario.Id == apiarioId && a.DeletionDate == null)
                .ToListAsync();
        }
    }
}
