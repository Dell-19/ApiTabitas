using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net.Security;
using WebTabitas.Repositorio;
using WebTabitas.Repositorio.IRepositorio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//autentificacion
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options=>
    {
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.LoginPath = "/Home/Login";
        options.AccessDeniedPath = "/Home/AccessDenied";
        options.SlidingExpiration = true;
    }
    );


// Configurar cliente Http
builder.Services.AddHttpClient();
builder.Services.AddScoped<IGeneralRepositorio, GeneralRepositorio>();
builder.Services.AddScoped<IAlmacenRepositorio, AlmacenRepositorio>();
builder.Services.AddScoped<IEtiquetaRepositorio, EtiquetaRepositorio>();
builder.Services.AddScoped<ICorteRepositorio, CorteRepositorio>();
builder.Services.AddScoped<ICorteLaserRepositorio, CorteLaserRepositorio>();
builder.Services.AddScoped<IBordadoRepositorio, BordadoRepositorio>();
builder.Services.AddScoped<ISerigrafiaRepositorio, SerigrafiaRepositorio>();
builder.Services.AddScoped<IMaquilaRepositorio, MaquilaRepositorio>();
builder.Services.AddScoped<ILavadoRepositorio, LavadoRepositorio>();
builder.Services.AddScoped<ICalidadRepositorio, CalidadRepositorio>();
builder.Services.AddScoped<ITerminadoRepositorio, TerminadoRepositorio>();
builder.Services.AddScoped<IProcesoActualRepositorio, ProcesoActualRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//soporte seccion
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
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

app.UseStaticFiles();
app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
