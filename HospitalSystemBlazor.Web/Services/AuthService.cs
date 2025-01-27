using HospitalSystemBlazor.Entities.DTOs;

namespace HospitalSystemBlazor.Web.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;

        public AuthService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<UsuarioDto>> ListaUsuarios()
        {
            var result = _http.GetAsync("");
        }
    }
}
