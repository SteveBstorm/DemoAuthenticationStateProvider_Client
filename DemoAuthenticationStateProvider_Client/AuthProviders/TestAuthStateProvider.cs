using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace DemoAuthenticationStateProvider_Client.AuthProviders
{
    public class TestAuthStateProvider : AuthenticationStateProvider
    {
        /*
         * On débute la démo avec un utilisateur anonyme, d'ou le constructeur sans param de ClaimsIdentity 
         * On y reviendra plus tard pour modifier le type d'authentification
         */

        //public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        //{
        //    await Task.Delay(2000);

        //    ClaimsIdentity anonymousUser = new ClaimsIdentity();
        //    return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonymousUser)));
        //}

        //Après l'étape 5 on commente la méthode et on recommence avec un vrai user
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await Task.Delay(2000);

            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name, "Arthur Pendragon"),
                new Claim(ClaimTypes.Role, "Admin")
                };

            ClaimsIdentity anonymousUser = new ClaimsIdentity(claims, "TestAuthSystem");
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonymousUser)));
        }
    }
}
