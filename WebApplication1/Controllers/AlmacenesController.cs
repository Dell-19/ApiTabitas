using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebTabitas.Models;
using WebTabitas.Models.ViewModel;
using WebTabitas.Repositorio.IRepositorio;
using WebTabitas.Utilidades;

namespace WebTabitas.Controllers
{

    public class AlmacenesController : Controller
    {
        private readonly IAlmacenRepositorio _repoAlmacen;
        private readonly IGeneralRepositorio _repoGeneral;
        private readonly IProcesoActualRepositorio _repoProceso;
        public AlmacenesController(IAlmacenRepositorio repoAlmacen,IGeneralRepositorio repoGeneral,IProcesoActualRepositorio repoProceso)
        {
            _repoAlmacen = repoAlmacen;
            _repoGeneral = repoGeneral;
            _repoProceso = repoProceso;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(new Almacen() { });
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosAlmacen()
        {
            return Json(new { data = await _repoAlmacen.GetTodoAsync(CT.RutasAlmacenApi) });
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
                Almacen = new Almacen()
            };
            return View(objVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Almacen almacen)
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
                Almacen = almacen
            };

            if (ModelState.IsValid)
            {
                bool exito = await _repoAlmacen.CrearAsync(CT.RutasAlmacenApi, almacen, HttpContext.Session.GetString("JWToken"));
                if (exito)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "El modelo seleccionado ya existe en Almacén o ocurrió un error al guardar.");
                }
            }
            return View(objVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            { return NotFound();}
            var item = await _repoAlmacen.GetAsync(CT.RutasAlmacenApi, id.Value);
            if (item == null)
            { return NotFound();}
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Almacen almacen)
        {
            if (ModelState.IsValid)
            {
                await _repoAlmacen.ActualizarAsync(CT.RutasAlmacenApi + almacen.IdAlmacen, almacen, HttpContext.Session.GetString("JWToken"));
                    return RedirectToAction(nameof(Index));
            }
            return View("Edit", almacen);
        }
    }
}
