using Microsoft.AspNetCore.Mvc;
using HospitalSystemBlazor.Service;
using HospitalSystemBlazor.Entities.DTOs;
using HospitalSystemBlazor.Service.Interface;
using Microsoft.AspNetCore.Authorization;

namespace HospitalSystemBlazor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _service;
        private readonly IHttpContextAccessor _http;
        private readonly IService<UsuarioDto> _uService;

        public AuthController(AuthService service, IHttpContextAccessor http, IService<UsuarioDto> uService)
        {
            _service = service;
            _http = http;
            _uService = uService;
        }

        [HttpPost("login")]
        public  async Task<ActionResult> Login(LoginUser user)
        {
            var result = await _service.LoginUser(user);

            if (!result.isSucces)
            {
                return BadRequest(new { message = result.Error });
            }


            return Ok(new { result.isSucces, message = result.Value });
        }

        [Authorize]    
        [HttpGet("headers")]
        public IActionResult GetHeaderData()
        {
            var userId = User.FindFirst("UserId")?.Value;
            var email = User.FindFirst("Email")?.Value;

            if (userId == null || email == null)
            {
                return Unauthorized("Usuario no autenticado o token inválido.");
            }

            return Ok($"Datos seguros para el usuario: {userId}, Email: {email}");
        }

            //[HttpGet("headers")]
            //public  async Task<ActionResult>  VerificarAutenticacion()
            //{
            //    var userid = _http.HttpContext?.Request.Headers["UserId"].ToString();


            //    if (string.IsNullOrEmpty(userid) || !int.TryParse(userid, out var userId))
            //    {
            //        return BadRequest(new { mensaje = "El header 'UserId' es inválido o está ausente" });
            //    }

            //    var usuarioId = Int32.Parse(userid);
            //    bool status = false;

            //    if(string.IsNullOrEmpty(userid))
            //    {
            //        return BadRequest(new { mensaje = "No se encontraron los headers", status });
            //    }

            //    var usuario = await _uService.Detalles(int.Parse(userid));

            //    if(!usuario.isSucces)
            //    {
            //        return BadRequest(new {usuario.isSucces, message = usuario.Error });
            //    }

            //    return Ok(new { usuario.isSucces, usuarioId });
            //}





        }
    }
