using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TimelessTapes.Data;

namespace TimelessTapes.Models
{
    public class Admin : User
    {
        public async Task AddVideo(DBHandler context, string title, string genre, decimal price)
        {
            var newVideo = new Video
            {
                Title = title,
                Genre = genre,
                Price = price,
                Available = true
            };

            context.Videos.Add(newVideo);
            await context.SaveChangesAsync();
        }

        public async Task RemoveVideo(DBHandler context, int videoId)
        {
            var videoToRemove = await context.Videos.FindAsync(videoId);
            if (videoToRemove != null)
            {
                context.Videos.Remove(videoToRemove);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Video not found.");
            }
        }

        public List<Transaction> CustomersTransactions(DBHandler context)
        {
            return context.Transactions.OrderBy(t => t.RentalDate).ToList();
        }
    }
}