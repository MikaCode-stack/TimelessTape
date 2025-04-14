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
            var customer = await _context.Customers
        .FirstOrDefaultAsync(c => c.UserId == request.CustomerId);

            if (customer == null)
            {
                return NotFound("Customer not found.");
            }

            var resultMessage = await customer.RentVideo(_context, request.TitleId);
            return Ok(resultMessage);
        }

        // Return video endpoint
        [HttpPost("return")]
        public async Task<IActionResult> ReturnVideo([FromBody] ReturnRequest request)
        {
            var customer = await _context.Users.FindAsync(request.CustomerId) as Customer;
            if (customer == null) return NotFound("Customer not found.");

            // Assuming ReturnVideo logic is handled in the Customer class or by the TransactionService
            var result = await _transactionService.ReturnVideoAsync(_context, customer, request.TitleId);
            return Ok(result);
        }

        // Get all transactions of a customer
        [HttpGet("customer/{customerId}")]
        public IActionResult GetCustomerTransactions(int customerId)
        {
            var transactions = _context.Transactions
                .Where(t => t.CustomerId == customerId)
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

        // Get fines for a customer
        [HttpGet("{customerId}/fines")]
        public IActionResult GetFines(int customerId)
        {
            var fine = TransactionService.CalculateFines(_context, customerId);
            return Ok(new { customerId, totalFine = fine });
        }
    }

    // RentRequest class to hold the rent video request data
    public class RentRequest
    {
        public int CustomerId { get; set; }
        public string TitleId { get; set; }
    }

    // ReturnRequest class to hold the return video request data
    public class ReturnRequest
    {
        public int CustomerId { get; set; }
        public string TitleId { get; set; }
    }
}
