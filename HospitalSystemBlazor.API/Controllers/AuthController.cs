using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HospitalSystemBlazor.Service;
using HospitalSystemBlazor.Entities.DTOs;
using HospitalSystemBlazor.Service.Interface;

namespace HospitalSystemBlazor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _service;
        private readonly IService<UsuarioDto> _userService;

        public AuthController(AuthService service, IService<UsuarioDto> userService)
        {
            _service = service;
            _userService = userService;
        }

        [HttpPost("login")]
        public  async Task<IActionResult> Login(LoginUser user)
        {
            var result = await _service.LoginUser(user);

            if (!result.isSucces)
            {
                return BadRequest(new { message = result.Error });
            }

            return Ok(new { result.isSucces, message = result.Value });
        }

        [HttpPost("createPaciente")]
        public async Task<IActionResult> Crear(UsuarioDto user)
        {
            var result = await _userService.Crear(user);

            if (!result.isSucces)
            {
                return BadRequest(new { message = result.Error });
            }
            return Ok(new { result.isSucces, message = result.Value });
        }
    }
}
