using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimelessTapes.Data;
using System.Linq;

namespace TimelessTapes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideosController : ControllerBase
    {
        private readonly DBHandler _context;

        public VideosController(DBHandler context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetTopVideos([FromQuery] string search = "", [FromQuery] string sortBy = "Title")
        {
            // Using HashSet to store video titles for fast searching
            var videoTitlesHashSet = new HashSet<string>();

            var videosQuery = _context.Titles.AsQueryable();

            // Search logic
            if (!string.IsNullOrEmpty(search))
            {
                videosQuery = videosQuery.Where(v => v.PrimaryTitle.Contains(search));
            }

            // Sorting logic (default sort is by Title)
            if (sortBy.ToLower() == "price")
            {
                videosQuery = videosQuery.OrderBy(v => v.Price);
            }
            else
            {
                videosQuery = videosQuery.OrderBy(v => v.PrimaryTitle);
            }

            // Top 10 Videos
            var topVideos = await videosQuery
                .Take(10)
                .Select(v => new
                {
                    v.TitleId,
                    v.PrimaryTitle,
                    v.Genres,
                    v.ReleaseYear,
                    v.Price
                })
                .ToListAsync();

            // Populate the HashSet with video titles for fast lookups
            foreach (var video in topVideos)
            {
                videoTitlesHashSet.Add(video.PrimaryTitle);
            }

            return Ok(new { videos = topVideos, videoTitlesHashSet });
        }
    }
}
