using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BackendApi.Services.ColmeiaService;

namespace BackendApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]

    public class ColmeiaController : ControllerBase
    {
        private readonly IColmeiaService _colmeiaService;

        public ColmeiaController(IColmeiaService colmeiaService)
        {
            _colmeiaService = colmeiaService;
        }

        [HttpPost("CriarColmeia")]
        public async Task<IActionResult> CriarColmeia([FromBody] ColmeiaCreateDTO dto)
        {
            int apiarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var response = await _colmeiaService.CriarColmeia(dto, apiarioId);
            return Ok(response);
        }

        [HttpGet("BuscarColmeias")]
        public async Task<IActionResult> BuscarColmeias()
        {
            int apiarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var response = await _colmeiaService.BuscarColmeiasDoApiario(apiarioId);
            return Ok(response);
        }

    }
}