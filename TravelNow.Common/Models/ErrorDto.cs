
using Newtonsoft.Json;

namespace TravelNow.Common.Models;

public class ErrorDto
{
    #region properties
    /// <summary>
    /// Codigo de respuesta
    /// </summary>
    public string? Code { get; set; }


    /// <summary>
    /// Mensaje de respuesta
    /// </summary>
    public string? Message { get; set; }


    /// <summary>
    /// Colección de errores
    /// </summary>
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public Dictionary<string, List<string>>? Errors { get; set; }

    #endregion properties


    #region constructor
    /// <summary>
    /// Constructor ErrorDto <see cref="ErrorDto"/>
    /// </summary>
    /// <param name="code"></param>
    /// <param name="message"></param>
    public ErrorDto(string? code = default, string? message = default)
    {
        Code = code;
        Message = message;
    }

    #endregion constructor 
}
