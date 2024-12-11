using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebTabitas.Models;
using WebTabitas.Models.ViewModel;
using WebTabitas.Repositorio.IRepositorio;
using WebTabitas.Utilidades;

namespace WebTabitas.Controllers
{

    public class EtiquetasController : Controller
    {
        private readonly IEtiquetaRepositorio _repoEtiqueta;
        private readonly IGeneralRepositorio _repoGeneral;
        private readonly IProcesoActualRepositorio _repoProceso;
        public EtiquetasController(IEtiquetaRepositorio repoEtiqueta, IGeneralRepositorio repoGeneral,IProcesoActualRepositorio repoProceso)
        {
            _repoEtiqueta = repoEtiqueta;
            _repoGeneral = repoGeneral;
            _repoProceso = repoProceso;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(new Etiqueta() { });
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosEtiqueta()
        {
            return Json(new { data = await _repoEtiqueta.GetTodoAsync(CT.RutasEtiquetasApi) });
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
                Etiqueta = new Etiqueta()

            };
            return View(objVM);
        }

        [HttpPost]
        public async Task <IActionResult> Create(Etiqueta etiqueta)
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
                Etiqueta = new Etiqueta()
            };
            if (ModelState.IsValid)
            {
                bool exito = await _repoEtiqueta.CrearAsync(CT.RutasEtiquetasApi, etiqueta, HttpContext.Session.GetString("JWToken"));
                if (exito)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "El modelo seleccionado ya existe o no ha sido registrado en Corte o Corte Laser.");
                }
            }
            return View(objVM);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            { return NotFound(); }
            var item = await _repoEtiqueta.GetAsync(CT.RutasEtiquetasApi, id.Value);

            if (item == null)
            { return NotFound(); }
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Etiqueta etiqueta)
        {
            if (ModelState.IsValid)
            {
                 await _repoEtiqueta.ActualizarAsync(CT.RutasEtiquetasApi + etiqueta.IdEtiquetas, etiqueta, HttpContext.Session.GetString("JWToken"));
                    return RedirectToAction(nameof(Index));
            }
            return View("Edit", etiqueta);
        }
    }
}
