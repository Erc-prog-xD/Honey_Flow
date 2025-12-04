using BackendApi.Data;
using BackendApi.Dto;
using BackendApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendApi.Services.ApiarioService
{
    public class ApiarioService : IApiarioService
    {
        private readonly AppDbContext _context;

        public ApiarioService(AppDbContext context)
        {
            _context = context;
        }

        // ----------------------------
        // Criar Apiário
        // ----------------------------
       public async Task<Response<string>> CriarApiario(ApiarioCreateDTO dto, int userId)
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

                var apiario = new Apiario
                {
                    User = user,
                    Localizacao = dto.Localizacao,
                    Coord_X = dto.Coord_X,
                    Coord_Y = dto.Coord_Y,
                    Bioma = dto.Bioma,
                    TipoDeAbelha = dto.TipoDeAbelha,
                    Atividade = dto.Atividade,
                    CreationDate = DateTime.Now
                };

                _context.Apiarios.Add(apiario);
                await _context.SaveChangesAsync();

                response.Dados = null;
                response.Mensage = "Operação realizada com sucesso.";
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Mensage = "Erro ao criar apiário: " + ex.Message;
            }

            return response;
        }


        // ----------------------------
        // Buscar Apiários do Usuário
        // ----------------------------
        public async Task<List<Apiario>> BuscarApiariosDoUsuario(int userId)
        {
            return await _context.Apiarios
                .Where(a => a.User.Id == userId && a.DeletionDate == null)
                .ToListAsync();
        }
    }
}
