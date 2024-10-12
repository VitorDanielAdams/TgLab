using Microsoft.EntityFrameworkCore;
using TgLabApi.Domain.Entities.Player;
using TgLabApi.Domain.Entities.Transaction;

namespace TGLabAPI.Infrastructure
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }

        public DbSet<PlayerEntity> Players { get; set; }
        public DbSet<WalletEntity> Wallets { get; set; }
        public DbSet<BetEntity> Bets { get; set; }
        public DbSet<TransactionEntity> Trasactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           
            modelBuilder.Entity<PlayerEntity>()
                .HasIndex(p => p.Email)
                .IsUnique();
        }
    }
}
