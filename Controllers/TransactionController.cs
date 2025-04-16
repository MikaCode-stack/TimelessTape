using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimelessTapes.Data;
using TimelessTapes.Models;
using TimelessTapes.Services;

namespace TimelessTapes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly DBHandler _context;
        private readonly TransactionService _transactionService;

        // Injecting both DBHandler and TransactionService into the controller
        public TransactionsController(DBHandler context, TransactionService transactionService)
        {
            _context = context;
            _transactionService = transactionService;
        }

        // Rent video endpoint
        [HttpPost("rent")]
        public async Task<IActionResult> RentVideo([FromBody] RentRequest request)
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr)) return Unauthorized("Login required");

            int userId = int.Parse(userIdStr);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null) return NotFound("User not found.");

            var resultMessage = await _transactionService.RentVideoAsync(_context, user, request.TitleId);
            return Ok(new { message = resultMessage });
        }

        // Return video endpoint
        [HttpPost("return")]
        public async Task<IActionResult> ReturnVideo([FromBody] ReturnRequest request)
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr)) return Unauthorized("Login required");

            int userId = int.Parse(userIdStr);
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return NotFound("User not found.");

            var result = await _transactionService.ReturnVideoAsync(_context, user, request.TitleId);
            return Ok(new { message = result });
        }

        // Get all transactions of a user
        [HttpGet("user/{userId}")]
        public IActionResult GetUserTransactions(int userId)
        {
            var transactions = _context.Transactions
                .Where(t => t.CustomerId == userId)
                .OrderBy(t => t.RentalDate)
                .ToList();

            return Ok(transactions);
        }

        // Get receipt for a transaction
        [HttpGet("{transactionId}/receipt")]
        public IActionResult GetReceipt(int transactionId)
        {
            var transaction = _context.Transactions.FirstOrDefault(t => t.TransactionId == transactionId);
            if (transaction == null) return NotFound("Transaction not found.");

            string receipt = TransactionService.GenerateReceipt(transaction);
            return Ok(receipt);
        }



        // RentRequest class to hold the rent video request data
        public class RentRequest
        {
            public string TitleId { get; set; }
        }

        // ReturnRequest class to hold the return video request data
        public class ReturnRequest
        {
            public string TitleId { get; set; }
        }
    }
}
