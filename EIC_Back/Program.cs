using Microsoft.EntityFrameworkCore;
using EIC_Back.DAL.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using EIC_Back.BLL.Controllers.JwtControllers;
using EIC_Back.DAL.Mapper;
using EIC_Back.BLL.Mapper;
using Microsoft.OpenApi.Models;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(
        o => o.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase"), x => x.MigrationsAssembly("EIC_Back.DAL"))
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

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddCors(options =>
{
    try
    {
        options.AddDefaultPolicy(policyBuilder =>
        {
            policyBuilder.SetIsOriginAllowed(origin =>
            {
                // Get the host part from the origin
                var host = new Uri(origin).Host;

                // Check if it's an IP address or a domain name
                if (IPAddress.TryParse(host, out var ipAddress))
                {
                    // List of allowed IPs
                    var allowedIPs = new List<IPAddress>
                    {
                                IPAddress.Parse("172.28.0.9"), // Replace with your allowed IPs
                                IPAddress.Parse("172.28.0.10"), // Replace with your allowed IPs
                                IPAddress.Parse("64.176.3.190"),
                                IPAddress.Parse("64.176.3.191"),
                                IPAddress.Parse("64.176.13.97")
                    };
                    if (builder.Environment.IsDevelopment())
                        allowedIPs.Add(IPAddress.Parse("127.0.0.1"));
                    return allowedIPs.Contains(ipAddress);
                }
                else
                {
                    // List of allowed domains
                    var allowedDomains = new List<string>
                        {
                                    "www.eicconstruccion.com"
                        };
                    if (builder.Environment.IsDevelopment())
                        allowedDomains.Add("localhost");

                    return allowedDomains.Contains(host);
                }
            })
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
    }
    catch (Exception ex)
    {
        throw ex;
    }
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["ConfiguracionJwt:Issuer"],
            ValidAudience = builder.Configuration["ConfiguracionJwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["ConfiguracionJwt:Llave"])),
            ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256 }, // Enforce HmacSha256
            ClockSkew = TimeSpan.FromMinutes(5)
        };
        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = context =>
            {
                var claims = context.Principal.Claims;
                foreach (var claim in claims)
                {
                    Console.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
                }
                return Task.CompletedTask;
            },
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"Token failed: {context.Exception}");
                return Task.CompletedTask;
            }
        };
    });
builder.Services.AddScoped<IManejoJwt, ManejoJwt>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "EIC_Back API v1"); //Versioning of APIs
        c.RoutePrefix = string.Empty;  // Set Swagger UI at apps root and redirect and url prefix
    });
}
app.UseCors();
app.UseExceptionHandler("/Error");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
