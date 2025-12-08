using Microsoft.AspNetCore.Mvc;
using BackendApi.Dto;
using BackendApi.Services.ProducaoService;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BackendApi.Controllers
{
    [Route("api/apiario/{apiarioId}/[controller]")]
    [ApiController]
    [Authorize]
    public class ProducaoController : ControllerBase
    {
        private readonly IProducaoService _producaoInterface;
        public ProducaoController(IProducaoService producaoInterface)
        {
            _producaoInterface = producaoInterface;
        }

        [HttpPost("RegistrarProducaoDeMel")]
        public async Task<ActionResult> RegistrarProducaoDeMel([FromRoute] int apiarioId, [FromBody] ProducaoRegisterDTO dto){            
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            int userId = 0;
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out userId)){
                return Unauthorized("Usuário inválido.");
            }

            var response = await _producaoInterface.RegistrarProducaoDeMel(dto, apiarioId, userId);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpGet("BuscarProducoesDoApiario")]
        public async Task<ActionResult> BuscarProducoesDoApiario([FromRoute] int apiarioId){
            if (!ModelState.IsValid) {
                return BadRequest(ModelState); 
            }

            var response = await _producaoInterface.BuscarProducoesDoApiario(apiarioId);
            return response.Status ? Ok(response) : BadRequest(response);
        }

    }
}