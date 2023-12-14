using Microsoft.EntityFrameworkCore;
using Stock_Back.Models;
using Stock_Back.DAL;
using System;

namespace Stock_Back
{
    class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var postgreSQLConnectionConfiguration = new PostgreSQLConfiguration()
            builder.Services.AddControllers();
            builder.Services.AddDbContext<Context>(options =>
                options.UseNpgsql(builder.Configuration.GetValue<string>("PostgreSQLClient")));
            builder.Services.AddDbContext<Context>(opt =>
                opt.UseInMemoryDatabase("PersonList"));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
