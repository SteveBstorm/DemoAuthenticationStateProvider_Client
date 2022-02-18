using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
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
        //public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        //{
        //    await Task.Delay(2000);

        //    List<Claim> claims = new List<Claim> {
        //        new Claim(ClaimTypes.Name, "Arthur Pendragon"),
        //        new Claim(ClaimTypes.Role, "Admin")
        //        };

        //    ClaimsIdentity anonymousUser = new ClaimsIdentity(claims, "TestAuthSystem");
        //    return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonymousUser)));
        //}

        //On ajoute la lecture du token après avoir créé le login component

        public IJSRuntime _js { get; set; }
        public TestAuthStateProvider(IJSRuntime js)
        {
            _js = js;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            List<Claim> claims = new List<Claim>();
            string token = await _js.InvokeAsync<string>("localStorage.getItem", "token");

            if (string.IsNullOrWhiteSpace(token))
            {
                ClaimsIdentity anonymousUser = new ClaimsIdentity();
                return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonymousUser)));
            }

            JwtSecurityToken jwt = new JwtSecurityToken(token);

            foreach (Claim claim in jwt.Claims)
            {
                claims.Add(claim);
            }

            ClaimsIdentity currentUser = new ClaimsIdentity(claims, "TestAuthSystem");

            //await _js.InvokeVoidAsync("localStorage.setItem", "name", claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value);

            return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(currentUser))).Result;

        }

        public void NotifyUserChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
