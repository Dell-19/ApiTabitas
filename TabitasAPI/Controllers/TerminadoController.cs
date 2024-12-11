using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TabitasAPI.DTOs;
using TabitasAPI.Models;
using TabitasAPI.Services;
using TabitasAPI.Services.IServices;
using XAct.Security;
using AllowAnonymousAttribute = Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute;

namespace TabitasAPI.Controllers
{
    [ResponseCache(Duration = 30)]
    [Route("api/[controller]")]
    [ApiController]
    public class TerminadoController : ControllerBase
    {
        private ITerminadoServices<TerminadoDTO, TerminadoInsertDTO, TerminadoUpdateDTO> _TerminadoServices;
        public TerminadoController(
         [FromKeyedServices("terminadoServices")] ITerminadoServices<TerminadoDTO, TerminadoInsertDTO, TerminadoUpdateDTO> terminadoServices)
        {
            _TerminadoServices = terminadoServices;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetTerminado()
        {
            var terminadoDTOs = await _TerminadoServices.GetTerminado();
            return Ok(terminadoDTOs);
        }
        [AllowAnonymous]
        [HttpGet("GetTerminadoByModelo/{Modelo}")]
        public async Task<ActionResult<TerminadoDTO>> GetTerminadoByModelo(string Modelo)
        {
            var terminadoDto = await _TerminadoServices.GetTerminadoByNombre(Modelo);
            return terminadoDto == null ? NotFound() : Ok(terminadoDto);
        }
        [AllowAnonymous]
        [HttpGet("{Idterminado}")]
        public async Task<ActionResult<TerminadoDTO>> GetTerminadoById(int Idterminado)
        {
            var terminadoDto = await _TerminadoServices.GetTerminadoById(Idterminado);
            return terminadoDto == null ? NotFound() : terminadoDto;
        }
        //[Authorize(Roles = "Terminado")]
        [HttpPost]
        public async Task<ActionResult<TerminadoDTO>> AddModelo([FromForm] TerminadoInsertDTO terminadoInsertDto)
        {
            try
            {
                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}" +
                          $"{HttpContext.Request.PathBase.Value}";

                var terminadoDto = await _TerminadoServices.AddTerminado(terminadoInsertDto, baseUrl);
                return CreatedAtAction(nameof(GetTerminadoById), new { IdTErminado = terminadoDto.IdTErminado }, terminadoDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //[Authorize(Roles = "Terminado")]
        [HttpPut("{Idterminado}")]
        public async Task<ActionResult<Terminado>> UpdateTerminado(int Idterminado, TerminadoUpdateDTO terminadoUpdateDto)
        { try
            {
                var terminadoDto = await _TerminadoServices.UpdateTerminado(Idterminado, terminadoUpdateDto);
                return terminadoDto == null ? NotFound() : Ok(terminadoDto);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

