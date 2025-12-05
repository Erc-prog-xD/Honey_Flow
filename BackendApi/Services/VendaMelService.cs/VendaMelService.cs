using BackendApi.Data;
using System;
using BackendApi.Dto;
using BackendApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendApi.Services.VendaMelService
{
    public class VendaMelService : IVendaMelService
    {
        private readonly AppDbContext _context;

        public VendaMelService(AppDbContext context)
        {
            _context = context;
        }

       public async Task<Response<string>> RegistrarVendaDeMel(VendaMelRegisterDTO dto, int userId)
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

                var vendaMel = new VendaMel
                {
                    User = user,
                    Data = dto.Data,
                    Volume = dto.Volume,
                    ValorTotal = dto.ValorTotal,
                    CreationDate = DateTime.Now
                };

                _context.VendasMel.Add(vendaMel);
                await _context.SaveChangesAsync();

                response.Dados = null;
                response.Mensage = "Operação realizada com sucesso.";
                response.Status = true;
            }

            catch (Exception ex)
            {
                response.Status = false;
                response.Mensage = "Erro ao registrar essa venda de mel: " + ex.Message;
            }

            return response;
        }

        public async Task<List<VendaMel>> BuscarVendasDeMelDoUsuario(int userId)
        {
            return await _context.VendasMel
                .Where(a => a.User.Id == userId && a.DeletionDate == null)
                .ToListAsync();
        }
    }
}
