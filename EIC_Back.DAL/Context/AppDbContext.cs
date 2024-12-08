using Microsoft.EntityFrameworkCore;
using EIC_Back.DAL.Models;
using System.Security.Cryptography;
using EIC_Back.DAL.Utilities;


namespace EIC_Back.DAL.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }


        public DbSet<Client> Clients { get; set; }
        public DbSet<Personnel> Personnel { get; set; }
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
                Name = "Francisco",
                Vigency = true,
                Job = "CEO",
                Phone = "99999999",
                SuperAdmin = true,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow
            };

            var materialCategory = new FinancialCategory
            {
                Id = 1,
                Description = "MaterialCategory",
                Name = "Materiales",
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow
            };

            var materialsSub = new FinancialSubCategory
            {
                FinancialCategoryId = 1,
                Id = 1,
                Name = "Materiales de Proyecto",
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow
            };

            modelBuilder.Entity<User>().HasData(adminUser);
            modelBuilder.Entity<FinancialCategory>().HasData(materialCategory);
            modelBuilder.Entity<FinancialSubCategory>().HasData(materialsSub);
        }
    }
}
