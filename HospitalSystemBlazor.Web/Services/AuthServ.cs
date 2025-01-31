using Blazored.LocalStorage;
using HospitalSystemBlazor.Entities.DTOs;
using HospitalSystemBlazor.Web.Utilities;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;

namespace HospitalSystemBlazor.Web.Services
{
    public class AuthServ
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;
        private readonly CustomAuthenticationStateProvider _authStateProvider;


        public AuthServ(HttpClient http, ILocalStorageService localStorage, CustomAuthenticationStateProvider authStateProvider)
        {
            _http = http;
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
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
                await _localStorage.SetItemAsync("token", resultContent.message);
                _authStateProvider.MarkUserAsAuthenticated(resultContent.message);
                //((CustomAuthenticationStateProvider)_authStateProvider).MarkUserAsAuthenticated(resultContent.message);
            }

            return new AuthResponse
            {
                isSucces = result.IsSuccessStatusCode,
                message = resultContent.message

            };

        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("token");
            _authStateProvider.MarkUserAsLoggedOut();
            //((CustomAuthenticationStateProvider)_authStateProvider).MarkUserAsLoggedOut();
        }

       

        
    }
}
