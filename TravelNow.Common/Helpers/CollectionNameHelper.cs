using TravelNow.Common.Enums;
using TravelNow.Common.Interfaces;

namespace TravelNow.Common.Helpers;

public class CollectionNameHelper : ICollectionNameHelper
{
    public Dictionary<ECollectionName, string> CollectionNames => new Dictionary<ECollectionName, string>
    {
        { ECollectionName.book, nameof(ECollectionName.book) },
        { ECollectionName.room, nameof(ECollectionName.room) },
        { ECollectionName.hotel, nameof(ECollectionName.hotel) },
        { ECollectionName.passerger, nameof(ECollectionName.passerger) },
        { ECollectionName.emergencyPasseger, nameof(ECollectionName.emergencyPasseger) }
    };
}
