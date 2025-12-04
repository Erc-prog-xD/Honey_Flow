using BackendApi.Models;

namespace BackendApi.Services.ApiarioService
{
    public interface IApiarioService
    {
        Task<Response<string>> CriarApiario(ApiarioCreateDTO dto, int userId);
        Task<List<Apiario>> BuscarApiariosDoUsuario(int userId);
        // Aqui você pode adicionar mais métodos (Delete, Update etc.)
    }
}
