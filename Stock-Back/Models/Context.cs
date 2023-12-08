using Microsoft.EntityFrameworkCore;

namespace Stock_Back.Models;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) { }

    public DbSet<Person> Personas { get; set; }

}