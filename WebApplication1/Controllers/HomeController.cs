using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using WebApplication1.Models;
using WebTabitas.Models;
using WebTabitas.Repositorio.IRepositorio;
using WebTabitas.Utilidades;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly IUsuarioRepositorio _repoUsu;

        public HomeController(IUsuarioRepositorio repoUsu)
        {
            //_logger = logger;
            _repoUsu = repoUsu;

        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            UsuarioAuth usuario = new UsuarioAuth();
            return View(usuario);
        }
        [HttpPost]
        public async Task<IActionResult> Login(UsuarioAuth obj)
        {
            if (ModelState.IsValid)
            {
                UsuarioAuth objUser = await _repoUsu.LoginAsync(CT.RutasUsuarioApi + "Login", obj);
                if (objUser.Token == null)
                {
                    TempData["alert"] = "Los Datos son Incorrectos";
                    return View();
                }
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Email, objUser.NombreUsuario));
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                HttpContext.Session.SetString("JWToken", objUser.Token);
                HttpContext.Session.SetString("Usuario", objUser.NombreUsuario);
                return RedirectToAction("Index");
            }
            else { return View(); }
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.Clear();
            if (Request.Cookies.ContainsKey(".AspNetCore.Session"))
            {
                Response.Cookies.Delete(".AspNetCore.Session");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(UsuarioAuth obj)
        {
            if (ModelState.IsValid) // Verificar si el modelo es válido
            {
                bool result = await _repoUsu.RegistroAsync("API_URL/Registro", obj); // Llamada a la API
                if (result)
                {
                    TempData["alert"] = "Registro Correcto"; // Mensaje de éxito
                    return RedirectToAction("Login", "Home"); // Redirige al login (Home/Login)
                }
                else
                {
                    TempData["alert"] = "Error al registrar el usuario."; // Mensaje de error
                    return View();
                }
            }

            return View(obj); // Si el modelo no es válido, volver al formulario
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
