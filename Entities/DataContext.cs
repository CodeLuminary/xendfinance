using Microsoft.EntityFrameworkCore;
namespace Xend_Finance.Entities
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<Clients> Clients { get; set; }
        public DbSet<Wallet> Wallet { get; set; }
    }
}
