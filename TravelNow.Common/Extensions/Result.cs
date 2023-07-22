
using Microsoft.AspNetCore.Mvc;
using TravelNow.Common.Models;

namespace TravelNow.Common.Extensions;

public static class Result
{
    /// <summary>
    /// Ejecuta un resultado de un request HTTP
    /// </summary>
    /// <typeparam name="T">Clase asignada para la respuesta</typeparam>
    /// <param name="modelResponse">Modelo para respuesta</param>
    /// <returns></returns>
    public static ObjectResult ExecuteResult<T>(ResponseDto<T> modelResponse)
    {
        ObjectResult result;
        int httpStatusCode = modelResponse.Code.GetHashCode();

        if (modelResponse != null && modelResponse.Response != null) 
            result = new ObjectResult(modelResponse.Response) { StatusCode = httpStatusCode };
        else if (!string.IsNullOrEmpty(modelResponse?.Message))
            result = new ObjectResult(new ErrorDto(httpStatusCode.ToString(), modelResponse.Message)) { StatusCode = httpStatusCode };
        else 
            result = new ObjectResult(default) { StatusCode = httpStatusCode };

        return result;
    }
}
