using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BackendApi.Dto.MovimentacaoDTO;

namespace BackendApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/apiarios/{apiarioId}/movimentacoes")]
    public class MovimentacaoController : ControllerBase
    {
        private readonly IMovimentacaoService _movimentacaoService;

        public MovimentacaoController(IMovimentacaoService movimentacaoService)
        {
            _movimentacaoService = movimentacaoService;
        }

        [HttpPost]
        public async Task<ActionResult> CriarMovimentacao(int apiarioId, [FromBody] MovimentacaoCreateDTO dto)
        {
            var response = await _movimentacaoService.CriarMovimentacao(apiarioId, dto);
            
            return Ok(response);
        }
    }
}
