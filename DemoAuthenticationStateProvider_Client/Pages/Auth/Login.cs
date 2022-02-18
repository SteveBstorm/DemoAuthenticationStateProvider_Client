using DemoAuthenticationStateProvider_Client.Models;
using Microsoft.AspNetCore.Components;
using System.Text;
using System.Text.Json;
using Microsoft.JSInterop;
using DemoAuthenticationStateProvider_Client.AuthProviders;
using Microsoft.AspNetCore.Components.Authorization;
using System.Diagnostics;

namespace DemoAuthenticationStateProvider_Client.Pages.Auth
{
    public partial class Login
    {
        
        
        public LoginForm MyFormProperties { get; set; }

        [Inject]
        public IJSRuntime _js { get; set; }
        

        [Inject]
        public IServiceProvider service { get; set; }

        public Login()
        {
            MyFormProperties = new LoginForm();
        }

        public async Task SubmitLogin()
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:44338/api/");
            string json = JsonSerializer.Serialize(MyFormProperties);
            HttpContent content = new StringContent(json,Encoding.UTF8, "application/json");
            
            using (HttpResponseMessage response = await Client.PostAsync("auth", content))
            {
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException(response.ReasonPhrase);

                string jsonResponse = response.Content.ReadAsStringAsync().Result;

                await _js.InvokeVoidAsync("localStorage.setItem","token", jsonResponse);

                ((TestAuthStateProvider)service.GetService<AuthenticationStateProvider>()).NotifyUserChanged();


            }
        }
    }
}
