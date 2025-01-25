using HospitalSystemBlazor.Data;
using HospitalSystemBlazor.Entities.DTOs;
using HospitalSystemBlazor.Entities.Models;
using HospitalSystemBlazor.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystemBlazor.Service
{
    public class UsuarioService : IService<UsuarioDto>
    {
        private readonly DataContext _context;
        public static Usuario user = new();

        public UsuarioService(DataContext context)
        {
            _context = context;
        }

        public async Task<Result<List<UsuarioDto>>> Listas()
        {
            var lista = await _context.Usuarios
                .Include(r => r.Roles)
                .Where(u => u.Activo == true)
                .Select(u => new UsuarioDto
                {
                    Email = u.Email,
                    Contraseña = u.Contraseña,
                    IdRol = u.Roles.Nombre                  
                   
                })
                .ToListAsync();

            if(lista.Count != 0)
            {
                return Result<List<UsuarioDto>>.Succes(lista);
            }

            return Result<List<UsuarioDto>>.Failure("Error, no se pudo obtener la lista de usuarios");
        }
        public async Task<Result<UsuarioDto>> Detalles(int id)
        {
            if(id == 0)
            {
                return Result<UsuarioDto>.Failure("Error, el identificador es incorrecto");
            }

            var usuario = await _context.Usuarios
                .Include(r => r.Roles)
                .Select(u => new UsuarioDto
                {
                    Email = u.Email,
                    Contraseña = u.Contraseña,
                    IdRol = u.Roles.Nombre
                })
                .FirstOrDefaultAsync(u => u.IdUsuario == id);

            if(usuario != null)
            {
                return Result<UsuarioDto>.Succes(usuario);
            }

            return Result<UsuarioDto>.Failure("Error, no se encontro el usuario");
        }

        private string HashPassword(string model)
        {
            var hashedPassword = new PasswordHasher<Usuario>()
                .HashPassword(user, model);

            return hashedPassword;
        }

        public async Task<Result<string>> Crear(UsuarioDto model)
        {
            if(string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Contraseña))
            {
                return Result<string>.Failure("Error");
            }

            

            var paciente = new Paciente();

            var nuevoUsuario = new Usuario();
            nuevoUsuario.Email = model.Email;
            nuevoUsuario.Contraseña = HashPassword(model.Contraseña);
            nuevoUsuario.IdRol = 3;
            nuevoUsuario.Activo = true;
            nuevoUsuario.FechaRegistro = DateTime.Now;


            if(nuevoUsuario != null)
            {
                _context.Usuarios.Add(nuevoUsuario);
                await _context.SaveChangesAsync();

                paciente.IdUsuario = nuevoUsuario.IdUsuario;
                paciente.Activo = true;
                paciente.FechaRegistro = DateTime.Now;

                _context.Pacientes.Add(paciente);

                await _context.SaveChangesAsync();
                return Result<string>.Succes("Usuario creado con  Exito ");
            }

            return Result<string>.Failure("Error, no se pudo crear el usuario");

        }

        public async Task<Result<string>> CrearTrabajador(UsuarioDto model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Contraseña))
            {
                return Result<string>.Failure("Error");
            }

            var hashedPassword = new PasswordHasher<Usuario>()
                .HashPassword(user, model.Contraseña);

            var buscarIdRol = await _context.Roles
                .Where(r => r.Nombre.Equals(model.IdRol))
                .Select(r => r.IdRol)
                .FirstOrDefaultAsync();

            var doctor = new Doctor();

            var nuevoUsuario = new Usuario();
            nuevoUsuario.Email = model.Email;
            nuevoUsuario.Contraseña = hashedPassword;
            nuevoUsuario.IdRol = 2;
            nuevoUsuario.Activo = true;
            nuevoUsuario.FechaRegistro = DateTime.Now;


            if (nuevoUsuario != null)
            {
                _context.Usuarios.Add(nuevoUsuario);

                doctor.IdUsuario = nuevoUsuario.IdUsuario;
                doctor.Activo = true;
                doctor.FechaRegistro = DateTime.Now;

                _context.Doctores.Add(doctor);

                await _context.SaveChangesAsync();
                return Result<string>.Succes("Usuario creado con  Exito ");
            }

            return Result<string>.Failure("Error, no se pudo crear el usuario");
        }


        public async Task<Result<string>> Editar(int id, UsuarioDto model)
        {
            if(string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Contraseña))
            {
                return Result<string>.Failure("Error, ingrese datos válidos");
            }

            var buscarUsuario = await _context.Usuarios
                .Where(u => u.IdUsuario == id)
                .FirstOrDefaultAsync();

            if(buscarUsuario != null)
            {
                buscarUsuario.Email = model.Email;
                buscarUsuario.Contraseña = model.Contraseña;
                buscarUsuario.Activo = model.Activo?? true;

                _context.Usuarios.Add(buscarUsuario);
                await _context.SaveChangesAsync();

                return Result<string>.Succes("Usuario registrado correctamente");
            }

            return Result<string>.Failure("Error, no se pudo registrar al usuario");

        }

        public async Task<Result<string>> Eliminar(int id)
        {
            var buscarUsuario = await _context.Usuarios
                .Include(u => u.Paciente)
                .Include(u => u.Doctor)
                .FirstOrDefaultAsync(u => u.IdUsuario == id);

            if (buscarUsuario == null)
            {
                return Result<string>.Failure("Error, usuario no encontrado");
            }

            if (buscarUsuario.IdRol == 3) // Si es Paciente
            {
                if (buscarUsuario.Paciente != null)
                {
                    buscarUsuario.Paciente.Activo = false;
                    buscarUsuario.Paciente.FechaEliminacion = DateTime.Now;
                }
            }
            else // Si es Doctor
            {
                if (buscarUsuario.Doctor != null)
                {
                    buscarUsuario.Doctor.Activo = false;
                    buscarUsuario.Doctor.FechaEliminacion = DateTime.Now;
                }
            }

            buscarUsuario.Activo = false;
            buscarUsuario.FechaEliminacion = DateTime.Now;

            await _context.SaveChangesAsync();

            return Result<string>.Succes("Usuario eliminado correctamente");
        }

    }
}
