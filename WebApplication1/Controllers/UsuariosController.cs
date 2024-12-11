using Microsoft.AspNetCore.Mvc;
using WebTabitas.Models;
using WebTabitas.Repositorio.IRepositorio;

namespace WebTabitas.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuarioRepositorio _repoUsuario;
        public UsuariosController(IUsuarioRepositorio repoUsuario)
        {
            _repoUsuario = repoUsuario;
        }
        public IActionResult Index()
        {
            return View(new Usuarios() { });
        }

    }
}
