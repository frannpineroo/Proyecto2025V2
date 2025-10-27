using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Proyecto2025.Servicio.ChatMemberServicioHttp;
using Proyecto2025.Servicio.ChatServicioHttp;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7016") // tu servidor actual
});

builder.Services.AddScoped<IChatMemberServicio, ChatMemberServicio>();

builder.Services.AddScoped<IChatServicio, ChatServicio>();

await builder.Build().RunAsync();
