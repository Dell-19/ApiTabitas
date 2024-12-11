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
    public class BordadoController : ControllerBase
    {
        private IBordadoServices<BordadoDTO, BordadoInsertDTO, BordadoUpdateDTO> _BordadoServices;
        public BordadoController(
         [FromKeyedServices("bordadoServices")] IBordadoServices<BordadoDTO, BordadoInsertDTO, BordadoUpdateDTO> bordadoServices)
        {
            _BordadoServices = bordadoServices;

        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetBordado()
        {
            var bordadoDTOs = await _BordadoServices.GetBordado();
            return Ok(bordadoDTOs);
        }
        [AllowAnonymous]
        [HttpGet("GetBordadoByModelo/{Modelo}")]
        public async Task<ActionResult<BordadoDTO>> GetBordadoByModelo(string Modelo)
        {
            var bordadoDto = await _BordadoServices.GetBordadoByNombre(Modelo);
            return bordadoDto == null ? NotFound() : Ok(bordadoDto);
        }
        [AllowAnonymous]
        [HttpGet("{Idbordado}")]
        public async Task<ActionResult<BordadoDTO>> GetBordadoById(int Idbordado)
        {
            var bordadoDto = await _BordadoServices.GetBordadoById(Idbordado);
            return bordadoDto == null ? NotFound() : bordadoDto;
        }
        //[Authorize(Roles = "Bordado")]
        [HttpPost]
        public async Task<ActionResult<BordadoDTO>> AddModelo([FromForm] BordadoInsertDTO bordadoInsertDto)
        {
            try
            {
                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}" +
                              $"{HttpContext.Request.PathBase.Value}";

                var bordadoDto = await _BordadoServices.AddBordado(bordadoInsertDto, baseUrl);
                return CreatedAtAction(nameof(GetBordadoById), new { IdBordado = bordadoDto.IdBordado }, bordadoDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        //[Authorize(Roles = "Bordado")]
        [HttpPut("{Idbordado}")]
        public async Task<ActionResult<Bordado>> UpdateBordado(int Idbordado, BordadoUpdateDTO bordadoUpdateDto)
        {
            try
            {
                var bordadoDto = await _BordadoServices.UpdateBordado(Idbordado, bordadoUpdateDto);
                return bordadoDto == null ? NotFound() : Ok(bordadoDto);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

