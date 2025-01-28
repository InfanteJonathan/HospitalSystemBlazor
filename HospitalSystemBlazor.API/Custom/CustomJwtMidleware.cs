//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Text;

//namespace HospitalSystemBlazor.API.Custom
//{
//    public class CustomJwtMidleware
//    {
//        private readonly RequestDelegate _next;

//        public CustomJwtMidleware(RequestDelegate next)
//        {
//            _next = next;
//        }

//        public async Task Invoke(HttpContext context)
//        {
//            var token = context.Request.Headers["Authorization"].FirstOrDefault();

//            if (token != null)
//            {
//                AttachUserToContext(context, token);
//            }

//            await _next(context);
//        }

//        private void AttachUserToContext(HttpContext context, string token)
//        {
//            try
//            {
//                var tokenHandler = new JwtSecurityTokenHandler();
//                var key = Encoding.UTF8.GetBytes("MiClaveDeSeguridadEsLaMejorYmasSeguraDelMundoPorFavorEvitarUsarla");
//                tokenHandler.ValidateToken(token, new TokenValidationParameters
//                {
//                    ValidateIssuerSigningKey = true,
//                    IssuerSigningKey = new SymmetricSecurityKey(key),
//                    ValidateIssuer = true,
//                    ValidIssuer = "SUPERKEYCLAVESSS",
//                    ValidateAudience = true,
//                    ValidAudience = "SUPERKEYCLAVESSS",
//                    ValidateLifetime = true,
//                }, out SecurityToken validatedToken);

//                var jwtToken = (JwtSecurityToken)validatedToken;
//                var userId = jwtToken.Claims.First(x => x.Type == "UserId").Value;

//                // Adjuntar la identidad del usuario al contexto
//                context.Items["User"] = userId;
//            }
//            catch
//            {
//                // No hacer nada si la validación falla
//            }

//        }
//    }

//}