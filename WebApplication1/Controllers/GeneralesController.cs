using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebTabitas.Models;
using WebTabitas.Models.ViewModel;
using WebTabitas.Repositorio.IRepositorio;
using WebTabitas.Utilidades;

namespace WebTabitas.Controllers
{

    public class GeneralesController : Controller
    {
        private readonly IGeneralRepositorio _repoGeneral;
        private readonly IProcesoActualRepositorio _repoProceso;
        public GeneralesController(IGeneralRepositorio repoGeneral,IProcesoActualRepositorio repoProceso)
        {
            _repoGeneral = repoGeneral;
            _repoProceso = repoProceso;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(new General() { });
        }
        [HttpGet]
        public async Task<IActionResult> GetTodosGeneral()
        {
            return Json(new { data = await _repoGeneral.GetTodoAsync(CT.RutasGeneralApi)});
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            IEnumerable<ProcesoActual> areaList = (IEnumerable<ProcesoActual>)await _repoProceso.GetTodoAsync(CT.RutasProcesoActualApi);
            AlmacenVM objVM = new AlmacenVM()
            {
                ListaAreas = areaList.Select(a => new SelectListItem
                {
                    Text = a.Area,
                    Value = a.IdProceso.ToString()
                }),
                General = new General()
            };
            return View(objVM);
        }

        [HttpPost]
        public async Task <IActionResult> Create(General general)
        {
            IEnumerable<ProcesoActual> areaList = (IEnumerable<ProcesoActual>)await _repoProceso.GetTodoAsync(CT.RutasProcesoActualApi);
            AlmacenVM objVM = new AlmacenVM()
            {
                ListaAreas = areaList.Select(a => new SelectListItem
                {
                    Text = a.Area,
                    Value = a.IdProceso.ToString()
                }),
                General = new General()
            };
            if (ModelState.IsValid)
            {
                if (general.IdProceso == 0)
                { return View(objVM);}
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                { general.Imagen = files[0]; }
                else
                {return View(objVM);}
                bool exito = await _repoGeneral.CrearGeneralAsync(CT.RutasGeneralApi, general, HttpContext.Session.GetString("JWToken"));
                if (exito)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "El modelo ya existe o ocurrió un error al guardar.");
                    return View(objVM);
                }
            }
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }
            return View(objVM);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {  return NotFound(); }
            var item= await _repoGeneral.GetAsync(CT.RutasGeneralApi, id.Value);

            if (item == null)
            { return NotFound(); }
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(General general)
        {
            if (ModelState.IsValid)
            {
                await _repoGeneral.ActualizarAsync(CT.RutasGeneralApi + general.IdGeneral, general, HttpContext.Session.GetString("JWToken"));
                return RedirectToAction(nameof(Index));
            }
            return View(general);
        }
    }
}
