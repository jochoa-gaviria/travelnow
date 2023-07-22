
namespace TravelNow.Common.Attributes;

public partial class MapperToAttribute : Attribute
{
    public string NameProperty { get; set; }
    public MapperToAttribute(string nameProperty)
    {
        NameProperty = nameProperty;
    }
}
