using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace TimelessTapes.Pages;
public class RegisterModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public RegisterModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [BindProperty]
    public string Name { get; set; }
    [BindProperty]
    public string Email { get; set; }
    [BindProperty]
    public string Password { get; set; }

    public string Message { get; set; }

        public async Task<IActionResult> OnPostAsync()
    {
        

        var user = new
        {
            Name = this.Name,
            Email = this.Email,
            Password = this.Password
        };

        using var client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7027");

        var response = await client.PostAsJsonAsync("/api/user/register", user);

        if (response.IsSuccessStatusCode)
        {
            Message = "Registration successful!";
        }
        else
        {
            Message = "Registration failed: " + await response.Content.ReadAsStringAsync();
        }

        return Page();
    }
}
