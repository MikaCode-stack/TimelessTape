using TimelessTapes.Data;
using TimelessTapes.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static TimelessTapes.Models.Transaction;
//using TimelessTapes.Models.Transaction;

namespace TimelessTapes.Services
{
    /// <summary>
    /// Handles business logic for transactions: caching, fine calculations, payments, receipts.
    /// </summary>
    public class TransactionService
    {
        // In-memory lookup maps for quick access
        private static readonly Dictionary<int, Transaction> TransactionMap = new();
        private static readonly Dictionary<int, List<Transaction>> CustomerTransactionMap = new();

        /// <summary>
        /// Adds transaction to in-memory cache.
        /// </summary>
        public static void CacheTransaction(Transaction transaction)
        {
            TransactionMap[transaction.TransactionId] = transaction;

            if (!CustomerTransactionMap.ContainsKey(transaction.CustomerId))
                CustomerTransactionMap[transaction.CustomerId] = new List<Transaction>();

            CustomerTransactionMap[transaction.CustomerId].Add(transaction);
        }

        /// <summary>
        /// Fetches transaction from cache by ID.
        /// </summary>
        public static Transaction? GetTransactionById(int id) =>
            TransactionMap.TryGetValue(id, out var transaction) ? transaction : null;

        /// <summary>
        /// Fetches all transactions for a given customer.
        /// </summary>
        public static List<Transaction> GetTransactionsByCustomerId(int customerId) =>
            CustomerTransactionMap.TryGetValue(customerId, out var list) ? list : new List<Transaction>();

        /// <summary>
        /// Rent a video for a customer.
        /// </summary>
        public async Task<string> RentVideoAsync(DBHandler context, User user, string titleId)
        {
            var title = await context.Titles
                .FirstOrDefaultAsync(t => t.TitleId == titleId);

            if (title == null)
                return "Video not found.";

            var existingTransaction = await context.Transactions
                .FirstOrDefaultAsync(t =>
                    t.CustomerId == user.UserId &&
                    t.TitleId == titleId &&
                    t.Status == RentalStatus.Rented);

            if (existingTransaction != null)
                return "This video is already rented by the customer.";

            var transaction = new Transaction
            {
                CustomerId = user.UserId,
                TitleId = titleId,
                Price = title.Price,
                Status = RentalStatus.Rented,
                RentalDate = DateTime.UtcNow,
                DaysRented = 7
            };

            context.Transactions.Add(transaction);
            await context.SaveChangesAsync();

            return "Video rented successfully.";
        }

        /// <summary>
        /// Process return of a video and calculate fine if overdue.
        /// </summary>
        public async Task<string> ReturnVideoAsync(DBHandler context, User user, string titleId)
        {
            var title = await context.Titles.FirstOrDefaultAsync(t => t.TitleId == titleId);
            if (title == null)
                return "Video not found.";

            var existingTransaction = await context.Transactions
                .FirstOrDefaultAsync(t => t.CustomerId == user.UserId && t.TitleId == titleId && t.Status == RentalStatus.Rented);
            if (existingTransaction == null)
                return "No rental record found for this video.";

            // Update the transaction status
            existingTransaction.Status = RentalStatus.Returned;
            existingTransaction.ReturnDate = DateTime.UtcNow;
            await context.SaveChangesAsync();

            return "Video returned successfully.";
        }

        /// <summary>
        /// Calculates fine for a single transaction based on due date.
        /// </summary>
        private static double CalculateFine(Transaction transaction)
        {
            var dueDate = transaction.RentalDate.AddDays(transaction.DaysRented);
            if (transaction.ReturnDate.HasValue && transaction.ReturnDate > dueDate)
            {
                int overdueDays = (transaction.ReturnDate.Value - dueDate).Days;
                double fine = overdueDays * 2.5;
                transaction.ExcessFines = fine;
                return fine;
            }
            return 0;
        }

   
        /// <summary>
        /// Generates a printable receipt for a transaction.
        /// </summary>
        public static string GenerateReceipt(Transaction t) =>
            $@"--- Receipt ---\n" +
            $"Transaction ID: {t.TransactionId}\n" +
            $"Customer ID:    {t.CustomerId}\n" +
            $"Title ID:       {t.TitleId}\n" +
            $"Price:          ${t.Price:F2}\n" +
            $"Days Rented:    {t.DaysRented}\n" +
            $"Rental Date:    {t.RentalDate:yyyy-MM-dd}\n" +
            $"Return Date:    {(t.ReturnDate?.ToString("yyyy-MM-dd") ?? "Not returned")}\n" +
            $"Fine:           ${t.ExcessFines:F2}\n" +
            $"Status:         {t.Status}\n" +
            $"----------------";

       
    }
}
