using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebTabitas.Models;
using WebTabitas.Models.ViewModel;
using WebTabitas.Repositorio.IRepositorio;
using WebTabitas.Utilidades;

namespace WebTabitas.Controllers
{

    public class MaquilasController : Controller
    {
        private readonly IMaquilaRepositorio _repoMaquila;
        private readonly IGeneralRepositorio _repoGeneral;
        private readonly IProcesoActualRepositorio _repoProceso;
        public MaquilasController(IMaquilaRepositorio repoMaquila, IGeneralRepositorio repoGeneral,IProcesoActualRepositorio repoProceso)
        {
            _repoMaquila = repoMaquila;
            _repoGeneral = repoGeneral;
            _repoProceso = repoProceso;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(new Maquila() { });
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosMaquila()
        {
            return Json(new { data = await _repoMaquila.GetTodoAsync(CT.RutasMaquilaApi) });
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
                Maquila = new Maquila()

            };
            return View(objVM);
        }

        [HttpPost]
        public async Task <IActionResult> Create(Maquila maquila)
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
                Maquila = new Maquila()
            };
            if (ModelState.IsValid)
            {
                bool exito = await _repoMaquila.CrearAsync(CT.RutasMaquilaApi, maquila, HttpContext.Session.GetString("JWToken"));
                if (exito)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "El modelo seleccionado ya existe o no ha sido registrado en Etiquetas.");
                }
            }
            return View(objVM);
        }
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        { return NotFound(); }
        var item = await _repoMaquila.GetAsync(CT.RutasMaquilaApi, id.Value);

        if (item == null)
        { return NotFound(); }
        return View(item);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(Maquila maquila)
    {
        if (ModelState.IsValid)
        {
             await _repoMaquila.ActualizarAsync(CT.RutasMaquilaApi + maquila.IdMaquila, maquila, HttpContext.Session.GetString("JWToken"));
                    return RedirectToAction(nameof(Index));
            }
        return View("Edit", maquila);
    }
}
}
