using BackendApi.Dto.MovimentacaoDTO;
using BackendApi.Models;

public interface IMovimentacaoService
{
    Task<Response<string>> CriarMovimentacao(int apiarioId, MovimentacaoCreateDTO dto);
}
