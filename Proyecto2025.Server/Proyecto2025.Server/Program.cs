using Microsoft.EntityFrameworkCore;
using Proyecto2025.BD.Datos;
using Proyecto2025.BD.Datos.Entity;
using Proyecto2025.Repositorio.Repositorios;
using Proyecto2025.Server.Components;
using System;
using System.Text.Json.Serialization;


using Proyecto2025.Repositorio.Repositorios; 


var builder = WebApplication.CreateBuilder(args);

// Controllers y Swagger
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// Agregando SignalR
builder.Services.AddSignalR();

// DbContext con SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("El string de conexion no existe.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IRepositorio<Chat>, Repositorio<Chat>>();
builder.Services.AddScoped<IChatMemberRepositorio<ChatMember>, ChatMemberRepositorio<ChatMember>>();

// Registro de Repositorios
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<INotificacionRepositorio, NotificacionRepositorio>(); // <-- ¡AQUÍ ESTÁ LA LÍNEA QUE AGREGUÉ!
builder.Services.AddScoped<IMensajeRepositorio, MensajeRepositorio>();
// HttpClient configurado con BaseAddress
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7016/") // ⚠️ ajustá el puerto al de tu API
});

// Registro del MessageApiService


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

////////////////////////////////////////
///
using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
try
{
    // Esto crea la base de datos si no existe
    context.Database.EnsureCreated();
    Console.WriteLine("¡Conexión a la base de datos OK!");
}
catch (Exception ex)
{
    Console.WriteLine($"Error de conexión: {ex.Message}");
}



app.Run();