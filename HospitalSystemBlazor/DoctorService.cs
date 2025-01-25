using HospitalSystemBlazor.Data;
using HospitalSystemBlazor.Entities.DTOs;
using HospitalSystemBlazor.Entities.Models;
using HospitalSystemBlazor.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSystemBlazor.Service
{
    public class DoctorService : IService<DetalleGeneralDoctor>
    {
        private readonly DataContext _context;

        public DoctorService(DataContext context)
        {
            _context = context;
        }

        public async Task<Result<List<DetalleGeneralDoctor>>> Listas()
        {
            var doctores = await _context.Doctores
                .Include(e => e.Especialidad)
                .Where(d => d.Activo)
                .Select(d => new DetalleGeneralDoctor
                {
                    IdDoctor = d.IdDoctor,
                    IdUsuario = d.IdUsuario,
                    IdEspecialidad = d.Especialidad.Nombre,
                    Nombre = d.Nombre,
                    Correo = d.Correo,
                    Telefono = d.Telefono                 

                })
                .ToListAsync();

            if(doctores.Count == 0)
            {
                return Result<List<DetalleGeneralDoctor>>.Failure("Error, no se pudo obtener la lista");
            }

            return Result<List<DetalleGeneralDoctor>>.Succes(doctores);
        }
        public  async Task<Result<DetalleGeneralDoctor>> Detalles(int id)
        {
            var doctores = await _context.Doctores
                .Include(e => e.Especialidad)
                .Where(d => d.Activo &&  d.IdDoctor == id)
                .Select(d => new DetalleGeneralDoctor
                {
                    IdDoctor = d.IdDoctor,
                    IdUsuario = d.IdUsuario,
                    IdEspecialidad = d.Especialidad.Nombre,
                    Nombre = d.Nombre,
                    Correo = d.Correo,
                    Telefono = d.Telefono

                })
                .FirstOrDefaultAsync();

            if (doctores == null)
            {
                return Result<DetalleGeneralDoctor>.Failure("Error, no se pudo obtener la lista");
            }

            return Result<DetalleGeneralDoctor>.Succes(doctores);
        }
        public Task<Result<string>> Crear(DetalleGeneralDoctor model)
        {
            throw new NotImplementedException();
        }


        public async Task<Result<string>> Editar(int id, DetalleGeneralDoctor model)
        {
            var buscarDoctor = await _context.Doctores
                .FirstOrDefaultAsync(d => d.IdDoctor == id);

            if(buscarDoctor == null)
            {
                return Result<string>.Failure("Error, no se pudo encontrar al doctor");
            }

            var BuscarIdEspecialidad = await _context.Especialidades
                .Where(e => e.Nombre.Equals(model.IdEspecialidad))
                .Select(e => e.IdEspecialidad)
                .FirstOrDefaultAsync();

            var doct = new Doctor();
            doct.IdEspecialidad = BuscarIdEspecialidad;
            doct.Nombre = model.Nombre;
            doct.Correo = model.Correo;
            doct.Telefono = model.Telefono;
            doct.Activo = model.Activo;
            doct.FechaEdicion = DateTime.Now;

            if (doct == null)
            {
                return Result<string>.Failure("Error, no se pudo registrar los datos");
            }

            await _context.SaveChangesAsync();

            return Result<string>.Succes("Datos guardados correctamente...");

        }

        public  Task<Result<string>> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

    }
}
