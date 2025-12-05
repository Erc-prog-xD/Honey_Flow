using Microsoft.AspNetCore.Mvc;
using BackendApi.Dto;
using BackendApi.Services.ColmeiaService;
using Microsoft.AspNetCore.Authorization;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColmeiaController : ControllerBase
    {
        private readonly IColmeiaService _colmeiaInterface;
        public ColmeiaController(IColmeiaService colmeiaInterface)
        {
            _colmeiaInterface = colmeiaInterface;
        }

        [HttpPost("CriarColmeia")]
        public async Task<ActionResult> CriarColmeia(ColmeiaCreateDTO dto, int apiarioId)
        {
            var response = await _colmeiaInterface.CriarColmeia(dto, apiarioId);
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