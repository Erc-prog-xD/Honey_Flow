using Microsoft.AspNetCore.Mvc;
using BackendApi.Dto;
using BackendApi.Services.VendaMelService;
using Microsoft.AspNetCore.Authorization;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendaMelController : ControllerBase
    {
        private readonly IVendaMelService _vendaMelInterface;
        public VendaMelController(IVendaMelService vendaMelInterface)
        {
            _vendaMelInterface = vendaMelInterface;
        }

        [HttpPost("RegistrarVendaDeMel")]
        public async Task<ActionResult> RegistrarVendaDeMel(VendaMelRegisterDTO dto, int userId)
        {
            var response = await _vendaMelInterface.RegistrarVendaDeMel(dto, userId);
            return Ok(response);
        }

        [HttpGet("BuscarVendasDeMelDoUsuario")]
        public async Task<ActionResult> BuscarVendasDeMelDoUsuario(int userId)
        {
            var response = await _vendaMelInterface.BuscarVendasDeMelDoUsuario(userId);
            return Ok(response);
        }

    }
}