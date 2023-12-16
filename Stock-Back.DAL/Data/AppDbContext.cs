using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Stock_Back.DAL.Models;


namespace Stock_Back.DAL.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }


        public DbSet<User> Users { get; set; }
    }
}
