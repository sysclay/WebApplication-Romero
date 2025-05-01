using WebApplication_Romero.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication_Romero.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
