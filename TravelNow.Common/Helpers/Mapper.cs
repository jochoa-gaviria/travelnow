using System.Reflection;
using TravelNow.Common.Attributes;

namespace TravelNow.Common.Helpers;

public static class Mapper
{
    public static TOutput TMapper<TOutput>(this object input)
    {
        var instance = Activator.CreateInstance(typeof(TOutput));
        List<PropertyInfo> inputProps = input.GetType().GetProperties().ToList();
        foreach (var inputProp in inputProps)
        {
            var data = instance.GetType().GetProperties().FirstOrDefault(x => x.Name.Equals(inputProp.Name));
            if (data != null && Equals(data.PropertyType, inputProp.PropertyType))
                data.SetValue(instance, inputProp.GetValue(input));
            MapperToAttribute attribute = (MapperToAttribute)inputProp.GetCustomAttribute(typeof(MapperToAttribute));
            if (attribute is not null)
            {
                var dataAttribute = instance.GetType().GetProperties().FirstOrDefault(x => x.Name.Equals(attribute.NameProperty));
                if (dataAttribute != null && Equals(dataAttribute.PropertyType, inputProp.PropertyType))
                    dataAttribute.SetValue(instance, inputProp.GetValue(input));
            }
        }
        return (TOutput)instance;
    }
}
