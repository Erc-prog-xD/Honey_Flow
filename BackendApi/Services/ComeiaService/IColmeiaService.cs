using BackendApi.Dto.ColmeiaDTO;
using BackendApi.Models;

namespace BackendApi.Services.ColmeiaService
{
    public interface IColmeiaService
    {
        Task<Response<string>> CriarColmeia(ColmeiaCreateDTO dto);
        Task<Response<List<ColmeiaResponseDTO>>> BuscarColmeiasDoApiario(int apiarioId);
    }
}