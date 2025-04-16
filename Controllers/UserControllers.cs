using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimelessTapes.Data;
using TimelessTapes.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;

//Controller for user registration and login
namespace TimelessTapes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DBHandler _context;
        public UserController(DBHandler context)
        {
            _context = context;
        }
        // POST: api/user/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationDTO model)
        {
            // Validate the incoming model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Check if the email already exists
            if (await _context.Users.AnyAsync(u => u.Email == model.Email))
                return BadRequest("Email is already in use.");
            // Create password hash and salt as this is a secure way to store passwords in the database
            PasswordHasher.CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);
            // Create user entity
            var customer = new Customer
            {
                Name = model.Name,
                Email = model.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                CreatedAt = DateTime.UtcNow,
               
            };
            // Add user to the database
            _context.Users.Add(customer);
            await _context.SaveChangesAsync();
            // For security reasons,cpassword hash and salt are not returned, only confirmation message is returned
            return Ok(new { Message = "User registered successfully." });
        }

        [AllowAnonymous]
        // POST: api/user/login => Allows user to login and save session data for the logged-in user
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            // Validate the incoming model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Retrieve the user by email
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null)
                return Unauthorized("Invalid email or password.");
            // Verify the password
            if (!PasswordHasher.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
                return Unauthorized("Invalid email or password.");
            
            HttpContext.Session.SetString("UserId", user.UserId.ToString());
            HttpContext.Session.SetString("AccessType", user.AccessType);


            return Ok(new
            {
                Message = "User logged in successfully.",
                UserId = user.UserId,
                AccessType = user.AccessType
            });

        }

        // POST: api/user/logout => Allows user to logout and clear session data
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Ok("Logged out successfully.");
        }

    }
}
