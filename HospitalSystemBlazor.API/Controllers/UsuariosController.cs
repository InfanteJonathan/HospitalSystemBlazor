using HospitalSystemBlazor.Entities.DTOs;
using HospitalSystemBlazor.Service;
using HospitalSystemBlazor.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalSystemBlazor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IService<UsuarioDto> _userService;
        private readonly UsuarioService _user;

        public UsuariosController(IService<UsuarioDto> userService, UsuarioService user)
        {
            _userService = userService;
            _user = user;
        }

        [HttpGet]
        public  async Task<ActionResult<List<UsuarioDto>>> Index()
        {
            var result = await _userService.Listas();

            if (!result.isSucces)
            {
                return BadRequest(new { message = result.Error });
            }

            return Ok(new { result.Value });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDto>> Detalles(int id)
        {
            var result = await _userService.Detalles(id);

            if (!result.isSucces)
            {
                return BadRequest(new { message = result.Error });
            }

            return Ok(new { result.Value });
        }

        [HttpPost("crearPaciente")]
        public async Task<ActionResult> CrearPaciente(UsuarioDto model)
        {
            var result = await _userService.Crear(model);

            if (!result.isSucces)
            {
                return BadRequest(new { message = result.Error });
            }

            return Ok(new { result.Value });
        }

        [HttpPost("crearTrabajador")]
        public async Task<ActionResult> CrearTrabajador(UsuarioDto model)
        {
            var result = await _user.CrearTrabajador(model);

            if (!result.isSucces)
            {
                return BadRequest(new { message = result.Error });
            }

            return Ok(new { result.Value });
        }

        [HttpPost("Editar")]
        public async Task<ActionResult> CrearTrabajador(int id, UsuarioDto model)
        {
            var result = await _user.CrearTrabajador(model);

            if (!result.isSucces)
            {
                return BadRequest(new { message = result.Error });
            }

            return Ok(new { result.Value });
        }
    }
}
