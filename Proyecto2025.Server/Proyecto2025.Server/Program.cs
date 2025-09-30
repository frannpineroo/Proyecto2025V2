using Microsoft.EntityFrameworkCore;
using Proyecto2025.BD.Datos;
<<<<<<< HEAD
using Proyecto2025.BD.Datos.Entity;
using Proyecto2025.Repositorio.Repositorios;
using Proyecto2025.Server.Components;
using System;
=======
using Proyecto2025.Repositorio.Repositorios;
using Proyecto2025.Server.Components;
using System.Text.Json.Serialization;
>>>>>>> 55769ae5e5faeaf4ea1aaa985e38823f4475e0c8

var builder = WebApplication.CreateBuilder(args);

// Controllers y Swagger
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// DbContext con SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("El string de conexion no existe.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

<<<<<<< HEAD
builder.Services.AddScoped<IRepositorio<Chat>, Repositorio<Chat>>();
builder.Services.AddScoped<IChatMemberRepositorio<ChatMember>, ChatMemberRepositorio<ChatMember>>();
=======
// Registro de Repositorios
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

// HttpClient configurado con BaseAddress
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:5001/") // ⚠️ ajustá el puerto al de tu API
});
>>>>>>> 55769ae5e5faeaf4ea1aaa985e38823f4475e0c8

// Blazor y Razor Components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Proyecto2025.Server.Client._Imports).Assembly);

app.MapControllers();

app.Run();
