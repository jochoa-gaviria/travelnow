using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TravelNow.Application.Contracts.Interfaces;
using TravelNow.Application.Services;

namespace TravelNow.CrossCuttring.Register;

public static class DIResgister
{
    public static IServiceCollection AddRegistration(this IServiceCollection services)
    {
        AddRegisterServices(services);
        AddRegisterRepositories(services);
        AddRegisterOthers();

        return services;
    }

    private static IServiceCollection AddRegisterServices(IServiceCollection services)
    {
        services.AddTransient<IHotelService, HotelService>();
        return services;
    }

    private static IServiceCollection AddRegisterRepositories(IServiceCollection services)
    {
        return services;
    }

    private static void AddRegisterOthers()
    {
        JsonConvert.DefaultSettings = () => new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver(),
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
    }

}