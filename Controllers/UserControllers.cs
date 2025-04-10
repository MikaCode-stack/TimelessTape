using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimelessTapes.Data;
using TimelessTapes.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimelessTapes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DBHandler _context;

        public UserController(DBHandler context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string name, string email, string password, string accessType)
        {
            try
            {
                // Explicitly qualify the User.Register method call
                int userId = await TimelessTapes.Models.User.Register(_context, name, email, password, accessType);
                return Ok(new { UserId = userId, Message = "User registered successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("login")]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user != null && user.Login(email, password) != "Guest")
            {
                return Ok(new { AccessType = user.AccessType, Message = "Login successful." });
            }
            return BadRequest(new { Message = "Invalid credentials." });
        }

        [HttpPost("logout")]
        public IActionResult Logout(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user != null)
            {
                user.Logout();
                return Ok(new { Message = "Logout successful." });
            }
            return BadRequest(new { Message = "User not found." });
        }

        [HttpPost("admin/addvideo")]
        public async Task<IActionResult> AddVideo(string adminEmail, string title, string genre, decimal price)
        {
            var admin = _context.Users.OfType<Admin>().FirstOrDefault(u => u.Email == adminEmail);

            if (admin != null)
            {
                await admin.AddVideo(_context, title, genre, price);
                return Ok(new { Message = "Video added successfully." });
            }
            return BadRequest(new { Message = "Admin user not found or unauthorized." });
        }

        [HttpPost("admin/removevideo")]
        public async Task<IActionResult> RemoveVideo(string adminEmail, int videoId)
        {
            var admin = _context.Users.OfType<Admin>().FirstOrDefault(u => u.Email == adminEmail);

            if (admin != null)
            {
                try
                {
                    await admin.RemoveVideo(_context, videoId);
                    return Ok(new { Message = "Video removed successfully." });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { Message = ex.Message });
                }
            }
            return BadRequest(new { Message = "Admin user not found or unauthorized." });
        }

        [HttpGet("admin/customertransactions")]
        public IActionResult CustomersTransactions(string adminEmail)
        {
            var admin = _context.Users.OfType<Admin>().FirstOrDefault(u => u.Email == adminEmail);

            if (admin != null)
            {
                var transactions = admin.CustomersTransactions(_context);
                return Ok(transactions);
            }
            return BadRequest(new { Message = "Admin user not found or unauthorized." });
        }

        [HttpPost("customer/rentvideo")]
        public async Task<IActionResult> RentVideo(string customerEmail, int videoId)
        {
            var customer = _context.Users.OfType<Customer>().FirstOrDefault(u => u.Email == customerEmail);

            if (customer != null)
            {
                string result = await customer.RentVideo(_context, videoId);
                return Ok(new { Message = result });
            }
            return BadRequest(new { Message = "Customer user not found or unauthorized." });
        }

        [HttpPost("customer/returnvideo")]
        public async Task<IActionResult> ReturnVideo(string customerEmail, int videoId)
        {
            var customer = _context.Users.OfType<Customer>().FirstOrDefault(u => u.Email == customerEmail);

            if (customer != null)
            {
                string result = await customer.ReturnVideo(_context, videoId);
                return Ok(new { Message = result });
            }
            return BadRequest(new { Message = "Customer user not found or unauthorized." });
        }

        [HttpGet("customer/mytransactions")]
        public IActionResult MyTransactions(string customerEmail)
        {
            var customer = _context.Users.OfType<Customer>().FirstOrDefault(u => u.Email == customerEmail);

            if (customer != null)
            {
                var transactions = customer.MyTransactions(_context);
                return Ok(transactions);
            }
            return BadRequest(new { Message = "Customer user not found or unauthorized." });
        }
    }
}
