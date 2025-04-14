using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimelessTapes.Data;
using Microsoft.EntityFrameworkCore;

namespace TimelessTapes.Models
{
    public class Customer : User
    {
        public async Task<string> RentVideo(DBHandler context, string titleId)
        {
            var video = await context.Titles.FindAsync(titleId);
            if (video != null && video.Copies > 0)
            {
                var transaction = new Transaction
                {
                    TitleId = titleId,
                    CustomerId = this.UserId,
                    RentalDate = DateTime.UtcNow,
                    Status = "Rented"
                };

                context.Transactions.Add(transaction);
                video.Copies -= 1;
                await context.SaveChangesAsync();
                return "Video rented successfully.";
            }
            else
            {
                return "Video not found or unavailable.";
            }
        }

        public async Task<string> ReturnVideo(DBHandler context, string titleId)
        {
            var transaction = await context.Transactions
                .Where(t => t.TitleId == titleId && t.CustomerId == this.UserId && t.Status == "Rented")
                .OrderByDescending(t => t.RentalDate)
                .FirstOrDefaultAsync();

            if (transaction != null)
            {
                var video = await context.Titles.FindAsync(titleId);
                if (video != null)
                {
                    video.Copies += 1;
                    transaction.Status = "Returned";
                    transaction.ReturnDate = DateTime.UtcNow;
                    await context.SaveChangesAsync();
                    return "Video returned successfully.";
                }
                else
                {
                    return "Video not found.";
                }
            }
            else
            {
                return "Transaction not found.";
            }
        }

        public async Task<List<Transaction>> MyTransactionsAsync(DBHandler context)
        {
            return await context.Transactions
                .Where(t => t.CustomerId == this.UserId)
                .OrderByDescending(t => t.RentalDate)
                .ToListAsync();
        }
    }
}
