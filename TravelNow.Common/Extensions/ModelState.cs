using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using TravelNow.Common.Models;

namespace TravelNow.Common.Extensions;

public static class ModelState
{
    public static void AddModelState(this IServiceCollection services)
    {
        services.AddMvcCore().ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = (context => SetModelState(context.ModelState));
        });
    }

    private static ObjectResult SetModelState(ModelStateDictionary modelState)
    {
        ObjectResult? result = default;
        ErrorDto errorDto = new ErrorDto(HttpStatusCode.BadRequest.GetHashCode().ToString(), "Modelo Inválido - Errores en el modelo Request")
        {
            Errors = new()
        };


        foreach(var item in modelState)
        {
            IEnumerable<string> errorMessages = item.Value.Errors.Select(error => error.ErrorMessage);
            List<string> validErrorMessages = new();

            validErrorMessages.AddRange(errorMessages.Where(message => !string.IsNullOrEmpty(message)));

            if (validErrorMessages.Any())
            {
                if (errorDto.Errors.ContainsKey(item.Key))
                    errorDto.Errors[item.Key].AddRange(validErrorMessages);
                else
                    errorDto.Errors.Add(item.Key, validErrorMessages.ToList());
            }
        }

        if (errorDto.Errors.Any())
            result = new ObjectResult(errorDto) { StatusCode = (int)HttpStatusCode.BadRequest };

        return result;
    }
}
