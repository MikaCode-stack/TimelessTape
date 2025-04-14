using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TimelessTapes.Data;

namespace TimelessTapes.Models
{
    public class Admin : User
    {

        public async Task AddVideo(DBHandler context, string titleId, string titleType, string primaryTitle,
             string director, string cast, int? releaseYear, string rating, string duration,
             string genres, string description, decimal price, int copies)
        {
            var newVideo = new Title
            {
                TitleId = titleId,
                TitleType = titleType,
                PrimaryTitle = primaryTitle,
                Director = director,
                Cast = cast,
                ReleaseYear = releaseYear,
                Rating = rating,
                Duration = duration,
                Genres = genres,
                Description = description,
                Price = price,
                Copies = copies
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