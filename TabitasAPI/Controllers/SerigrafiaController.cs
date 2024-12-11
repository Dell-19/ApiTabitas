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
    public class SerigrafiaController : ControllerBase
    {
        private ISerigrafiaServices<SerigrafiaDTO, SerigrafiaInsertDTO, SerigrafiaUpdateDTO> _SerigrafiaServices;
        public SerigrafiaController(
         [FromKeyedServices("serigrafiaServices")] ISerigrafiaServices<SerigrafiaDTO, SerigrafiaInsertDTO, SerigrafiaUpdateDTO> serigrafiaServices)
        {
            _SerigrafiaServices = serigrafiaServices;

        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetSerigrafia()
        {
            var serigrafiaDTOs = await _SerigrafiaServices.GetSerigrafia();
            return Ok(serigrafiaDTOs);
        }
        [AllowAnonymous]
        [HttpGet("GetSerigrafiaByModelo/{Modelo}")]
        public async Task<ActionResult<SerigrafiaDTO>> GetSerigrafiaByModelo(string Modelo)
        {
            var serigrafiaDto = await _SerigrafiaServices.GetSerigrafiaByNombre(Modelo);
            return serigrafiaDto == null ? NotFound() : Ok(serigrafiaDto);
        }
        [AllowAnonymous]
        [HttpGet("{Idserigrafia}")]
        public async Task<ActionResult<SerigrafiaDTO>> GetSerigrafiaById(int Idserigrafia)
        {
            var serigrafiaDto = await _SerigrafiaServices.GetSerigrafiaById(Idserigrafia);
            return serigrafiaDto == null ? NotFound() : serigrafiaDto;
        }
        //[Authorize(Roles = "Serigrafia")]
        [HttpPost]
        public async Task<ActionResult<SerigrafiaDTO>> AddModelo([FromForm] SerigrafiaInsertDTO serigrafiaInsertDto)
        {
            try
            {
                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}" +
                          $"{HttpContext.Request.PathBase.Value}";

                var serigrafiaDto = await _SerigrafiaServices.AddSerigrafia(serigrafiaInsertDto, baseUrl);
                return CreatedAtAction(nameof(GetSerigrafiaById), new { IdSerigrafia = serigrafiaDto.IdSerigrafia }, serigrafiaDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        //[Authorize(Roles = "Serigrafia")]
        [HttpPut("{Idserigrafia}")]
        public async Task<ActionResult<Serigrafia>> UpdateSerigrafia(int Idserigrafia, SerigrafiaUpdateDTO serigrafiaUpdateDto)
        {
            try
            {
                var serigrafiaDto = await _SerigrafiaServices.UpdateSerigrafia(Idserigrafia, serigrafiaUpdateDto);
                return serigrafiaDto == null ? NotFound() : Ok(serigrafiaDto);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}

