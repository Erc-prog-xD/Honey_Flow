using BackendApi.Data;
using System;
using BackendApi.Dto;
using BackendApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendApi.Services.PerdaMelService
{
    public class PerdaMelService : IPerdaMelService
    {
        private readonly AppDbContext _context;

        public PerdaMelService(AppDbContext context)
        {
            _context = context;
        }

       public async Task<Response<string>> RegistrarPerdaDeMel(PerdaMelRegisterDTO dto, int userId)
        {
            var response = new Response<string>();

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                {
                    response.Status = false;
                    response.Mensage = "Usuário não encontrado.";
                    return response;
                }

                var perdaMel = new PerdaMel
                {
                    User = user,
                    Volume = dto.Volume,
                    Data = dto.Data,
                    CreationDate = DateTime.Now
                };

                _context.PerdasMel.Add(perdaMel);
                await _context.SaveChangesAsync();

                response.Dados = null;
                response.Mensage = "Operação realizada com sucesso.";
                response.Status = true;
            }

            catch (Exception ex)
            {
                response.Status = false;
                response.Mensage = "Erro ao registrar essa perda de mel: " + ex.Message;
            }

            return response;
        }

        public async Task<List<PerdaMel>> BuscarPerdasDeMelDoUsuario(int userId)
        {
            return await _context.PerdasMel
                .Where(a => a.User.Id == userId && a.DeletionDate == null)
                .ToListAsync();
        }
    }
}
