using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TimelessTapes.Models;

namespace TimelessTapes.Pages;

public class LoginModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public LoginModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [BindProperty]
    public string Email { get; set; }

    [BindProperty]
    public string Password { get; set; }

    public string Message { get; set; }


  

    public async Task<IActionResult> OnPostAsync()
    {
        

        var loginDTO = new
        {
            Email = this.Email,
            Password = this.Password
        };

        try
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:7027");

            var response = await client.PostAsJsonAsync("/api/user/login", loginDTO);

            if (response.IsSuccessStatusCode)
            {

                var user = await response.Content.ReadFromJsonAsync<UserLoginResponse>();


                Message = "Login successful!";
                HttpContext.Session.SetString("UserId", user.userId.ToString());
                HttpContext.Session.SetString("AccessType", user.accessType); // Should be "Admin" or "User"

                // Optionally redirect to another page
                // return RedirectToPage("/Home");
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                Message = $"Login failed: {error}";
            }
        }
        catch (Exception ex)
        {
            Message = "Error: " + ex.Message;
        }

        return Page();
    }
    
}
