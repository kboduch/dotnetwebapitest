namespace core_web_api_fundamentals.api.Database;

public class CityDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = String.Empty;
    public string? Description { get; init; } = null;
}