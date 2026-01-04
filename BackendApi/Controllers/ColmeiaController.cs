using Microsoft.AspNetCore.Mvc;
using BackendApi.Services.ColmeiaService;
using Microsoft.AspNetCore.Authorization;
using BackendApi.Dto.ColmeiaDTO;
using System.Security.Claims;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ColmeiaController : ControllerBase
    {
        private readonly IColmeiaService _colmeiaInterface;
        public ColmeiaController(IColmeiaService colmeiaInterface)
        {
            _colmeiaInterface = colmeiaInterface;
        }

        [HttpPost("CriarColmeia")]
        public async Task<ActionResult> CriarColmeia(ColmeiaCreateDTO dto)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var response = await _colmeiaInterface.CriarColmeia(userId ,dto);
            return Ok(response);
        }

        [HttpGet("BuscarColmeiasDoApiario")]
        public async Task<ActionResult> BuscarColmeiasDoApiario(int apiarioId)
        {
            var response = await _colmeiaInterface.BuscarColmeiasDoApiario(apiarioId);
            return Ok(response);
        }

    }
}