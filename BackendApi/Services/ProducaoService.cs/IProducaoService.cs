using BackendApi.Models;

namespace BackendApi.Services.ProducaoService
{
    public interface IProducaoService
    {
        Task<Response<string>> RegistrarProducaoDeMel(ProducaoRegisterDTO dto, int apiarioId, int userId);
        Task<List<Procucao>> BuscarProducoesDoApiario(int apiarioId);
    }
}
