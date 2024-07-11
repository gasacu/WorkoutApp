using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;
using WorkoutApp.DTOs;
using WorkoutApp.Entities;

namespace WorkoutApp.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        
		// ProtectedSessionStorage for storing user session data securely in the browser.
		private readonly ProtectedSessionStorage _sessionStorage;

        // _anonymous for unautheticated user. a "claim" is a piece of information about a user or system entity.
        // ClaimsPrincipal is used to represent an anonymous (unauthenticated) user, and designed to work with claims-based identity systems
        // A ClaimsPrincipal can be composed of multiple ClaimsIdentity instances. 
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(ProtectedSessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                await Task.Delay(500);
                var userSessionStorageResult = await _sessionStorage.GetAsync<User>("UserSession");
                var userSession = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;
                if (userSession == null)
                    return await Task.FromResult(new AuthenticationState(_anonymous));
                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userSession.Email),
                    new Claim(ClaimTypes.Role, userSession.IsTrainer ? "Administrator" : "User")
                }, "CustomAuth"));
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }

        public void UpdateAuthenticationState(User userSession)
        {
            ClaimsPrincipal claimsPrincipal;

            if (userSession != null)
            {
                _sessionStorage.SetAsync("UserSession", userSession);
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userSession.Email),
                    new Claim(ClaimTypes.Role, userSession.IsTrainer ? "Administrator" : "User")
                }));
            }
            else
            {
                _sessionStorage.DeleteAsync("UserSession");
                claimsPrincipal = _anonymous;
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
        

        public async Task UnsetUserAsync()
        {
            await _sessionStorage.DeleteAsync("UserSession");
            var claimsPrincipal = _anonymous;
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

    }
}

