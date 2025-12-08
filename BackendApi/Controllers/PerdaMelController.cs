using Microsoft.AspNetCore.Mvc;
using BackendApi.Dto;
using BackendApi.Services.PerdaMelService;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PerdaMelController : ControllerBase
    {
        private readonly IPerdaMelService _perdaMelInterface;
        public PerdaMelController(IPerdaMelService perdaMelInterface)
        {
            _perdaMelInterface = perdaMelInterface;
        }

        [HttpPost("RegistrarPerdaDeMel")]
        public async Task<ActionResult> RegistrarPerdaDeMel([FromBody] PerdaMelRegisterDTO dto){
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            int userId = 0;
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out userId)){
                return Unauthorized("Usuário inválido.");
            }

            var response = await _perdaMelInterface.RegistrarPerdaDeMel(dto, userId);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpGet("BuscarPerdasDeMelDoUsuario")]
        public async Task<ActionResult> BuscarPerdasDeMelDoUsuario(){
            int userId = 0;
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out userId)){
                return Unauthorized("Usuário inválido.");
            }

            var response = await _perdaMelInterface.BuscarPerdasDeMelDoUsuario(userId);
            return response.Status ? Ok(response) : BadRequest(response);
        }

    }
}