using Microsoft.EntityFrameworkCore;
using TimelessTapes.Models;

namespace TimelessTapes.Data
{
    public class DBHandler : DbContext
    {
        public DBHandler(DbContextOptions<DBHandler> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<AdminLog> AdminLogs { get; set; }
        public DbSet<CustomerReview> CustomerReviews { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Latefee> LateFees { get; set; }
        public DbSet<Refund> Refunds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Users Table
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<User>().Property(u => u.UserId).HasColumnName("userId").ValueGeneratedOnAdd();//Auto Increment
            modelBuilder.Entity<User>().Property(u => u.Name).HasColumnName("name");
            modelBuilder.Entity<User>().Property(u => u.Email).HasColumnName("email");
            modelBuilder.Entity<User>().Property(u => u.PasswordHash).HasColumnName("passwordHash");//Has to encrypt for dynamic data saving
            modelBuilder.Entity<User>().Property(u => u.AccessType).HasColumnName("accessType");//Admin or Customer
            modelBuilder.Entity<User>().Property(u => u.CreatedAt).HasColumnName("createdAt").HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();//Gets the current date and time(Timestamp)

            // AdminLogs Table
            modelBuilder.Entity<AdminLog>().ToTable("AdminLogs").HasKey(a => a.LogId);
            modelBuilder.Entity<AdminLog>().Property(a => a.Action).HasColumnName("action");
            modelBuilder.Entity<AdminLog>().Property(a => a.ActionDate).HasColumnName("actionDate").HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();//Gets the current date and time(Timestamp)
            modelBuilder.Entity<AdminLog>().Property(a => a.IsActive).HasColumnName("isActive");//Boolean to check if the log is active or not
            modelBuilder.Entity<AdminLog>().HasOne(a => a.User).WithMany(u => u.AdminLog).HasForeignKey(a => a.AdminId).OnDelete(DeleteBehavior.Restrict);//Foreign key to get UserId as AdminId

            // CustomerReviews Table
            modelBuilder.Entity<CustomerReview>().ToTable("CustomerReviews").HasKey(c => c.ReviewId);
            modelBuilder.Entity<CustomerReview>().Property(c => c.ReviewText).HasColumnName("reviewText");
            modelBuilder.Entity<CustomerReview>().Property(c => c.Rating).HasColumnName("rating");
            modelBuilder.Entity<CustomerReview>().HasOne(c => c.User).WithMany(u => u.CustomerReviews).HasForeignKey(c => c.CustomerId).OnDelete(DeleteBehavior.Restrict);//Foreign key to get UserId as CustomerId
            modelBuilder.Entity<CustomerReview>().HasOne(c => c.Title).WithMany(v => v.CustomerReviews).HasForeignKey(c => c.TitleId).OnDelete(DeleteBehavior.Restrict);//Foreign key to get TitleId

            // Titles Table
            modelBuilder.Entity<Title>(entity =>
            {
                entity.ToTable("Titles");

                entity.HasKey(t => t.TitleId);

                entity.Property(t => t.TitleId).IsRequired();
                entity.Property(t => t.TitleType);
                entity.Property(t => t.PrimaryTitle);
                entity.Property(t => t.Director);
                entity.Property(t => t.Cast);
                entity.Property(t => t.ReleaseYear);
                entity.Property(t => t.Rating);
                entity.Property(t => t.Duration);
                entity.Property(t => t.Genres);
                entity.Property(t => t.Description);
                entity.Property(t => t.Price);
                entity.Property(t => t.Copies);

                // Relationships
                entity.HasMany(t => t.CustomerReviews)
                      .WithOne(r => r.Title)
                      .HasForeignKey(r => r.TitleId)
                      .HasPrincipalKey(t => t.TitleId);

                entity.HasMany(t => t.Transaction)
                      .WithOne(tr => tr.Title)
                      .HasForeignKey(tr => tr.TitleId)
                      .HasPrincipalKey(t => t.TitleId);
            });

            // Transactions Table
            modelBuilder.Entity<Transaction>().ToTable("Transactions").HasKey(t => t.TransactionId);
            modelBuilder.Entity<Transaction>().Property(t => t.Price).HasColumnName("price");
            modelBuilder.Entity<Transaction>().Property(t => t.Status).HasColumnName("status");
            modelBuilder.Entity<Transaction>().Property(t => t.ReturnDate).HasColumnName("returnDate");
            modelBuilder.Entity<Transaction>().Property(t => t.RentalDate).HasColumnName("rentalDate").HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
            modelBuilder.Entity<Transaction>().HasOne(t => t.Customer).WithMany(u => u.Transaction).HasForeignKey(t => t.CustomerId).OnDelete(DeleteBehavior.NoAction); // Changed to NoAction
            modelBuilder.Entity<Transaction>().HasOne(t => t.Title).WithMany(v => v.Transaction).HasForeignKey(t => t.TitleId).OnDelete(DeleteBehavior.NoAction); // Changed to NoAction

            // Payments Table
            modelBuilder.Entity<Payment>().ToTable("Payments").HasKey(p => p.PaymentId);
            modelBuilder.Entity<Payment>().Property(p => p.Amount).HasColumnName("amount");
            modelBuilder.Entity<Payment>().Property(p => p.PaymentDate).HasColumnName("paymentDate").HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
            modelBuilder.Entity<Payment>().HasOne(p => p.User).WithMany(u => u.Payments).HasForeignKey(p => p.CustomerId).OnDelete(DeleteBehavior.NoAction); // Changed to NoAction
            modelBuilder.Entity<Payment>().HasOne(p => p.Transaction).WithMany(t => t.Payments).HasForeignKey(p => p.TransactionId).OnDelete(DeleteBehavior.NoAction); // Changed to NoAction

            // Refunds Table
            modelBuilder.Entity<Refund>().ToTable("Refunds").HasKey(r => r.RefundId);
            modelBuilder.Entity<Refund>().Property(r => r.RefundAmount).HasColumnName("amount");
            modelBuilder.Entity<Refund>().Property(r => r.RefundDate).HasColumnName("refundDate").HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
            modelBuilder.Entity<Refund>().Property(r => r.Status).HasColumnName("status");
            modelBuilder.Entity<Refund>().HasOne(r => r.Payment).WithMany(p => p.Refund).HasForeignKey(r => r.PaymentId).OnDelete(DeleteBehavior.Restrict);

            // LateFees Table
            modelBuilder.Entity<Latefee>().ToTable("LateFees").HasKey(l => l.LatefeeId);
            modelBuilder.Entity<Latefee>().Property(l => l.Amount).HasColumnName("amount");
            modelBuilder.Entity<Latefee>().Property(l => l.FeeDate).HasColumnName("feeDate");
            modelBuilder.Entity<Latefee>().Property(l => l.Paid).HasColumnName("paid");
            modelBuilder.Entity<Latefee>().HasOne(l => l.Transaction).WithMany(t => t.Latefees).HasForeignKey(l => l.TransactionId).OnDelete(DeleteBehavior.NoAction); // Changed to NoAction
        }
    }
}
