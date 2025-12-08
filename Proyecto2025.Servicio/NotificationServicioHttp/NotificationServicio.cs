using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Proyecto2025.Shared.DTO;
using System.Net.Http.Json;

namespace Proyecto2025.Servicio
{
    // Servicio que usa el cliente Http para llamar a la API de notificaciones.
    public class NotificationServicio : INotificationServicio
    {
        private readonly HttpClient _http;

        public NotificationServicio(HttpClient http)
        {
            _http = http;
        }

        // Devuelve las notificaciones pendientes de un usuario.
        public async Task<List<NotificationDTO>> ObtenerPendientes(int userId)
        {
            // Si la API falla, devuelvo una lista vacía para evitar errores en la UI.
            return await _http.GetFromJsonAsync<List<NotificationDTO>>(
                $"api/Notification/user/{userId}/pending"
            ) ?? new List<NotificationDTO>();
        }

        // Devuelve cuántas notificaciones pendientes tiene un usuario.
        public async Task<int> ObtenerCantidad(int userId)
        {
            try
            {
                return await _http.GetFromJsonAsync<int>(
                    $"api/Notification/count/{userId}"
                );
            }
            catch
            {
                return 0; // Si la API falla, no rompo la UI.
            }
        }

        // Crea una nueva notificación usando el endpoint de pruebas.
        public async Task Crear(NotificationDTO dto)
        {
            var response = await _http.PostAsJsonAsync("api/Notification/send-test", dto);

            // Verifico que el servidor haya respondido bien.
            response.EnsureSuccessStatusCode();
        }

        // Marca una notificación como leída.
        public async Task MarcarLeida(long id)
        {
            var response = await _http.PutAsync(
                $"api/Notification/{id}/markasread",
                null
            );

            response.EnsureSuccessStatusCode();
        }
    }
}
