using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ZhangLe.Models
{
    public class FinanceDbContext: IdentityDbContext
    {
        public FinanceDbContext(DbContextOptions<FinanceDbContext> options): base(options)
        {

        }

        public DbSet<Transaction> transactions { get; set; }
    }
}