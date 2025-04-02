using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TimelessTapes.Data;
using TimelessTapes.Models;

[Route("api/[controller]")]
[ApiController]
public class TransactionsController : ControllerBase
{
    private readonly DBHandler _context;

    public TransactionsController(DBHandler context)
    {
        _context = context;
    }

    // Retning a video (Creates a new transaction)
    [HttpPost("rent")]
    public async Task<IActionResult> RentVideo([FromBody] Transaction transaction)
    {
        if (transaction == null)
        {
            return BadRequest("Invalid transaction data.");
        }

        // Set default values
        transaction.Status = "Active";
        transaction.RentalDate = DateTime.UtcNow;
        transaction.ReturnDate = null;

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Rental successful", transaction });
    }

    // Returning a video (Updates returnDate and status)
    [HttpPut("return/{transactionID}")]
    public async Task<IActionResult> ReturnVideo(int transactionID)
    {
        var transaction = await _context.Transactions.FindAsync(transactionID);
        if (transaction == null)
        {
            return NotFound("Transaction not found.");
        }

        transaction.ReturnDate = DateTime.UtcNow;
        transaction.Status = "Completed";  // Mark as returned

        await _context.SaveChangesAsync();
        return Ok(new { message = "Video returned successfully", transaction });
    }

    // Get transaction details by ID (Receipt)
    [HttpGet("{transactionID}")]
    public async Task<IActionResult> GetTransaction(int transactionID)
    {
        var transaction = await _context.Transactions.FindAsync(transactionID);
        if (transaction == null)
        {
            return NotFound("Transaction not found.");
        }
        return Ok(transaction);
    }

    // Process a payment for a transaction
    [HttpPost("process-payment")]
    public async Task<IActionResult> ProcessPayment(int customerID, decimal amount)
    {
        var customerTransactions = await _context.Transactions
            .Where(t => t.CustomerID == customerID && t.Status == "Active")
            .ToListAsync();

        if (!customerTransactions.Any())
        {
            return BadRequest("No active transactions found for this customer.");
        }

        // Logic to process payment (simplified)
        foreach (var transaction in customerTransactions)
        {
            transaction.Status = "Paid";  // Update status to 'Paid'
        }

        await _context.SaveChangesAsync();
        return Ok(new { message = "Payment processed successfully", amount });
    }

    // Calculate overdue fines for a customer
    [HttpGet("calculate-fines/{customerID}")]
    public async Task<IActionResult> CalculateFines(int customerID)
    {
        var overdueTransactions = await _context.Transactions
            .Where(t => t.CustomerID == customerID && t.ReturnDate == null && t.RentalDate < DateTime.UtcNow.AddDays(-7))
            .ToListAsync();

        if (!overdueTransactions.Any())
        {
            return Ok(new { message = "No fines due.", totalFine = 0.00 });
        }

        decimal fineAmount = overdueTransactions.Count * 5.00m; // $5 fine per overdue transaction

        return Ok(new { message = "Fines calculated.", totalFine = fineAmount });
    }
}
