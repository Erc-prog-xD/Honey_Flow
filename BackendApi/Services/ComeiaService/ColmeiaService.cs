using BackendApi.Data;
using BackendApi.Models;
using Microsoft.EntityFrameworkCore;
using BackendApi.Dto.ColmeiaDTO;

namespace BackendApi.Services.ColmeiaService
{
    public class ColmeiaService : IColmeiaService
    {
        private readonly AppDbContext _context;

        public ColmeiaService(AppDbContext context)
        {
            _context = context;
        }

       public async Task<Response<string>> CriarColmeia(ColmeiaCreateDTO dto)
        {
            var response = new Response<string>();

            try
            {
                var apiario = await _context.Apiarios.FirstOrDefaultAsync(u => u.Id == dto.ApiarioId);

                if (apiario == null)
                {
                    response.Status = false;
                    response.Mensage = "Apiario não encontrado.";
                    return response;
                }

                var colmeia = new Colmeia
                {
                    Apiario = apiario,
                    AnoColmeia = dto.AnoColmeia,
                    AnoRainha = dto.AnoRainha,
                    Status = dto.Status
                };

                _context.Colmeias.Add(colmeia);
                await _context.SaveChangesAsync();

                response.Dados = null;
                response.Mensage = "Colmeia criada e vinculada ao apiario!";
                response.Status = true;
            }

            catch (Exception ex)
            {
                response.Status = false;
                response.Mensage = "Erro ao criar colmeia: " + ex.Message;
            }

            return response;
        }

        public async Task<Response<List<ColmeiaResponseDTO>>> BuscarColmeiasDoApiario(int apiarioId)
        {
            var response = new Response<List<ColmeiaResponseDTO>>();
            
            try
            {
                // 1. Inclua o Apiario para evitar NullReferenceException se for acessado no DTO
                var colmeias = await _context.Colmeias
                    .Include(c => c.Apiario) // BOA PRÁTICA: Garante que Apiario está carregado
                    .Where(c => c.Apiario.Id == apiarioId && c.DeletionDate == null)
                    .ToListAsync();
                
                if (colmeias == null || colmeias.Count == 0)
                {
                    response.Status = false;
                    response.Mensage = "Nenhuma colmeia encontrada para este Apiário."; 
                    response.Dados = new List<ColmeiaResponseDTO>(); 
                }
                else
                {
                    // 🚨 SOLUÇÃO: Mapeamento de Entidade (Colmeia) para DTO (ColmeiaResponseDTO)
                    var colmeiaDTOs = colmeias.Select(c => new ColmeiaResponseDTO
                    {
                        Id = c.Id,
                        ApiarioId = c.Apiario.Id, // Assumindo que a entidade Colmeia tem Apiario (objeto)
                        AnoColmeia = c.AnoColmeia,
                        AnoRainha = c.AnoRainha,
                        Status = c.Status,
                        CreationDate = c.CreationDate,
                        DeletionDate = c.DeletionDate ?? null
                    }).ToList();
                    
                    response.Status = true;
                    response.Mensage = "Colmeias buscadas com sucesso.";
                    response.Dados = colmeiaDTOs; // Atribui a lista de DTOs
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensage = "Erro ao buscar colmeias: " + ex.Message;
                response.Dados = null; 
            }
            return response;
        }
    }
}