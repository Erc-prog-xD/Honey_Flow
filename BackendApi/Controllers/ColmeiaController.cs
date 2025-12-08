using Microsoft.AspNetCore.Mvc;
using BackendApi.Dto;
using BackendApi.Services.ColmeiaService;
using Microsoft.AspNetCore.Authorization;

namespace BackendApi.Controllers
{
    [Route("api/apiario/{apiarioID}/[controller]")]
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
        public async Task<ActionResult> CriarColmeia([FromRoute] int apiarioId, [FromBody] ColmeiaCreateDTO dto)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState); 
            }

            var response = await _colmeiaInterface.CriarColmeia(dto, apiarioId);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpGet("BuscarColmeiasDoApiario")]
        public async Task<ActionResult> BuscarColmeiasDoApiario([FromRoute] int apiarioId)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState); 
            }

            var response = await _colmeiaInterface.BuscarColmeiasDoApiario(apiarioId);
            return response.Status ? Ok(response) : BadRequest(response);
        }

    }
}