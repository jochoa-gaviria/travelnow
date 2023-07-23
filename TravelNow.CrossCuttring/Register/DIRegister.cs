using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TravelNow.Application.Contracts.Interfaces;
using TravelNow.Application.Services;
using TravelNow.Common.Helpers;
using TravelNow.Common.Interfaces;
using TravelNow.DataAccess.Context;
using TravelNow.DataAccess.Contracts.Interfaces;

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
        services.AddTransient<IRoomService, RoomService>();
        services.AddTransient<ITravelerService, TravelerService>();
        services.AddTransient<IBookingService, BookingService>();
        services.AddSingleton<IEmailSenderService, EmailSenderService>();
        return services;
    }

    private static IServiceCollection AddRegisterRepositories(IServiceCollection services)
    {
        services.AddTransient<IDataBaseContextRepository, DataBaseContextRepository>();
        services.AddTransient<ICollectionNameHelper, CollectionNameHelper>();
        services.AddTransient<IAppConfigHelper, AppConfigHelper>();
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