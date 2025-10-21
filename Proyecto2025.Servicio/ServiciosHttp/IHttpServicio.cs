
namespace Proyecto2025.Servicio.ServiciosHttp
{
    public interface IHttpServicio
    {
        Task<HttpRespuesta<object>> Delete(string url);
        Task<HttpRespuesta<T>> Get<T>(string url);
        Task<HttpRespuesta<TResp>> Post<T, TResp>(string url, T entidad);
    }
}