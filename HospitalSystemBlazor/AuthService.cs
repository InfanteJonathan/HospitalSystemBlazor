using HospitalSystemBlazor.Data;
using HospitalSystemBlazor.Entities.DTOs;
using HospitalSystemBlazor.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HospitalSystemBlazor.Service
{
    public class AuthService
    {
        private readonly DataContext _context;
        public static Usuario user;

        public AuthService(DataContext context)
        {
            _context = context;
        }

        public async Task<Result<string>> LoginUser(LoginUser model)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email.Equals(model.Email));

            if (usuario == null)
            {
                return Result<string>.Failure("Email Incorrecto!");
            }

            var passwordHasher = new PasswordHasher<Usuario>();
            var verificationResult = passwordHasher.VerifyHashedPassword(usuario, usuario.Contraseña, model.Password);

            if (verificationResult == PasswordVerificationResult.Failed)
            {
                return Result<string>.Failure("Contraseña Incorrecta");
            }

            var token = CreateToken(usuario);
            Console.WriteLine(token);
            return Result<string>.Succes(token);
        }

        private string CreateToken(Usuario user)
        {
            var buscarNombreRol = _context.Roles
                .Where(r => r.IdRol == user.IdRol)
                .Select(r => r.Nombre)
                .First();

            if(buscarNombreRol == null)
            {
                return null;
            }

            var claims = new List<Claim>
            {
                new Claim("UserId", user.IdUsuario.ToString()),
                new Claim("Email", user.Email),
                new Claim(ClaimTypes.Role , buscarNombreRol)
            };

            var handler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MiClaveDeSeguridadEsLaMejorYmasSeguraDelMundoPorFavorEvitarUsarla"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new JwtSecurityToken
            (
                issuer: "SUPERKEYCLAVESSS",
                audience: "SUPERKEYCLAVESSS",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(5),
                signingCredentials: creds
            );

            return handler.WriteToken(tokenDescriptor);
        }

    }
}
