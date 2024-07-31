namespace core_web_api_fundamentals.api.Controllers;

public class CityDto
{
    public static CityDto FromDbDto(Database.CityDto city)
    {
        return new CityDto{Id = city.Id, Name = city.Name, Description = city.Description};
    }
    
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; } = null;

    public string TestCasePropertyName { get; } = string.Empty;
}