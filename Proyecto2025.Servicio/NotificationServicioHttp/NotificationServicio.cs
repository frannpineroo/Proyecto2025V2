using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto2025.Shared.DTO;
using System.Net.Http.Json;

public class NotificationServicio : INotificationServicio
{
    private readonly HttpClient _http;

    public NotificationServicio(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<NotificationDTO>> ObtenerPendientes(int userId)
    {
        return await _http.GetFromJsonAsync<List<NotificationDTO>>(
            $"api/Notification/user/{userId}/pending"
        );
    }

    public async Task<int> ObtenerCantidad(int userId)
    {
        return await _http.GetFromJsonAsync<int>(
            $"api/Notification/count/{userId}"
        );
    }

    public async Task Crear(NotificationDTO dto)
    {
        await _http.PostAsJsonAsync("api/Notification/send-test", dto);
    }

    public async Task MarcarLeida(long id)
    {
        await _http.PutAsync($"api/Notification/{id}/markasread", null);
    }
}
