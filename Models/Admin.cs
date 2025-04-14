using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TimelessTapes.Data;

namespace TimelessTapes.Models
{
    public class Admin : User
    {

        public async Task AddVideo(DBHandler context, string title, string genre, decimal price, int availableCopies)
        {
            var newVideo = new Title
            {
                PrimaryTitle = title,
                Genres = genre,
                Price = price,
                Copies = availableCopies
            };

            context.Titles.Add(newVideo);

            await context.SaveChangesAsync();
        }

        public async Task RemoveVideo(DBHandler context, int videoId)
        {

            var videoToRemove = await context.Titles.FindAsync(videoId);
            if (videoToRemove != null)
            {
                context.Titles.Remove(videoToRemove);

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