using Microsoft.AspNetCore.Mvc;
using BackendApi.Dto;
using BackendApi.Services.VendaMelService;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VendaMelController : ControllerBase
    {
        private readonly IVendaMelService _vendaMelInterface;
        public VendaMelController(IVendaMelService vendaMelInterface)
        {
            _vendaMelInterface = vendaMelInterface;
        }

        [HttpPost("RegistrarVendaDeMel")]
        public async Task<ActionResult> RegistrarVendaDeMel([FromBody] VendaMelRegisterDTO dto){
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            
            int userId = 0;
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out userId)){
                return Unauthorized("Usuário inválido.");
            }

            var response = await _vendaMelInterface.RegistrarVendaDeMel(dto, userId);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpGet("BuscarVendasDeMelDoUsuario")]
        public async Task<ActionResult> BuscarVendasDeMelDoUsuario()
        {
            int userId = 0;
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out userId)){
                return Unauthorized("Usuário inválido.");
            }

            var response = await _vendaMelInterface.BuscarVendasDeMelDoUsuario(userId);
            return response.Status ? Ok(response) : BadRequest(response);
        }

    }
}