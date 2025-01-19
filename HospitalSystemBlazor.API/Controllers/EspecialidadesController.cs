using HospitalSystemBlazor.Entities.DTOs;
using HospitalSystemBlazor.Service;
using HospitalSystemBlazor.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalSystemBlazor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadesController : ControllerBase
    {
        private readonly IService<EspecialidadDTO> _service;

        public EspecialidadesController(IService<EspecialidadDTO> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<EspecialidadDTO>>> Index()
        {
            var result = await _service.ListaEspecialidades();

            if (!result.isSucces)
            {
                return BadRequest(new { message = result.Error });
            }

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EspecialidadDTO>> DetalleEspecialidad(int id)
        {
            var result = await _service.Detalles(id);

            if (!result.isSucces)
            {
                return BadRequest(new { message = result.Error });
            }

            return Ok(result.Value);
        }

        [HttpPost("NuevaEsp")]
        public async Task<ActionResult> Crear(EspecialidadDTO model)
        {
            var result = await _service.Crear(model);

            if (!result.isSucces)
            {
                return BadRequest(new { message = result.Error });
            }

            return Ok(new { result.isSucces, message = result.Value });
        }

        [HttpPut("Actualizar/{id}")]
        public async Task<ActionResult> Editar(int id, EspecialidadDTO model)
        {
            var result = await _service.Editar(id, model);

            if (!result.isSucces)
            {
                return BadRequest(new { message = result.Error });
            }

            return Ok(new { result.isSucces, message = result.Value });
        }

        [HttpDelete("Eliminar/{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var result = await _service.Eliminar(id);

            if (!result.isSucces)
            {
                return BadRequest(new { message = result.Error });
            }

            return Ok(new { result.isSucces, result.Value });
        }
    }
}
