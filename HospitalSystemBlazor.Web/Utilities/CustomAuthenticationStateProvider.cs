using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace HospitalSystemBlazor.Web.Utilities
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;

        public CustomAuthenticationStateProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("token");
            var user = string.IsNullOrEmpty(token) ? new ClaimsPrincipal(new ClaimsIdentity())
                                                   : new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "Bearer"));

            return new AuthenticationState(user);
        }

        public void MarkUserAsAuthenticated(string token)
        {
            var authState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public void MarkUserAsLoggedOut()
        {
            var authState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var jsonToken = jwtHandler.ReadToken(jwt) as JwtSecurityToken;
            return jsonToken?.Claims ?? Enumerable.Empty<Claim>();
        }
    }
}
