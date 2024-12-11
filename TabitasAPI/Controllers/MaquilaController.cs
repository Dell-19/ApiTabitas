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
    public class MaquilaController : ControllerBase
    {
        private IMaquilaServices<MaquilaDTO, MaquilaInsertDTO, MaquilaUpdateDTO> _MaquilaServices;
        public MaquilaController(
         [FromKeyedServices("maquilaServices")] IMaquilaServices<MaquilaDTO, MaquilaInsertDTO, MaquilaUpdateDTO> maquilaServices)
        {
            _MaquilaServices = maquilaServices;

        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetMaquila()
        {
            var maquilaDTOs = await _MaquilaServices.GetMaquila();
            return Ok(maquilaDTOs);
        }
        [AllowAnonymous]
        [HttpGet("GetMaquilaByModelo/{Modelo}")]
        public async Task<ActionResult<MaquilaDTO>> GetMaquilaByModelo(string Modelo)
        {
            var maquilaDto = await _MaquilaServices.GetMaquilaByNombre(Modelo);
            return maquilaDto == null ? NotFound() : Ok(maquilaDto);
        }
        [AllowAnonymous]
        [HttpGet("{Idmaquila}")]
        public async Task<ActionResult<MaquilaDTO>> GetMaquilaById(int Idmaquila)
        {
            var maquilaDto = await _MaquilaServices.GetMaquilaById(Idmaquila);
            return maquilaDto == null ? NotFound() : maquilaDto;
        }
        //[Authorize(Roles = "Maquila")]
        [HttpPost]
        public async Task<ActionResult<MaquilaDTO>> AddModelo([FromForm] MaquilaInsertDTO maquilaInsertDto)
        {
            try
            {
                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}" +
                          $"{HttpContext.Request.PathBase.Value}";

                var maquilaDto = await _MaquilaServices.AddMaquila(maquilaInsertDto, baseUrl);
                return CreatedAtAction(nameof(GetMaquilaById), new { IdMaquila = maquilaDto.IdMaquila }, maquilaDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        //[Authorize(Roles = "Maquila")]
        [HttpPut("{Idmaquila}")]
        public async Task<ActionResult<Maquila>> UpdateMaquila(int Idmaquila, MaquilaUpdateDTO maquilaUpdateDto)
        { try
            {
                var maquilaDto = await _MaquilaServices.UpdateMaquila(Idmaquila, maquilaUpdateDto);
                return maquilaDto == null ? NotFound() : Ok(maquilaDto);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}

