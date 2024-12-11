using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebTabitas.Models;
using WebTabitas.Models.ViewModel;
using WebTabitas.Repositorio.IRepositorio;
using WebTabitas.Utilidades;

namespace WebTabitas.Controllers
{

    public class CortesController : Controller
    {
        private readonly ICorteRepositorio _repoCorte;
        private readonly IGeneralRepositorio _repoGeneral;
        private readonly IProcesoActualRepositorio _repoProceso;
        public CortesController(IGeneralRepositorio repoGeneral, IProcesoActualRepositorio repoProceso, ICorteRepositorio repoCorte)
        {
            _repoCorte = repoCorte;
            _repoGeneral = repoGeneral;
            _repoProceso = repoProceso;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(new Corte() { });
        }
        [HttpGet]
        public async Task<IActionResult> GetTodosCorte()
        {
            return Json(new { data = await _repoCorte.GetTodoAsync(CT.RutasCorteApi) });
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
                Corte = new Corte()

            };
            return View(objVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Corte corte)
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
                Corte = new Corte()
            };

            var files = HttpContext.Request.Form.Files;
            if (files.Count > 0)
            {
                var file = files[0];
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    corte.RutaImagen = Convert.ToBase64String(memoryStream.ToArray());
                }
            }
            bool exito = await _repoCorte.CrearGeneralAsync(CT.RutasCorteApi, corte, HttpContext.Session.GetString("JWToken"));
            if (exito)
            {return RedirectToAction(nameof(Index));}
            else
            {
                ModelState.AddModelError(string.Empty, "El modelo seleccionado ya existe o  no ha sido registrado en Almacen.");
                return View(objVM);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            { return NotFound(); }
            var item = await _repoCorte.GetAsync(CT.RutasCorteApi, id.Value);

            if (item == null)
            { return NotFound(); }
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Corte corte)
        {
            if (ModelState.IsValid)
            {
                bool exito = await _repoCorte.ActualizarAsync(CT.RutasCorteApi + corte.IdCorte, corte, HttpContext.Session.GetString("JWToken"));
                if (exito)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "El modelo ya fue editado una vez.");
                    return View("Edit", corte);
                }
            }
            return View("Edit", corte);
        }
    }
}
