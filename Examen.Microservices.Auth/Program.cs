using System.Text;
using Examen.Microservices.Auth.Data;
using Examen.Microservices.Auth.Models;
using Examen.Microservices.Auth.Services;
using Examen.Microservices.Auth.Services.IService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


/*****************************/

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "examen-auth-api", 
            ValidAudience = "examen-client", 
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication")) //
        };
    });

builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddDbContext<ApplicationDBContext>(opciones =>
                    opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql")));

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));

// Agregar Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDBContext>()
    .AddDefaultTokenProviders();
builder.Services.AddControllers();
builder.Services.AddScoped<IAuthService,AuthService>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        if (!app.Environment.IsDevelopment())
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auth API");
            c.RoutePrefix = string.Empty;
        }
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
ApplyMigration();
app.Run();

void ApplyMigration()
{
    using(var scope = app.Services.CreateScope())
    {
        var _db = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
        if(_db.Database.GetPendingMigrations().Count() > 0)
        {
            _db.Database.Migrate();
        }
    }
}