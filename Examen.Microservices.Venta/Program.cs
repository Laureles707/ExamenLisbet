using System.Text;
using Examen.Microservices.Compras.Mappers;
using Examen.Microservices.Venta.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Configurar autenticación JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:7130"; //  URL del API Auth
        options.RequireHttpsMetadata = false; // Si estás en desarrollo, evita errores SSL
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "examen-auth-api", //  Debe coincidir con el issuer del token
            ValidAudience = "examen-client", //  DEBE SER IGUAL al que se usa en el Gateway
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication"))
        };
    });

// Configurar autenticación con JWT
builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddDbContext<ApplicationDBContext>(opciones =>
                    opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql")));



//agregamos el automapper
builder.Services.AddAutoMapper(typeof(VentaMapper));





builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins("http://localhost:7264") // Permitir solo el API Gateway
              .AllowAnyHeader()
              .AllowAnyMethod();
        //.AllowCredentials(); // Si usas autenticación
    });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthorization();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Microservicio Ventas");
    });
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();



app.Run();