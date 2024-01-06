using Microsoft.EntityFrameworkCore;
using Stock_Back.DAL.Models;


namespace Stock_Back.DAL.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }


        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
    }
}
