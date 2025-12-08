using Microsoft.EntityFrameworkCore;
using Proyecto2025.BD.Datos;
using Proyecto2025.BD.Datos.Entity;
using Proyecto2025.Repositorio.Repositorios;
using Proyecto2025.Server.Components;
using Proyecto2025.Server.Hubs;
using Proyecto2025.Servicio.ChatMemberServicioHttp;
using Proyecto2025.Servicio.ChatServicioHttp;
using Proyecto2025.Servicio.ServiciosHttp;
using System;

var builder = WebApplication.CreateBuilder(args);


//  AGREGAR CONTROLLERS Y SWAGGER

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();


//  AGREGAR SIGNALR (PARA NOTIFICACIONES)

builder.Services.AddSignalR();


//  CONFIGURAR BASE DE DATOS

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("El string de conexión no existe.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));


//  REGISTRO DE REPOSITORIOS

builder.Services.AddScoped<IRepositorio<Chat>, Repositorio<Chat>>();
builder.Services.AddScoped<IChatMemberRepositorio<ChatMember>, ChatMemberRepositorio<ChatMember>>();

builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<INotificationRepositorio, NotificationRepositorio>();
builder.Services.AddScoped<IMensajeRepositorio, MensajeRepositorio>();


//  SERVICIOS HTTP (CLIENTE)

builder.Services.AddScoped<IChatServicio, ChatServicio>();
builder.Services.AddScoped<IChatMemberServicio, ChatMemberServicio>();
builder.Services.AddScoped<IHttpServicio, HttpServicio>();

//  HttpClient apuntando al servidor
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7016/")
});


//  BLAZOR

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();


//  MIDDLEWARE

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

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Proyecto2025.Server.Client._Imports).Assembly);


//  RUTAS DE CONTROLADORES

app.MapControllers();


//  HUBS DE SIGNALR

app.MapHub<MessageHub>("/messagehub");
app.MapHub<NotificationHub>("/notificationhub");


//  VERIFICAR CONEXIÓN A BD

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

try
{
    // Crea la base si no existe
    context.Database.EnsureCreated();
    Console.WriteLine("¡Conexión a la base de datos OK!");
}
catch (Exception ex)
{
    Console.WriteLine($"Error de conexión: {ex.Message}");
}


//  EJECUTAR LA APLICACIÓN

app.Run();
