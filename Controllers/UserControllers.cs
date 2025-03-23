using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimelessTapes.Data;
using TimelessTapes.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimelessTapes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DBHandler _context;

        public UsersController(DBHandler context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
