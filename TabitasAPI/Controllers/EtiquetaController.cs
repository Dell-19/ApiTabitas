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
    public class EtiquetaController : ControllerBase
    {
        private IEtiquetaServices<EtiquetaDTO, EtiquetaInsertDTO, EtiquetaUpdateDTO> _EtiquetaServices;
        public EtiquetaController(
         [FromKeyedServices("etiquetaServices")] IEtiquetaServices<EtiquetaDTO, EtiquetaInsertDTO, EtiquetaUpdateDTO> etiquetaServices)
        {
            _EtiquetaServices = etiquetaServices;

        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetEtiqueta()
        {
            var etiquetaDTOs = await _EtiquetaServices.GetEtiqueta(); 
            return Ok(etiquetaDTOs);
        }
        [AllowAnonymous]
        [HttpGet("GetEtiquetaByModelo/{Modelo}")]
        public async Task<ActionResult<EtiquetaDTO>> GetEtiquetaByModelo(string Modelo)
        {
            var etiquetaDto = await _EtiquetaServices.GetEtiquetaByNombre(Modelo);
            return etiquetaDto == null ? NotFound() : Ok(etiquetaDto);
        }
        [AllowAnonymous]
        [HttpGet("{Idetiquetas}")]
        public async Task<ActionResult<EtiquetaDTO>> GetEtiquetaById(int Idetiquetas)
        {
            var etiquetaDto = await _EtiquetaServices.GetEtiquetaById(Idetiquetas);
            return etiquetaDto == null ? NotFound() : etiquetaDto;
        }
        //[Authorize(Roles = "Etiqueta")]
        [HttpPost]
        public async Task<ActionResult<EtiquetaDTO>> AddModelo([FromForm] EtiquetaInsertDTO etiquetaInsertDto)
        {
            try
            {
                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}" +
                              $"{HttpContext.Request.PathBase.Value}";
                var etiquetaDto = await _EtiquetaServices.AddEtiqueta(etiquetaInsertDto, baseUrl);
                return CreatedAtAction(nameof(GetEtiquetaById), new { IdEtiquetas = etiquetaDto.IdEtiquetas }, etiquetaDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //[Authorize(Roles = "Etiqueta")]
        [HttpPut("{Idetiquetas}")]
        public async Task<ActionResult<Etiqueta>> UpdateEtiqueta(int Idetiquetas, EtiquetaUpdateDTO etiquetaUpdateDto)
        {
            try
            {
                var etiquetaDto = await _EtiquetaServices.UpdateEtiqueta(Idetiquetas, etiquetaUpdateDto);
                return etiquetaDto == null ? NotFound() : Ok(etiquetaDto);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

