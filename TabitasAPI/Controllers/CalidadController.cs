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
    public class CalidadController : ControllerBase
    {
        private ICalidadServices<CalidadDTO, CalidadInsertDTO, CalidadUpdateDTO> _CalidadServices;
        public CalidadController(
         [FromKeyedServices("calidadServices")] ICalidadServices<CalidadDTO, CalidadInsertDTO, CalidadUpdateDTO> calidadServices)
        {
            _CalidadServices = calidadServices;

        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetCalidad()
        {
            var corteDTOs = await _CalidadServices.GetCalidad();
            return Ok(corteDTOs);
        }
        [AllowAnonymous]
        [HttpGet("GetCalidadByModelo/{Modelo}")]
        public async Task<ActionResult<CalidadDTO>> GetCalidadByModelo(string Modelo)
        {
            var calidadDto = await _CalidadServices.GetCalidadByNombre(Modelo);
            return calidadDto == null ? NotFound() : Ok(calidadDto);
        }
        [AllowAnonymous]
        [HttpGet("{Idcalidad}")]
        public async Task<ActionResult<CalidadDTO>> GetCalidadById(int Idcalidad)
        {
            var calidadDto = await _CalidadServices.GetCalidadById(Idcalidad);
            return calidadDto == null ? NotFound() : calidadDto;
        }
        //[Authorize(Roles = "Calidad")]
        [HttpPost]
        public async Task<ActionResult<CalidadDTO>> AddModelo([FromForm] CalidadInsertDTO calidadInsertDto)
        {
            try
            {
                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}" +
                              $"{HttpContext.Request.PathBase.Value}";

                var calidadDto = await _CalidadServices.AddCalidad(calidadInsertDto, baseUrl);
                return CreatedAtAction(nameof(GetCalidadById), new { IdCalidad = calidadDto.IdCalidad }, calidadDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        //[Authorize(Roles = "Calidad")]
        [HttpPut("{Idcalidad}")]
        public async Task<ActionResult<Calidad>> UpdateCalidad(int Idcalidad, CalidadUpdateDTO calidadUpdateDto)
        {
            try
            {
                var calidadDto = await _CalidadServices.UpdateCalidad(Idcalidad, calidadUpdateDto);
                return calidadDto == null ? NotFound() : Ok(calidadDto);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

