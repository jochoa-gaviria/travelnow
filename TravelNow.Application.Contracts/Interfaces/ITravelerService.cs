using TravelNow.Business.Models.Traveler;
using TravelNow.Common.Models;

namespace TravelNow.Application.Contracts.Interfaces;

public interface ITravelerService
{
    /// <summary>
    /// Permite crear un pasajero
    /// </summary>
    /// <param name="createPassengerRequestDto"></param>
    /// <returns></returns>
    Task<ResponseDto<TravelerResponseBaseDto>> CreatePassenger(CreatePassengerRequestDto createPassengerRequestDto);
    
    /// <summary>
    /// Permite crear el contacto de emergencia de un pasajero
    /// </summary>
    /// <param name="createPassengerEmergencyContactRequestDto"></param>
    /// <returns></returns>
    Task<ResponseDto<TravelerResponseBaseDto>> CreatePassengerEmergencyContact(CreatePassengerEmergencyContactRequestDto createPassengerEmergencyContactRequestDto);
}
