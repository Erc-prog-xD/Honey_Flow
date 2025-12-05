using Microsoft.AspNetCore.Mvc;
using BackendApi.Dto;
using BackendApi.Services.ProducaoService;
using Microsoft.AspNetCore.Authorization;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducaoController : ControllerBase
    {
        private readonly IProducaoService _producaoInterface;
        public ProducaoController(IProducaoService producaoInterface)
        {
            _producaoInterface = producaoInterface;
        }

        [HttpPost("RegistrarProducaoDeMel")]
        public async Task<ActionResult> RegistrarProducaoDeMel(ProducaoRegisterDTO dto, int apiarioId)
        {
            var response = await _producaoInterface.RegistrarProducaoDeMel(dto, apiarioId);
            return Ok(response);
        }

        [HttpGet("BuscarProducoesDoApiario")]
        public async Task<ActionResult> BuscarProducoesDoApiario(int apiarioId)
        {
            var response = await _producaoInterface.BuscarProducoesDoApiario(apiarioId);
            return Ok(response);
        }

    }
}