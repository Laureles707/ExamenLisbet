using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Microsoft.OpenApi.Models;
using Ocelot.Cache.CacheManager;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Agregar autenticación JWT
var secretKey = builder.Configuration["Jwt:SecretKey"];

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Agregar configuración de Ocelot
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddOcelot(builder.Configuration)
    .AddCacheManager(x=>
    {
        x.WithDictionaryHandle();
    });


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:7130";
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "examen-auth-api",
            ValidAudience = "examen-client", // DEBE COINCIDIR CON EL CLAIM "aud"
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication"))
        };
    });

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
       // c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Gateway v1");
        c.SwaggerEndpoint("https://localhost:7120/swagger/v1/swagger.json", "Microservicio Compras");

    });
}
builder.Services.AddAuthorization();
app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();
// Usar Ocelot como middleware
await app.UseOcelot();
app.MapControllers();
app.Run();