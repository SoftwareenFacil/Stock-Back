using Microsoft.EntityFrameworkCore;
using Stock_Back.DAL.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Stock_Back.BLL.Controllers.JwtControllers;
using Stock_Back.DAL.Mapper;
using Stock_Back.BLL.Mapper;
using Microsoft.OpenApi.Models;
using Stock_Back.Controllers.Services;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net;

namespace Stock_Back
{
    class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<AppDbContext>(
                    o => o.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase"), x => x.MigrationsAssembly("Stock_Back.DAL"))
                );
            builder.Services.AddAutoMapper(typeof(DalMappingProfile).Assembly, typeof(BllMappingProfile).Assembly);
            builder.Services.AddControllers(opt =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                opt.Filters.Add(new AuthorizeFilter(policy));
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });


            builder.Services.AddCors(options =>
            {
                try
                {
                    options.AddDefaultPolicy(policyBuilder =>
                    {

                        policyBuilder.SetIsOriginAllowed(origin =>
                        {
                            // Convert domain to IP 
                            // be careful, domains can have multiple IPs
                            var host = new Uri(origin).Host;
                            var ipAddresses = Dns.GetHostAddresses(host);

                            // List of allowed IPs
                            var allowedIPs = new List<IPAddress>
                                {
                                IPAddress.Parse("172.18.0.9"), // Replace with your allowed IPs
                                IPAddress.Parse("64.176.3.190"),  // Another IP
                                IPAddress.Parse("127.0.0.1")
                                };

                            return ipAddresses.Any(ip => allowedIPs.Contains(ip));
                        })
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            });


            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["ConfiguracionJwt:Llave"] ?? string.Empty)
                        )
                    };
                });
            builder.Services.AddScoped<IManejoJwt, ManejoJwt>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Stock_Back API v1"); //Versioning of APIs
                    c.RoutePrefix = string.Empty;  // Set Swagger UI at apps root and redirect and url prefix
                });
            }
            app.UseCors();
            app.UseExceptionHandler("/Error");

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
