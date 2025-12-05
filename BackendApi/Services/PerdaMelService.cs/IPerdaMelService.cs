using BackendApi.Models;

namespace BackendApi.Services.PerdaMelService
{
    public interface IPerdaMelService
    {
        Task<Response<string>> RegistrarPerdaDeMel(PerdaMelRegisterDTO dto, int userId);
        Task<List<PerdaMel>> BuscarPerdasDeMelDoUsuario(int userId);
    }
}
