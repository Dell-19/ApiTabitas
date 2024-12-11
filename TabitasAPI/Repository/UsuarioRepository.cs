using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using TabitasAPI.Data;
using TabitasAPI.DTOs;
using TabitasAPI.Models;
using TabitasAPI.Repository.IRepository;
using XSystem.Security.Cryptography;

namespace TabitasAPI.Repository
{
    public class UsuarioRepository:IUsuarioRepository
    {
        //se intancia los objetos que se utilizaran
        private TabitasContext _context;//el contexto lo puedes usar donde quieras ya que esta inyectado solo declarandolo
        //el contexto es para trabajar con ef y trabajar con la bd
        private string claveSecreta;//clave de login
        public UsuarioRepository(TabitasContext context, IConfiguration config)// esto es para poder acceder en la aplicacion desde app seting
        {
            _context = context;
            claveSecreta = config.GetValue<string>("ApiSettings:Secreta");
        }

        public Usuarios GetUsuario(int UsuarioId)
        {
            return _context.Usuarios.FirstOrDefault(c => c.idUsuario == UsuarioId);//si no funciona cheacar esto****
        }

        public ICollection<Usuarios> GetUsuarios()
        {
            return _context.Usuarios.OrderBy(c => c.NombreUsuario).ToList();
        }

        public bool IsUniqueUser(string usuario)
        {
            var usuarioBd = _context.Usuarios.FirstOrDefault(u => u.NombreUsuario == usuario);
            if (usuarioBd == null)
            {
                return true;
            }
            return false;
        }

        public async Task<UsuarioLoginRespuestaDTO> Login(UsuarioLoginDTO usuarioLoginDTO)
        {
            var passwordEncriptado = obtenermd5(usuarioLoginDTO.Password);
            var usuarios = _context.Usuarios.FirstOrDefault(
                u => u.NombreUsuario.ToLower() == usuarioLoginDTO.NombreUsuario.ToLower()
                && u.Password == passwordEncriptado);
            //validamos si el ususario no existe con la combinacion de usuario y contrasena
            if (usuarios == null)
            {
                return new UsuarioLoginRespuestaDTO()
                {
                    token = "",
                    Usuarios = null
                };
            }
            //aqui existe el usuario entonce spodemos procesar el login
            var manejadortoken = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(claveSecreta);// la clave se hace en appsetting (donde esta la conexion)
            //token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,usuarios.NombreUsuario.ToString()),
                    new Claim(ClaimTypes.Role, usuarios.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(1 ),
                SigningCredentials = new (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = manejadortoken.CreateToken(tokenDescriptor);
            UsuarioLoginRespuestaDTO usuarioLoginRespuestaDTO = new UsuarioLoginRespuestaDTO()
            {
                token = manejadortoken.WriteToken(token),
                Usuarios = usuarios
            };
            return usuarioLoginRespuestaDTO;
        }

        public async Task<Usuarios> Registro(UsuarioInsertDTO usuarioInsertDTO)
        {
            //encriptar la passwaord 
            var passwordEncriptado = obtenermd5(usuarioInsertDTO.Password);
            Usuarios usuarios = new Usuarios()
            {
                NombreUsuario = usuarioInsertDTO.NombreUsuario,
                Password = passwordEncriptado,
                Nombre = usuarioInsertDTO.Nombre,
                Role= usuarioInsertDTO.Role,
            };
             _context.Usuarios.Add(usuarios);
            await _context.SaveChangesAsync();
            usuarios.Password = passwordEncriptado;
            return usuarios;
        }
        // metodo para encriptar contrasena con md5 e usa tanto en acceso como en registro
        public static string obtenermd5(string valor)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(valor);
            data = x.ComputeHash(data);
            string resp = "";
            for (int i = 0; i < data.Length; i++)
                resp += data[i].ToString("x2").ToLower();
            return resp;
        }
    }
}
