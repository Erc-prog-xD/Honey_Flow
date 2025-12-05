using BackendApi.Data;
using BackendApi.Dto;
using BackendApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendApi.Services.ColmeiaService
{
    public class ColmeiaService : IColmeiaService 
    {
        private readonly AppDbContext _context;

        public ColmeiaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Response<string>> CriarColmeia(ColmeiaCreateDTO dto, int apiarioId)
        {
            var response = new Response<string>();

            try 
            {
                var apiario = await _context.Apiarios.FirstOrDefaultAsync(u => u.Id == apiarioId);

                if(apiario == null){
                    response.Status = false;
                    response.Mensage = "404 Not Found";
                    return response;
                }

                var Colmeia = new Colmeia {
                    Apiario = apiario,
                    AnoColmeia = dto.AnoColmeia,
                    AnoRainha = dto.AnoRainha,
                    Atividade = dto.Atividade,
                    CreationDate = DateTime.Now
                };

                _context.Colmeias.Add(Colmeia);
                await _context.SaveChangesAsync();

                response.Dados = Colmeia.Id.ToString();;
                response.Mensage = "201 Created";
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensage = "500 Internal Server Error: " + ex.Message;
            }

            return response;
        }

        public async Task<List<Colmeia>> BuscarColmeiasDoApiario(int apiarioId)
        {
            return await _context.Colmeias
                .Where(a => a.Apiario.Id == apiarioId && a.DeletionDate == null)
                .ToListAsync();
        }
    }
}