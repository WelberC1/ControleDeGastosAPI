using ControleDeGastosAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleDeGastosAPI.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options){ }

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasKey(u => u.UUID);

            modelBuilder.Entity<User>()
               .Property(u => u.UUID)
               .HasColumnType("nvarchar(450)")
               .HasDefaultValueSql("NEWID()")
               .ValueGeneratedOnAdd();

            modelBuilder.Entity<User>()
                .Property(u => u.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<User>()
                .Property(u => u.Balance)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Transaction>()
                .HasKey(t => t.UUID);

            modelBuilder.Entity<Transaction>()
               .Property(t => t.UUID)
               .HasColumnType("nvarchar(450)")
               .HasDefaultValueSql("NEWID()")
               .ValueGeneratedOnAdd();

            modelBuilder.Entity<Transaction>()
                .Property(t => t.IsVisible)
                .HasDefaultValue(true);

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Amount)
                .HasColumnType("decimal(18,2)");

        }
    }
}
