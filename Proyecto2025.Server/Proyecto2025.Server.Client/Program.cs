using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Proyecto2025.Servicio.ChatMemberServicioHttp;
using Proyecto2025.Servicio.ChatServicioHttp;
using Proyecto2025.Servicio.ServiciosHttp;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp =>
new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IHttpServicio, HttpServicio>();
builder.Services.AddScoped<IChatServicio, ChatServicio>();
builder.Services.AddScoped<IChatMemberServicio, ChatMemberServicio>();

await builder.Build().RunAsync();
