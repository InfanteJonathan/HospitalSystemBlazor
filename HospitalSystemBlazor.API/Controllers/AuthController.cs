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


        public AuthController(AuthService service)
        {
            _service = service;
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

        
    }
}
