using HospitalSystemBlazor.Data;
using HospitalSystemBlazor.Entities.DTOs;
using HospitalSystemBlazor.Entities.Models;
using HospitalSystemBlazor.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystemBlazor.Service
{
    public class EspecialidadService : IService<EspecialidadDTO>
    {
        public readonly DataContext _context;

        public EspecialidadService(DataContext context)
        {
            _context = context;
        }

        public async Task<Result<List<EspecialidadDTO>>> ListaEspecialidades()
        {
            List<EspecialidadDTO> lista = await _context.Especialidades
                .Where(x => x.Activo)
                .Select(e => new EspecialidadDTO
                {
                    IdEspecialidad = e.IdEspecialidad,
                    Nombre = e.Nombre,
                    Precio = e.Precio,
                    Descripcion = e.Descripcion,
                    Activo = e.Activo

                }).ToListAsync();

            if(!lista.Any())
            {
                return Result<List<EspecialidadDTO>>.Failure("Error, no se encontro la lista de datos");
            }

            return Result<List<EspecialidadDTO>>.Succes(lista);
        }

        public async Task<Result<EspecialidadDTO>> Detalles(int id)
        {
            var espec = await _context.Especialidades
                .Select(e => new EspecialidadDTO
                {
                    IdEspecialidad = e.IdEspecialidad,
                    Nombre = e.Nombre,
                    Precio = e.Precio,
                    Descripcion = e.Descripcion,
                    Activo = e.Activo

                })
                .FirstOrDefaultAsync(x => x.IdEspecialidad == id);

            if(espec == null)
            {
                return Result<EspecialidadDTO>.Failure("Error, no se encontro el dato");
            }
                

            return Result<EspecialidadDTO>.Succes(espec);
        }

        public async Task<Result<string>> Crear(EspecialidadDTO model)
        {
            var nuevaEspecialidad = new Especialidad();

            if(string.IsNullOrEmpty(model.Nombre) || model.Precio == 0)
            {
                return Result<string>.Failure("El nombre y/o precio no pueden ser nulos o vacios"); // EN EL RETUNR DE ERRORES SE CAMBIARA POR RESULT PATERN
            }

            nuevaEspecialidad.Nombre = model.Nombre;
            nuevaEspecialidad.Descripcion = model.Descripcion ?? string.Empty;
            nuevaEspecialidad.Precio = model.Precio;
            nuevaEspecialidad.FechaRegistro = DateTime.Now;
            nuevaEspecialidad.Activo = true;

            _context.Especialidades.Add(nuevaEspecialidad);
            await _context.SaveChangesAsync();

            return Result<string>.Succes("Dato creado con exito");
        }


        public async Task<Result<string>> Editar(int id, EspecialidadDTO model)
        {
            if (id == 0)
            {                
                Result<string>.Failure("Error, no existe el dato");
            }

            if (string.IsNullOrEmpty(model.Nombre) || model.Precio == 0)
            {
                Result<string>.Failure("Error, datos incorrectos"); 
            }

            var especialidad = await _context.Especialidades
                .FirstOrDefaultAsync(x => x.IdEspecialidad == id);

            especialidad.Nombre = model.Nombre;
            especialidad.Descripcion = model.Descripcion;
            especialidad.Precio = model.Precio;
            especialidad.Activo = model.Activo;
            especialidad.FechaEdicion = DateTime.Now;

            await _context.SaveChangesAsync();

            return Result<string>.Succes("Data actualizado con exito");
        }

        public async Task<Result<string>> Eliminar(int id)
        {
            var espec = await _context.Especialidades
                .FirstOrDefaultAsync(e => e.IdEspecialidad == id);

            if(espec == null)
            {
                return Result<string>.Failure("El dato a eliminar no existe");
            }

            espec.Activo = false;
            espec.FechaEliminacion = DateTime.Now;

            return Result<string>.Succes("Dato eliminado exitosamente");

        }
    }
}
