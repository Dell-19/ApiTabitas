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
    public class CorteLaserController : ControllerBase
    {
        private ICorteLaserServices<CorteLaserDTO, CorteLaserInsertDTO, CorteLaserUpdateDTO> _CorteLaserServices;
        public CorteLaserController(
         [FromKeyedServices("corteLaserServices")] ICorteLaserServices<CorteLaserDTO, CorteLaserInsertDTO, CorteLaserUpdateDTO> corteLaserServices)
        {
            _CorteLaserServices = corteLaserServices;

        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetCorteLaser()
        {
            var corteLaserDTOs = await _CorteLaserServices.GetCorteLaser();
            return Ok(corteLaserDTOs);
        }
        [AllowAnonymous]
        [HttpGet("GetCorteLaserByModelo/{Modelo}")]
        public async Task<ActionResult<CorteLaserDTO>> GetCorteLaserByModelo(string Modelo)
        {
            var corteLaserDto = await _CorteLaserServices.GetCorteLaserByNombre(Modelo);
            return corteLaserDto == null ? NotFound() : Ok(corteLaserDto);
        }
        [AllowAnonymous]
        [HttpGet("{IdcorteLaser}")]
        public async Task<ActionResult<CorteLaserDTO>> GetCorteLaserById(int IdcorteLaser)
        {
            var corteLaserDto = await _CorteLaserServices.GetCorteLaserById(IdcorteLaser);
            return corteLaserDto == null ? NotFound() : corteLaserDto;
        }
        //[Authorize(Roles = "CorteLaser")]
        [HttpPost]
        public async Task<ActionResult<CorteLaserDTO>> AddModelo([FromForm] CorteLaserInsertDTO corteLaserInsertDto)
        {
            try
            {
                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}" +
                          $"{HttpContext.Request.PathBase.Value}";

            var corteLaserDto = await _CorteLaserServices.AddCorteLaser(corteLaserInsertDto, baseUrl);
            return CreatedAtAction(nameof(GetCorteLaserById), new { IdCorteLaser = corteLaserDto.IdCorteLaser }, corteLaserDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        //[Authorize(Roles = "CorteLaser")]
        [HttpPut("{IdcorteLaser}")]
        public async Task<ActionResult<CorteLaser>> UpdateCorteLaser(int IdcorteLaser, CorteLaserUpdateDTO corteLaserUpdateDto)
        {
            try
            {
                var corteLaserDto = await _CorteLaserServices.UpdateCorteLaser(IdcorteLaser, corteLaserUpdateDto);
                return corteLaserDto == null ? NotFound() : Ok(corteLaserDto);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}

