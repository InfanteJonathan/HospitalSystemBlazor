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
    public class PacientesService : IService<DetalleGeneralPaciente>
    {
        private readonly DataContext _contex;

        public PacientesService(DataContext contex)
        {
            _contex = contex;
        }
        public Result<List<DetalleGeneralPaciente>> Listas()
        {
            var lista = _contex.Pacientes
                .Where(p => p.Activo == true)
                .Select(p => new DetalleGeneralPaciente
                {
                    IdPaciente = p.IdPaciente,
                    IdUsuario = p.IdUsuario,
                    Nombre = p.Nombre,
                    Apellido = p.Apellido,
                    FechaNacimiento = p.FechaNacimiento,
                    Sexo = p.Sexo,
                    Direccion = p.Direccion,
                    NumContacto = p.NumContacto
                })
                .ToList();

            if(lista.Count == 0)
            {
                return Result<List<DetalleGeneralPaciente>>.Failure("Error, no se encontro una lista");
            }

            return Result<List<DetalleGeneralPaciente>>.Succes(lista);
        }
        public Result<DetalleGeneralPaciente> Detalles(int id)
        {
            var buscarPaciente = _contex.Pacientes
                .Where(p => p.IdPaciente == id)
                .Select(p => new DetalleGeneralPaciente
                {

                    IdPaciente = p.IdPaciente,
                    IdUsuario = p.IdUsuario,
                    Nombre = p.Nombre,
                    Apellido = p.Apellido,
                    FechaNacimiento = p.FechaNacimiento,
                    Sexo = p.Sexo,
                    Direccion = p.Direccion,
                    NumContacto = p.NumContacto
                })
                .FirstOrDefault();

            if(buscarPaciente == null)
            {
                return Result<DetalleGeneralPaciente>.Failure("Error, no se pudo encontrar el usuario");
            }

            return Result<DetalleGeneralPaciente>.Succes(buscarPaciente);
        }

        public Result<string> Crear(DetalleGeneralPaciente model)
        {
            throw new NotImplementedException();
        }


        public Result<string> Editar(int id, DetalleGeneralPaciente model)
        {
            var buscarPaciente = _contex.Pacientes
                .Where(p => p.IdPaciente == id)
                .FirstOrDefault();

            if (buscarPaciente == null)
            {
                return Result<string>.Failure("Error, no se pudo encontrar el paciente");
            }

            buscarPaciente.Nombre = model.Nombre;
            buscarPaciente.Apellido = model.Apellido;
            buscarPaciente.FechaNacimiento = model.FechaNacimiento;
            buscarPaciente.Sexo = model.Sexo;
            buscarPaciente.Direccion = model.Direccion;
            buscarPaciente.NumContacto = model.NumContacto;


            if(buscarPaciente == null)
            {
                return Result<string>.Failure("Error, no se pudo editar el paciente");
            }

            _contex.SaveChanges();

            return Result<string>.Succes("Paciente editado correctamente");

        }

        public Result<string> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        
    }
}
