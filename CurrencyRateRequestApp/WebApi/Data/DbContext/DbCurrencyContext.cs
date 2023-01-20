
using Microsoft.EntityFrameworkCore;
using WebApi.Core.Entities.Concrete;

namespace WebApi.Data.DbContext
{
    public class DbCurrencyContext:Microsoft.EntityFrameworkCore.DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString: @"Host=localhost;Database=DbCurrency;Port=5434;Username=postgres;Password=3458"); //
        }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<CurrencyRate> CurrencyRate { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; } //Host=localhost;Database=DbCurrency;User Id=postgres; Password=3458;
    }
}
