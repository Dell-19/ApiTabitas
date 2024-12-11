﻿using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebTabitas.Models;
using WebTabitas.Models.ViewModel;
using WebTabitas.Repositorio.IRepositorio;
using WebTabitas.Utilidades;

namespace WebTabitas.Controllers
{

    public class LavadosController : Controller
    {
        private readonly ILavadoRepositorio _repoLavado;
        private readonly IGeneralRepositorio _repoGeneral;
        private readonly IProcesoActualRepositorio _repoProceso;
        public LavadosController(ILavadoRepositorio repoLavado, IGeneralRepositorio repoGeneral,IProcesoActualRepositorio repoProceso)
        {
            _repoLavado = repoLavado;
            _repoGeneral = repoGeneral;
            _repoProceso = repoProceso;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(new Lavado() { });
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosLavado()
        {
            return Json(new { data = await _repoLavado.GetTodoAsync(CT.RutasLavadoApi) });
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
                Lavado = new Lavado()

            };
            return View(objVM);
        }

        [HttpPost]
        public async Task <IActionResult> Create(Lavado lavado)
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
                Lavado = new Lavado()
            };
            if (ModelState.IsValid)
            {
                bool exito = await _repoLavado.CrearAsync(CT.RutasLavadoApi, lavado, HttpContext.Session.GetString("JWToken"));
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
            var item = await _repoLavado.GetAsync(CT.RutasLavadoApi, id.Value);

            if (item == null)
            { return NotFound(); }
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Lavado lavado)
        {
            if (ModelState.IsValid)
            {
                 await _repoLavado.ActualizarAsync(CT.RutasLavadoApi + lavado.IdLavado, lavado, HttpContext.Session.GetString("JWToken"));
                    return RedirectToAction(nameof(Index));
            }
            return View("Edit", lavado);
        }
    }
}
