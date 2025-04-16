using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TimelessTapes.Data;
using TimelessTapes.Models;
//Admin Actions => Adding a new video, removing a video
namespace TimelessTapes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminActionController : ControllerBase
    {
        private readonly DBHandler _context;
        public AdminActionController(DBHandler context)
        {
            _context = context;
        }
        // POST: api/adminaction/add-video Allows admin to upload a new video
        [HttpPost("AddVideo")]
        public async Task<IActionResult> AddVideo([FromBody] AddVideoDTO videoDto)
        {
            var userId = HttpContext.Session.GetString("UserId");
            var accessType = HttpContext.Session.GetString("AccessType");

            if (string.IsNullOrEmpty(userId) || accessType != "Admin")
            {
                return StatusCode(403, "Only admins can perform this action.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var newVideo = new Title
                {
                    TitleId = videoDto.TitleId,
                    TitleType = videoDto.TitleType,
                    PrimaryTitle = videoDto.PrimaryTitle,
                    Director = videoDto.Director,
                    Cast = videoDto.Cast,
                    ReleaseYear = videoDto.ReleaseYear,
                    Rating = videoDto.Rating,
                    Duration = videoDto.Duration,
                    Genres = videoDto.Genres,
                    Description = videoDto.Description,
                    Price = videoDto.Price,
                    Copies = videoDto.Copies
                };

                _context.Titles.Add(newVideo);
                await _context.SaveChangesAsync();

                

                // Log the actions admin during admin session
                var log = new AdminLog
                {
                    AdminId = int.Parse(userId),
                    Action = $"Added video '{newVideo.PrimaryTitle}' (ID: {newVideo.TitleId})",
                    ActionDate = DateTime.UtcNow,
                    IsActive = true
                };

                _context.AdminLogs.Add(log);
                await _context.SaveChangesAsync();
                return Ok("Video added successfully.");

            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to add video: {ex.Message}");
            }
        }


        //Allows admin to remove a video from the database/system
        [HttpDelete("remove-video")]
        public async Task<IActionResult> RemoveVideo([FromBody] RemoveVideoDTO dto)
        {
            var userId = HttpContext.Session.GetString("UserId");
            var accessType = HttpContext.Session.GetString("AccessType");

            /*if (string.IsNullOrEmpty(userId) || accessType != "Admin")
            {
                return StatusCode(403, "Only admins can perform this action.");
            }*/

            var video = await _context.Titles
                .FirstOrDefaultAsync(t => t.TitleId == dto.TitleId && t.PrimaryTitle == dto.PrimaryTitle);

            if (video == null)
            {
                return NotFound("Video not found.");
            }

            _context.Titles.Remove(video);

            var log = new AdminLog
            {
                AdminId = int.Parse(userId),
                Action = $"Removed video '{video.PrimaryTitle}' (ID: {video.TitleId})",
                ActionDate = DateTime.UtcNow,
                IsActive = true
            };

            //logs admin action again
            _context.AdminLogs.Add(log);

            await _context.SaveChangesAsync();

            return Ok("Video removed.");
        }

    }
}
