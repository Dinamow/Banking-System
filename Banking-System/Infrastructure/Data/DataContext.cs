using Microsoft.EntityFrameworkCore;
using Banking_System.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Banking_System.Infrastructure.Data
{
    public class BankingDataContext : IdentityDbContext<IdentityUser>
    {
        public BankingDataContext(DbContextOptions<BankingDataContext> options) : base(options) { }
        public DbSet<BankingAccount> Accounts { get; set; }
        public DbSet<BankingTransaction> Transactions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Account Entity
            modelBuilder.Entity<BankingAccount>(entity =>
            {
                entity.HasKey(a => a.Id); // Primary Key
                entity.HasIndex(a => a.AccountNumber).IsUnique();
                entity.Property(a => a.AccountType).IsRequired().HasMaxLength(20);
                entity.Property(a => a.Balance).HasDefaultValue(0);
                entity.Property(a => a.OverDraftLimit).HasDefaultValue(0); // Default value 0
                entity.Property(a => a.IntrestRate).HasDefaultValue(0.0f); // Default value 0.0
                entity.Property(a => a.CreateAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(a => a.UpdateAt).HasDefaultValueSql("CURRENT_TIMESTAMP");


                entity.HasOne(a => a.User)
                      .WithMany()
                      .HasForeignKey(a => a.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Transaction Entity
            modelBuilder.Entity<BankingTransaction>(entity =>
            {
                entity.HasKey(t => t.Id); // Primary Key
                entity.Property(t => t.TransactionType).IsRequired().HasMaxLength(20);
                entity.Property(t => t.Amount).IsRequired();
                entity.Property(t => t.CreateAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(t => t.UpdateAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

                // Foreign Key Relationship
                entity.HasOne<BankingAccount>()
                      .WithMany()
                      .HasForeignKey(t => t.AccountId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Self-Referencing Relationship for Transfers
                entity.HasOne<BankingAccount>()
                      .WithMany()
                      .HasForeignKey(t => t.TargetAccount)
                      .OnDelete(DeleteBehavior.Restrict)
                      .IsRequired(false);
            });
        }
    }
}
