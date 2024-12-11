using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebTabitas.Models;
using WebTabitas.Models.ViewModel;
using WebTabitas.Repositorio.IRepositorio;
using WebTabitas.Utilidades;

namespace WebTabitas.Controllers
{

    public class CorteLaseresController : Controller
    {
        private readonly ICorteLaserRepositorio _repoCorteLaser;
        private readonly IGeneralRepositorio _repoGeneral;
        private readonly IProcesoActualRepositorio _repoProceso;
        public CorteLaseresController(ICorteLaserRepositorio repoCorteLaser, IGeneralRepositorio repoGeneral,IProcesoActualRepositorio repoProceso)
        {
            _repoCorteLaser = repoCorteLaser;
            _repoGeneral = repoGeneral;
            _repoProceso = repoProceso;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(new CorteLaser() { });
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosCorteLaser()
        {
            return Json(new { data = await _repoCorteLaser.GetTodoAsync(CT.RutasCorteLaserApi) });
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            IEnumerable<ProcesoActual> areaList = (IEnumerable<ProcesoActual>)await _repoProceso.GetTodoAsync(CT.RutasProcesoActualApi);
            IEnumerable<General> gList = (IEnumerable<General>)await _repoGeneral.GetTodoAsync(CT.RutasGeneralApi);
            AlmacenVM objVM = new AlmacenVM()
            {
                ListaAreas = areaList.Select(a => new SelectListItem
                {
                    Text = a.Area,
                    Value = a.IdProceso.ToString()
                }),
                ListaGeneral = gList.Select(a => new SelectListItem
                {
                    Text = a.Modelo,
                    Value = a.IdGeneral.ToString()
                }),
                CorteLaser = new CorteLaser()

            };
            return View(objVM);
        }

        [HttpPost]
        public async Task <IActionResult> Create(CorteLaser corteLaser)
        {
            IEnumerable<ProcesoActual> areaList = (IEnumerable<ProcesoActual>)await _repoProceso.GetTodoAsync(CT.RutasProcesoActualApi);
            IEnumerable<General> gList = (IEnumerable<General>)await _repoGeneral.GetTodoAsync(CT.RutasGeneralApi);
            AlmacenVM objVM = new AlmacenVM()
            {
                ListaAreas = areaList.Select(a => new SelectListItem
                {
                    Text = a.Area,
                    Value = a.IdProceso.ToString()
                }),
                ListaGeneral = gList.Select(a => new SelectListItem
                {
                    Text = a.Modelo,
                    Value = a.IdGeneral.ToString()
                }),
                CorteLaser = new CorteLaser()
            };
            if (ModelState.IsValid)
            {
               bool exito = await _repoCorteLaser.CrearAsync(CT.RutasCorteLaserApi, corteLaser, HttpContext.Session.GetString("JWToken"));
                if (exito)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "El modelo seleccionado ya existe o no ha sido registrado en Almacen.");
                }
            }
            return View(objVM);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            { return NotFound(); }
            var item = await _repoCorteLaser.GetAsync(CT.RutasCorteLaserApi, id.Value);

            if (item == null)
            { return NotFound(); }
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CorteLaser corte)
        {
            if (ModelState.IsValid)
            {
                 await _repoCorteLaser.ActualizarAsync(CT.RutasCorteLaserApi + corte.IdCorteLaser, corte, HttpContext.Session.GetString("JWToken"));
                 return RedirectToAction(nameof(Index));
   
            }
            return View("Edit", corte);
        }
    }
}
