using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PA_ORIGEN_N1.Data;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------
// Configuración de servicios
// ---------------------------

// Conexión a la base de datos
builder.Services.AddDbContext<AplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Autenticación con cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Acceso/Login";
        options.AccessDeniedPath = "/Home/AccessDenied";
    });

// Soporte para sesiones
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Permitir acceso al contexto HTTP en las vistas
builder.Services.AddHttpContextAccessor();

// Agregar MVC
builder.Services.AddControllersWithViews();

// ---------------------------
// Configuración del middleware
// ---------------------------

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

// Rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


