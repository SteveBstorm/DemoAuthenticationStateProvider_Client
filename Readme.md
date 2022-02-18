Demo AuthenticationStateProvider ClientSide

1) Ajouter le nuget : Microsoft.AspNetCore.Components.WebAssembly.Authentication


2) Créer une classe : AuthProvider qui hérite de AuthenticationStateProvider
	//fournit l'état de l'utilisateur courant
	//3états possible : Authorized / Non-Authorized / Authorizing
	
	=> override GetAuthenticationStateAsync()
	 	```
	ClaimsIdentity anonymousUser = new ClaimsIdentity();
    return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonymousUser)));
```
3) Program.cs =>
	
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, TestAuthStateProvider>();

4) Modifier app.blazor
=> englober par <CascadingAuthenticationState> pour rendre le state disponible partout dans l'app
=>
```
<Found Context="routeData">
            @*<RouteView RouteData="@routeData" DefaultLayout="@typeof(MainLayout)" />
                <FocusOnNavigate RouteData="@routeData" Selector="h1" />*@
            <AuthorizeRouteView RouteData="@routeData" DefaultLayout="@typeof(MainLayout)" >
                <Authorizing>
                    <text>Authorisation en cours, veuillez patienter...</text>
                </Authorizing>
            </AuthorizeRouteView>

        </Found>
```
5) Ajouter @using Microsoft.AspNetCore.Components.Authorization
	dans _Imports.blazor

	Permet d'utiliser le composant <AuthorizeView> dans les composants de l'app

	(Voir : counter.Razor)

6) Modifier TestAuthStateProvider => voir commentaire dans la démo

7) Retourner sur Counter.Razor (ajout @using System.Security.Claims)
	@context.User.FindFirst(ClaimTypes.Name).Value

8) Lancer l'api https://github.com/SteveBstorm/DemoAuthAPI

9) Modifier le Provider et mettre la 3 ème version de la méthode GetAuthenticationStateAsync()
    => ne pas oublier d'injecter IJSRuntime
    => Créer une méthode NotifyUserChanged pour call L'event NotifyAuthenticationStateChanged() 

10) Créer le composant Login.razor + Login.cs

11) Ajouter dans index.razor le <AuthorizeView> sans prise en charge de role

12) faire la démo avec 2 comptes différents sur l'api pour montrer le comportement