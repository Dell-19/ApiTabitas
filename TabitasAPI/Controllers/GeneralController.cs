using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TabitasAPI.DTOs;
using TabitasAPI.Models;
using TabitasAPI.Services;
using TabitasAPI.Services.IServices;

namespace TabitasAPI.Controllers
{
    [ResponseCache(Duration = 10)]
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralController : ControllerBase
    {
        private IGeneralServices<GeneralDTO, GeneralInsertDTO, GeneralUpdateDTO> _GeneralServices;
        public GeneralController(
         [FromKeyedServices("generalServices")] IGeneralServices<GeneralDTO, GeneralInsertDTO, GeneralUpdateDTO> generalServices)
        {
            _GeneralServices = generalServices;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetGeneral()
        {
            var generalDTOs = await _GeneralServices.GetGeneral();
            return Ok(generalDTOs);
        }
        [AllowAnonymous]
        [HttpGet("GetByModelo/{Modelo}")]
        public async Task<ActionResult<GeneralDTO>> GetGeneralByModelo(string Modelo)
        {
            var generalDto = await _GeneralServices.GetGeneralByNombre(Modelo);
            return generalDto == null ? NotFound() : generalDto;
        }
        [AllowAnonymous]
        [HttpGet("{Idgeneral}")]
        public async Task<ActionResult<GeneralDTO>> GetGeneralById(int Idgeneral)
        {
            var generalDto = await _GeneralServices.GetGeneralById(Idgeneral);
            return generalDto == null ? NotFound() : generalDto;
        }
        //[Authorize(Roles = "General")]
        [HttpPost]
        public async Task<ActionResult<GeneralDTO>> AddModelo([FromForm] GeneralInsertDTO generalInsertDto)
        {
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}" +
                $"{HttpContext.Request.PathBase.Value}";
            var generalDto = await _GeneralServices.AddGeneral(generalInsertDto, baseUrl);
            return CreatedAtAction(nameof(GetGeneralById), new { Idgeneral = generalDto.IdGeneral }, generalDto);
        }
        //[Authorize(Roles = "General")]
        [HttpPut("{Idgeneral}")]
        public async Task<ActionResult<General>> UpdateGeneral(int Idgeneral, GeneralUpdateDTO generalUpdateDto)
        {
            var generalDto = await _GeneralServices.UpdateGeneral(Idgeneral, generalUpdateDto);
            return generalDto == null ? NotFound() : Ok(generalDto);
        }
    }
}