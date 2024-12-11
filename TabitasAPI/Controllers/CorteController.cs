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
    public class CorteController : ControllerBase
    {
        private ICorteServices<CorteDTO, CorteInsertDTO, CorteUpdateDTO> _CorteServices;
        public CorteController(
         [FromKeyedServices("corteServices")] ICorteServices<CorteDTO, CorteInsertDTO, CorteUpdateDTO> corteServices)
        {
            _CorteServices = corteServices;

        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetCorte()
        {
            var corteDTOs = await _CorteServices.GetCorte();
            return Ok(corteDTOs);
        }
        [AllowAnonymous]
        [HttpGet("GetCorteByModelo/{Modelo}")]
        public async Task<ActionResult<CorteDTO>> GetCorteByModelo(string Modelo)
        {
            var corteDto = await _CorteServices.GetCorteByNombre(Modelo);
            return corteDto == null ? NotFound() : Ok(corteDto);
        }
        [AllowAnonymous]
        [HttpGet("{Idcorte}")]
        public async Task<ActionResult<CorteDTO>> GetCorteById(int Idcorte)
        {
            var corteDto = await _CorteServices.GetCorteById(Idcorte);
            return corteDto == null ? NotFound() : corteDto;
        }
        //[Authorize(Roles = "Corte")]
        [HttpPost]
        public async Task<ActionResult<CorteDTO>> AddModelo([FromForm] CorteInsertDTO corteInsertDto)
        {
            try
            {
                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}" +
                              $"{HttpContext.Request.PathBase.Value}";

                var corteDto = await _CorteServices.AddCorte(corteInsertDto, baseUrl);
                return CreatedAtAction(nameof(GetCorteById), new { IdCorte = corteDto.IdCorte }, corteDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        //[Authorize(Roles = "Corte")]
        [HttpPut("{Idcorte}")]
        public async Task<ActionResult<Corte>> UpdateCorte(int Idcorte, CorteUpdateDTO corteUpdateDto)
        {
            try
            {
                var corteDto = await _CorteServices.UpdateCorte(Idcorte, corteUpdateDto);
                return corteDto == null ? NotFound() : Ok(corteDto);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

