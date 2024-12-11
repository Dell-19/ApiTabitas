using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebTabitas.Models;
using WebTabitas.Models.ViewModel;
using WebTabitas.Repositorio.IRepositorio;
using WebTabitas.Utilidades;

namespace WebTabitas.Controllers
{

    public class SerigrafiasController : Controller
    {
        private readonly ISerigrafiaRepositorio _repoSerigrafia;
        private readonly IGeneralRepositorio _repoGeneral;
        private readonly IProcesoActualRepositorio _repoProceso;
        public SerigrafiasController(ISerigrafiaRepositorio repoSerigrafia, IGeneralRepositorio repoGeneral,IProcesoActualRepositorio repoProceso)
        {
            _repoSerigrafia = repoSerigrafia;
            _repoGeneral = repoGeneral;
            _repoProceso = repoProceso;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(new Serigrafia() { });
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosSerigrafia()
        {
            return Json(new { data = await _repoSerigrafia.GetTodoAsync(CT.RutasSerigrafiaApi) });
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
                Serigrafia = new Serigrafia()

            };
            return View(objVM);
        }

        [HttpPost]
        public async Task <IActionResult> Create(Serigrafia serigrafia)
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
                Serigrafia = new Serigrafia()
            };
            if (ModelState.IsValid)
            {
                bool exito = await _repoSerigrafia.CrearAsync(CT.RutasSerigrafiaApi, serigrafia, HttpContext.Session.GetString("JWToken"));
                if (exito)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "El modelo seleccionado ya existe o  no ha sido registrado en Etiquetas.");
                }
            }
            return View(objVM);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            { return NotFound(); }
            var item = await _repoSerigrafia.GetAsync(CT.RutasSerigrafiaApi, id.Value);

            if (item == null)
            { return NotFound(); }
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Serigrafia serigrafia)
        {
            if (ModelState.IsValid)
            {
                 await _repoSerigrafia.ActualizarAsync(CT.RutasSerigrafiaApi + serigrafia.IdSerigrafia, serigrafia, HttpContext.Session.GetString("JWToken"));
                    return RedirectToAction(nameof(Index));
            }
            return View("Edit", serigrafia);
        }
    }
}
