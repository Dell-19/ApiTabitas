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
    [ResponseCache(Duration =30)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProcesoActualController : ControllerBase
    {
        private IProcesoActualServices<ProcesoActualDTO> _ProcesoActualServices;
        public ProcesoActualController(
         [FromKeyedServices("procesoActualServices")] IProcesoActualServices<ProcesoActualDTO> procesoActualServices)
        {
            _ProcesoActualServices = procesoActualServices;

        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetProcesoActual()
        {
            var procesoActualDTOs = await _ProcesoActualServices.GetProcesoActual();
            return Ok(procesoActualDTOs);
        }
    }
}

