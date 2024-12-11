using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TabitasAPI.DTOs;
using TabitasAPI.Models;
using TabitasAPI.Services.IServices;
using XAct.Security;
using AllowAnonymousAttribute = Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute;

namespace TabitasAPI.Controllers
{
    [ResponseCache(Duration =30)]
    [Route("api/[controller]")]
    [ApiController]
    public class AlmacenController : ControllerBase
    {
        private IAlmacenServices<AlmacenDTO, AlmacenInsertDTO, AlmacenUpdateDTO> _AlmacenServices;
        public AlmacenController(
         [FromKeyedServices("almacenServices")] IAlmacenServices<AlmacenDTO, AlmacenInsertDTO, AlmacenUpdateDTO> almacenServices)
        {
            _AlmacenServices = almacenServices;

        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAlmacen()
        {
            var almacenDTOs = await _AlmacenServices.GetAlmacen();
            return Ok(almacenDTOs);
        }
        [AllowAnonymous]
        [HttpGet("GetAlmacenByModelo/{Modelo}")]
        public async Task<ActionResult<AlmacenDTO>> GetAlmacenByModelo(string Modelo)
        {
            var almacenDto = await _AlmacenServices.GetAlmacenByNombre(Modelo);
            return almacenDto == null ? NotFound() : Ok(almacenDto);
        }
        [AllowAnonymous]
        [HttpGet("{Idalmacen}")]
        public async Task<ActionResult<AlmacenDTO>> GetAlmacenById(int Idalmacen)
        {
            var almacenDto = await _AlmacenServices.GetAlmacenById(Idalmacen);
            return almacenDto == null ? NotFound() : almacenDto;
        }
        //[Authorize(Roles = "Almacen")]
        [HttpPost]
        public async Task<ActionResult<AlmacenDTO>> AddModelo([FromForm] AlmacenInsertDTO almacenInsertDto)
        {
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}" +
                          $"{HttpContext.Request.PathBase.Value}";

            var almacenDto = await _AlmacenServices.AddAlmacen(almacenInsertDto, baseUrl);
            return CreatedAtAction(nameof(GetAlmacenById), new { IdAlmacen = almacenDto.IdAlmacen }, almacenDto);
        }
        //[Authorize(Roles = "Almacen")]
        [HttpPut("{Idalmacen}")]
        public async Task<IActionResult> UpdateAlmacen(int Idalmacen, AlmacenUpdateDTO almacenUpdateDto)
        {
            try
            {
                var almacenDto = await _AlmacenServices.UpdateAlmacen(Idalmacen, almacenUpdateDto);
                return almacenDto == null ? NotFound() : Ok(almacenDto);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


    }
}

