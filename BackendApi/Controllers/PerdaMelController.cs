using Microsoft.AspNetCore.Mvc;
using BackendApi.Dto;
using BackendApi.Services.PerdaMelService;
using Microsoft.AspNetCore.Authorization;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerdaMelController : ControllerBase
    {
        private readonly IPerdaMelService _perdaMelInterface;
        public PerdaMelController(IPerdaMelService perdaMelInterface)
        {
            _perdaMelInterface = perdaMelInterface;
        }

        [HttpPost("RegistrarPerdaDeMel")]
        public async Task<ActionResult> RegistrarPerdaDeMel(PerdaMelRegisterDTO dto, int userId)
        {
            var response = await _perdaMelInterface.RegistrarPerdaDeMel(dto, userId);
            return Ok(response);
        }

        [HttpGet("BuscarPerdasDeMelDoUsuario")]
        public async Task<ActionResult> BuscarPerdasDeMelDoUsuario(int userId)
        {
            var response = await _perdaMelInterface.BuscarPerdasDeMelDoUsuario(userId);
            return Ok(response);
        }

    }
}