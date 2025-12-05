using BackendApi.Models;

namespace BackendApi.Services.ColmeiaService
{
    public interface IColmeiaService
    {
        Task<Response<string>> CriarColmeia(ColmeiaCreateDTO dto, int userId);
        Task<List<Colmeia>> BuscarColmeiasDoApiario(int apiarioId);
    }
}
