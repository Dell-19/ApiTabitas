using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TabitasAPI.DTOs;
using TabitasAPI.Models;
using TabitasAPI.Repository.IRepository;

namespace TabitasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usRepo;
        protected RespuestasAPI _respuestasApi;
        private readonly IMapper _mapper;
        public UsuarioController(IUsuarioRepository usRepo, IMapper mapper)
        {
            _usRepo = usRepo;
            _mapper = mapper;
            this._respuestasApi = new();
        }
        //ver lista de ususarios
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetUsuarios() 
        {
            var listaUsuarios = _usRepo.GetUsuarios();
            var listaUsuariosDTO = new List<UsuarioDTO>();
            foreach (var lista in listaUsuarios)
            {
                listaUsuariosDTO.Add(_mapper.Map<UsuarioDTO>(lista));
            }
            return Ok(listaUsuariosDTO);
        }
        
        // ver un solo usuario
        [HttpGet("{idUsuario:int}",Name = "GetUsuario")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUsuario(int idUsuario)
        {
            var itemUsuario = _usRepo.GetUsuario(idUsuario);
            if (itemUsuario == null)
            {
                return NotFound();
            }
            var itemUsuarioDTO = _mapper.Map<UsuarioDTO>(itemUsuario);
            return Ok(itemUsuarioDTO);
        }
        //ingresar
        [HttpPost("Registro")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Registro([FromBody] UsuarioInsertDTO usuarioInsertDTO)
        {
            bool validarNombreUsuarioUnico = _usRepo.IsUniqueUser(usuarioInsertDTO.NombreUsuario);
            if (!validarNombreUsuarioUnico)
            {
                _respuestasApi.StatusCode = HttpStatusCode.BadRequest;
                _respuestasApi.IsSuccess = false;
                _respuestasApi.ErrorMessages.Add("El nombre de ususario ya existe");
                return BadRequest(_respuestasApi);
            }
            var usuarios = _usRepo.Registro(usuarioInsertDTO);
            if (usuarios == null)
            {
                _respuestasApi.StatusCode = HttpStatusCode.BadRequest;
                _respuestasApi.IsSuccess = false;
                _respuestasApi.ErrorMessages.Add("Error en el registro");
                return BadRequest(_respuestasApi);
            }
            _respuestasApi.StatusCode = HttpStatusCode.OK;
            _respuestasApi.IsSuccess = true;
            return Ok(_respuestasApi);
        }
        //ingresar
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Registro([FromBody] UsuarioLoginDTO usuarioLoginDTO)
        {
            var respuestaLogin = await _usRepo.Login(usuarioLoginDTO);
            if (respuestaLogin.Usuarios == null || string.IsNullOrEmpty(respuestaLogin.token))
            {
                _respuestasApi.StatusCode = HttpStatusCode.BadRequest;
                _respuestasApi.IsSuccess = false;
                _respuestasApi.ErrorMessages.Add("El nombre de usuario o passwod son incorrectos");
                return BadRequest(_respuestasApi);
            }
            _respuestasApi.StatusCode = HttpStatusCode.OK;
            _respuestasApi.IsSuccess = true;
            _respuestasApi.Result = respuestaLogin;
            return Ok(_respuestasApi);
        }
    }
    
}
