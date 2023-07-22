
using Newtonsoft.Json;
using System.Net;

namespace TravelNow.Common.Models;

public class ResponseDto<T>
{

    #region properties

    /// <summary>
    /// Codigo respuesta
    /// </summary>
    public HttpStatusCode Code { get; set; } = HttpStatusCode.OK;


    /// <summary>
    /// Mensaje respuesta
    /// </summary>
    public string Message { get; set; } = "Success";


    /// <summary>
    /// Respuesta
    /// </summary>
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public T? Response { get; set; }

    #endregion properties


    #region methods

    /// <summary>
    /// Asigna la respuesta de clase generica <see cref="ResponseDto{T}"/>
    /// </summary>
    /// <param name="code">Codigo de respuesta</param>
    /// <param name="message">Mensaje de respuesta</param>
    /// <param name="response">Respuesta</param>
    public void SetProperties(HttpStatusCode code, string message, object? response = default)
    {
        Code = code;
        Message = message;

        if (response != null)
            Response = (T?)response;
    }

    #endregion methods
}
