using HospitalSystemBlazor.Data;
using HospitalSystemBlazor.Entities.DTOs;
using HospitalSystemBlazor.Entities.Models;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<string>> LoginUser(LoginUser model)
        {
            var usuario = await _context.Usuarios
                .FirstAsync(u => u.Email.Equals(model.Email));

            if (usuario == null)
            {
                return Result<string>.Failure("Email Incorrecto!");
            }

            if (usuario.Contraseña.Equals(HashPassword(model.Password)))
            {
                return Result<string>.Failure("Contraseña Incorrecta");
            }

            var (userid, email) = ObtenerHeaders();

            string token = CreateToken(usuario);
            return Result<string>.Succes(token);
        }


        private string HashPassword(string model)
        {
            var hashedPassword = new PasswordHasher<Usuario>()
                .HashPassword(user, model);

            return hashedPassword;
        }

        private string CreateToken(Usuario user)
        {
            var claims = new List<Claim>
            {
                new Claim("UserId", user.IdUsuario.ToString()),
                new Claim("Email", user.Email)
            };

            var handler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YhT7y!mL9g$y5rD3qNpX9wA@tK6vZ4h*F8uJ2pL1xC7bV9kQ3nR6tM5wU8oP2y"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            var tokenDescriptor = new JwtSecurityToken
            (
                issuer: "SUPERKEYCLAVESSS",
                audience: "SUPERKEYCLAVESSS",
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return handler.WriteToken(tokenDescriptor);

        }

        public (string? UserId, string? Email) ObtenerHeaders()
        {
            var userid = _httpContextAccessor.HttpContext.Request.Headers["UserId"].ToString();
            var email = _httpContextAccessor.HttpContext.Request.Headers["Email"].ToString();



            if (string.IsNullOrEmpty(userid) || string.IsNullOrEmpty(email))
            {
                throw new Exception("No se encontraron los headers");
            }

            return (userid, email);
        }
    }
}
