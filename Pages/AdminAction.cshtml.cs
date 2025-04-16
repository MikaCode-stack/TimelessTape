using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Json;
using TimelessTapes.Models;

namespace TimelessTapes.Pages
{
    public class AdminActionModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminActionModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public AddVideoDTO AddVideo { get; set; }

        [BindProperty]
        public RemoveVideoDTO RemoveVideo { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {
            var userId = HttpContext.Session.GetString("UserId");
            var accessType = HttpContext.Session.GetString("AccessType");

            if (string.IsNullOrEmpty(userId) || accessType != "Admin")
            {
                Response.Redirect("/Login");
            }
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:7027");

            var response = await client.PostAsJsonAsync("/api/adminaction/AddVideo", AddVideo);

            if (response.IsSuccessStatusCode)
            {
                Message = "Video added successfully!";
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                Message = $"Error adding video: {error}";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostRemoveAsync()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:7027");

            var response = await client.SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("/api/adminaction/remove-video", UriKind.Relative),
                Content = JsonContent.Create(RemoveVideo)
            });

            if (response.IsSuccessStatusCode)
            {
                Message = "Video removed successfully!";
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                Message = $"Error removing video: {error}";
            }

            return Page();
        }
    }
}
