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
    public class LavadoController : ControllerBase
    {
        private ILavadoServices<LavadoDTO, LavadoInsertDTO, LavadoUpdateDTO> _LavadoServices;
        public LavadoController(
         [FromKeyedServices("lavadoServices")] ILavadoServices<LavadoDTO, LavadoInsertDTO, LavadoUpdateDTO> lavadoServices)
        {
            _LavadoServices = lavadoServices;

        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetLavado()
        {
            var lavadoDTOs = await _LavadoServices.GetLavado();
            return Ok(lavadoDTOs);
        }
        [AllowAnonymous]
        [HttpGet("GetLavadoByModelo/{Modelo}")]
        public async Task<ActionResult<LavadoDTO>> GetLavadoByModelo(string Modelo)
        {
            var lavadoDto = await _LavadoServices.GetLavadoByNombre(Modelo);
            return lavadoDto == null ? NotFound() : Ok(lavadoDto);
        }
        [AllowAnonymous]
        [HttpGet("{Idlavado}")]
        public async Task<ActionResult<LavadoDTO>> GetLavadoById(int Idlavado)
        {
            var lavadoDto = await _LavadoServices.GetLavadoById(Idlavado);
            return lavadoDto == null ? NotFound() : lavadoDto;
        }
        //[Authorize(Roles = "Lavado")]
        [HttpPost]
        public async Task<ActionResult<LavadoDTO>> AddModelo([FromForm] LavadoInsertDTO lavadoInsertDto)
        {
            try {
                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}" +
                              $"{HttpContext.Request.PathBase.Value}";

                var lavadoDto = await _LavadoServices.AddLavado(lavadoInsertDto, baseUrl);
                return CreatedAtAction(nameof(GetLavadoById), new { Idlavado = lavadoDto.IdLavado }, lavadoDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //[Authorize(Roles = "Lavado")]
        [HttpPut("{Idlavado}")]
        public async Task<ActionResult<Lavado>> UpdateLavado(int Idlavado, LavadoUpdateDTO lavadoUpdateDto)
        { try
            {
                var lavadoDto = await _LavadoServices.UpdateLavado(Idlavado, lavadoUpdateDto);
                return lavadoDto == null ? NotFound() : Ok(lavadoDto);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

