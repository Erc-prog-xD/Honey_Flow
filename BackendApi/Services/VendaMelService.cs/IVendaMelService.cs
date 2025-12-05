using BackendApi.Models;

namespace BackendApi.Services.VendaMelService
{
    public interface IVendaMelService
    {
        Task<Response<string>> RegistrarVendaDeMel(VendaMelRegisterDTO dto, int userId);
        Task<List<VendaMel>> BuscarVendasDeMelDoUsuario(int userId);
    }
}
