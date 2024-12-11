using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebTabitas.Models;
using WebTabitas.Models.ViewModel;
using WebTabitas.Repositorio.IRepositorio;
using WebTabitas.Utilidades;

namespace WebTabitas.Controllers
{

    public class CalidadesController : Controller
    {
        private readonly ICalidadRepositorio _repoCalidad;
        private readonly IGeneralRepositorio _repoGeneral;
        private readonly IProcesoActualRepositorio _repoProceso;
        public CalidadesController(ICalidadRepositorio repoCalidad, IGeneralRepositorio repoGeneral,IProcesoActualRepositorio repoProceso)
        {
            _repoCalidad = repoCalidad;
            _repoGeneral = repoGeneral;
            _repoProceso = repoProceso;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(new Calidad() { });
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosCalidad()
        {
            return Json(new { data = await _repoCalidad.GetTodoAsync(CT.RutasCalidadApi) });
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
                Calidad = new Calidad()

            };
            return View(objVM);
        }

        [HttpPost]
        public async Task <IActionResult> Create(Calidad calidad)
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
                Calidad = new Calidad()
            };
            if (ModelState.IsValid)
            {
                bool exito = await _repoCalidad.CrearAsync(CT.RutasCalidadApi, calidad, HttpContext.Session.GetString("JWToken"));
                if (exito)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "El modelo seleccionado ya existe o no ha sido registrado en un Proceso Anterior.");
                }
            }
            return View(objVM);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            { return NotFound(); }
            var item = await _repoCalidad.GetAsync(CT.RutasCalidadApi, id.Value);

            if (item == null)
            { return NotFound(); }
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Calidad calidad)
        {
            if (ModelState.IsValid)
            {
                  await _repoCalidad.ActualizarAsync(CT.RutasCalidadApi + calidad.IdCalidad, calidad, HttpContext.Session.GetString("JWToken"));
                    return RedirectToAction(nameof(Index));
            }
            return View("Edit",calidad);
        }
    }
}
