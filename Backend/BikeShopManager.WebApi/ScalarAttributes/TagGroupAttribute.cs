namespace BikeShopManager.WebApi.ScalarAttributes;

public class TagGroupAttribute(string name) : Attribute
{
    public string Name { get; } = name;
}
