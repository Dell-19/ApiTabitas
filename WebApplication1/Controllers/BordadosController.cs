using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebTabitas.Models;
using WebTabitas.Models.ViewModel;
using WebTabitas.Repositorio.IRepositorio;
using WebTabitas.Utilidades;

namespace WebTabitas.Controllers
{

    public class BordadosController : Controller
    {
        private readonly IBordadoRepositorio _repoBordado;
        private readonly IGeneralRepositorio _repoGeneral;
        private readonly IProcesoActualRepositorio _repoProceso;
        public BordadosController(IBordadoRepositorio repoBordado, IGeneralRepositorio repoGeneral,IProcesoActualRepositorio repoProceso)
        {
            _repoBordado = repoBordado;
            _repoGeneral = repoGeneral;
            _repoProceso = repoProceso;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(new Bordado() { });
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosBordado()
        {
            return Json(new { data = await _repoBordado.GetTodoAsync(CT.RutasBordadoApi) });
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
                Bordado = new Bordado()

            };
            return View(objVM);
        }

        [HttpPost]
        public async Task <IActionResult> Create(Bordado bordado)
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
                Bordado = new Bordado()
            };
            if (ModelState.IsValid)
            {
                bool exito = await _repoBordado.CrearAsync(CT.RutasBordadoApi, bordado, HttpContext.Session.GetString("JWToken"));
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
            var item = await _repoBordado.GetAsync(CT.RutasBordadoApi, id.Value);

            if (item == null)
            { return NotFound(); }
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Bordado bordado)
        {
            if (ModelState.IsValid)
            {
                await _repoBordado.ActualizarAsync(CT.RutasBordadoApi + bordado.IdBordado, bordado, HttpContext.Session.GetString("JWToken"));
                    return RedirectToAction(nameof(Index));
            }
            return View("Edit", bordado);
        }
    }
}
