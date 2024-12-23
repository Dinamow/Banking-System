using Microsoft.EntityFrameworkCore;
using Banking_System.Core.Models;

namespace Banking_System.Infrastructure.Data
{
    public class BankingDataContext : DbContext
    {
        public BankingDataContext(DbContextOptions<BankingDataContext> options) : base(options) { }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Account Entity
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(a => a.Id); // Primary Key
                entity.Property(a => a.AccountNumber).IsRequired().HasMaxLength(20);
                entity.Property(a => a.AccountType).IsRequired().HasMaxLength(20);
                entity.Property(a => a.Balance).HasDefaultValue(0);
                entity.Property(a => a.OverDraftLimit).HasDefaultValue(0); // Default value 0
                entity.Property(a => a.IntrestRate).HasDefaultValue(0.0f); // Default value 0.0
                entity.Property(a => a.CreateAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(a => a.UpdateAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            // Configure Transaction Entity
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(t => t.Id); // Primary Key
                entity.Property(t => t.TransactionType).IsRequired().HasMaxLength(20);
                entity.Property(t => t.Amount).IsRequired();
                entity.Property(t => t.CreateAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(t => t.UpdateAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

                // Foreign Key Relationship
                entity.HasOne<Account>()
                      .WithMany()
                      .HasForeignKey(t => t.AccountId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Self-Referencing Relationship for Transfers
                entity.HasOne<Account>()
                      .WithMany()
                      .HasForeignKey(t => t.TargetAccount)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
