using Microsoft.EntityFrameworkCore;
using Stock_Back.DAL.Models;
using System.Security.Cryptography;
using Stock_Back.DAL.Utilities;


namespace Stock_Back.DAL.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }


        public DbSet<Client> Clients { get; set; }
        public DbSet<MaterialType> Materials { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FinancialCategory> FinancialCategory { get; set; }
        public DbSet<FinancialSubCategory> FinancialSubCategory { get; set; }
        public DbSet<FinancialMovements> FinancialMovements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var adminUser = new User
            {
                Id = 1,
                Email = "admin@admin.cl",
                Password = Hasher.HashPassword("admin1234"),
                SuperAdmin = true,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow
            };

            modelBuilder.Entity<User>().HasData(adminUser);


        }
    }
}
