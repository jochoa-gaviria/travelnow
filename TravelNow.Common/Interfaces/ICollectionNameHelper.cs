using TravelNow.Common.Enums;

namespace TravelNow.Common.Interfaces;

public interface ICollectionNameHelper
{
    Dictionary<ECollectionName, string> CollectionNames { get; }
}
