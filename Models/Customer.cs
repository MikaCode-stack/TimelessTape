using System;
using TimelessTapes.Data;

namespace TimelessTapes.Models
{
    public class Customer : User
    {
        public async Task<string> RentVideo(DBHandler context, int videoId)
        {
            var video = await context.Videos.FindAsync(videoId);
            if (video != null && video.Available)
            {
                var transaction = new Transaction
                {
                    VideoID = videoId,
                    CustomerID = this.UserId,
                    RentalDate = DateTime.UtcNow,
                    Status = "Rented"
                };

                context.Transactions.Add(transaction);
                video.Available = false;
                await context.SaveChangesAsync();
                return "Video rented successfully.";
            }
            else
            {
                return "Video not found or unavailable.";
            }
        }

        public async Task<string> ReturnVideo(DBHandler context, int videoId)
        {
            var transaction = context.Transactions.FirstOrDefault(t => t.VideoID == videoId && t.CustomerID == this.UserId && t.Status == "Rented");
            if (transaction != null)
            {
                var video = await context.Videos.FindAsync(videoId);
                if (video != null)
                {
                    video.Available = true;
                    transaction.Status = "Returned";
                    await context.SaveChangesAsync();
                    return "Video returned successfully.";
                }
                else
                {
                    return "Video not found";
                }
            }
            else
            {
                return "Transaction not found.";
            }
        }

        public List<Transaction> MyTransactions(DBHandler context)
        {
            return context.Transactions.Where(t => t.CustomerID == this.UserId).OrderBy(t => t.RentalDate).ToList();
        }

        internal Task<string> RentVideo(DBHandler context, object videoId)
        {
            throw new NotImplementedException();
        }
    }
}
