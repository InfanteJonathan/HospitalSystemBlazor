using Blazored.LocalStorage;
using HospitalSystemBlazor.Entities.DTOs;
using HospitalSystemBlazor.Web.Utilities;
using System.Net.Http.Json;

namespace HospitalSystemBlazor.Web.Services
{
    public class AuthServ
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;

        public AuthServ(HttpClient http, ILocalStorageService localStorage)
        {
            _http = http;
            _localStorage = localStorage;
        }

        public async Task<AuthResponse> IniciarSesion(LoginUser model)
        {

            var result = await _http.PostAsJsonAsync("api/Auth/login", model);

            if (!result.IsSuccessStatusCode)
            {
                var errorContent = await result.Content.ReadAsStringAsync();
                throw new Exception($"Error en la API: {result.StatusCode}, Detalles: {errorContent}");
            }

            var resultContent = await result.Content.ReadFromJsonAsync<AuthResponse>();

            if (resultContent != null && !string.IsNullOrEmpty(resultContent.message))
            {
                await _localStorage.SetItemAsync("token", resultContent.message); // Almacena el token en localStorage
            }

            return new AuthResponse
            {
                isSucces = result.IsSuccessStatusCode,
                message = "Login Succesfull"

            };

        }

        public async Task<MensajeOperacion> GetHeaders()
        {
            var token = await _localStorage.GetItemAsync<string>("token");

            if (!string.IsNullOrEmpty(token))
            {
                _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            else
            {
                // Manejar el caso cuando no hay token disponible
                return new MensajeOperacion
                {
                    Exito = false,
                    Mensaje = "Token no encontrado"
                };
            }

            var result = await _http.GetAsync("api/Auth/headers");

            if (!result.IsSuccessStatusCode)
            {
                var errorContent = await result.Content.ReadAsStringAsync();
                throw new Exception($"Error en la API: {result.StatusCode}, Detalles: {errorContent}");

            }

            var mensaje = await result.Content.ReadAsStringAsync();

            return new MensajeOperacion
            {
                Exito = result.IsSuccessStatusCode,
                Mensaje = mensaje
            };

        }

        public async Task<T> GetTAsync<T>(string uri)
        {
            var token = await _localStorage.GetItemAsync<string>("token");

            if (!string.IsNullOrEmpty(token))
            {
                _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

             
            }

            var response = await _http.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("token");
        }
    }
}
