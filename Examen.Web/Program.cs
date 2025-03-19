using Examen.Web.Service;
using Examen.Web.Service.IService;
using Examen.Web.Utility;
using Microsoft.AspNetCore.Authentication.Cookies;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<ICompraService, CompraService>();

SD.GatewayAPI = builder.Configuration["ServiceUrls:GatewayAPI"];

builder.Services.AddScoped<IBaseService,BaseService>();
builder.Services.AddScoped<ICompraService, CompraService>();
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddScoped<IVentaService, VentaService>();
builder.Services.AddScoped<IMovimientoService, MovimientoService>();

//builder.Services.AddAuthentication("Bearer")
//    .AddJwtBearer("Bearer", options =>
//    {
//        options.Authority = "https://localhost:7130"; // URL del servicio de autenticación
//        options.Audience = "https://localhost:7264"; // Debe coincidir con el API
//    });
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromHours(10);
        options.LoginPath = "/Login/Login";
        options.AccessDeniedPath = "/Login/AccessDenied";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

app.Run();
