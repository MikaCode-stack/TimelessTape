using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TimelessTapes.Models;
namespace TimelessTapes.Data
{
    public class DBHandler : DbContext
    {
        public DBHandler(DbContextOptions<DBHandler> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Video> Videos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");  // Maps 'User' class to existing 'Users' table
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);  // Ensures it's a Primary Key

            modelBuilder.Entity<User>()
                .Property(u => u.UserId)
                .HasColumnName("userId")
                .ValueGeneratedOnAdd();  // ✅ THIS ENABLES AUTO-INCREMENT
            modelBuilder.Entity<User>()
                .Property(u => u.Name)
                .HasColumnName("name");
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .HasColumnName("email");
            modelBuilder.Entity<User>()
                .Property(u => u.PasswordHash)
                .HasColumnName("passwordHash");
            modelBuilder.Entity<User>()
                .Property(u => u.AccessType)
                .HasColumnName("accessType");
            modelBuilder.Entity<User>()
               .Property(u => u.CreatedAt)
               .HasColumnName("createdAt")
               .HasDefaultValueSql("GETDATE()") // ✅ Let SQL Server set timestamp
               .ValueGeneratedOnAdd();          // ✅ Prevent explicit insertion
        }
    }
}
