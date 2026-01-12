using Microsoft.EntityFrameworkCore;
using WEB_253551_Levchuk.API.Models;

namespace WEB_253551_Levchuk.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}

