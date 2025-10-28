using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Proyecto2025.Servicio.ServiciosHttp;
using Proyecto2025.Servicio.ChatServicioHttp;
using Proyecto2025.Servicio.ChatMemberHttp;
using Proyecto2025.Servicio.ChatMemberServicioHttp;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp =>
new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IChatMemberServicio, ChatMemberServicio>();
builder.Services.AddScoped<IHttpServicio, HttpServicio>();

await builder.Build().RunAsync();
