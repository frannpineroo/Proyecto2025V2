namespace Proyecto2025.Servicio.ChatServicioHttp
{
    public interface IChatServicio
    {
        Task<ChatRespuesta<T>> Get<T>(string url);
        Task<ChatRespuesta<Tresp>> Post<T, Tresp>(string url, T enviar);
        Task<ChatRespuesta<object>> Delete(string url);
    }
}
