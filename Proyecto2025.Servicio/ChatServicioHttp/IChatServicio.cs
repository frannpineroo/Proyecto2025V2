namespace Proyecto2025.Servicio.ChatServicioHttp
{
    public interface IChatServicio
    {
        Task<ChatRespuesta<T>> GetEsAsync<T>(string url);
        Task<ChatRespuesta<Tresp>> Post<T, Tresp>(string url, T enviar);
        Task<ChatRespuesta<Tresp>> Put<T, Tresp>(string url, T enviar);
        Task<ChatRespuesta<object>> Delete(string url);
    }
}
