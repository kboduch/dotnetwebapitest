namespace core_web_api_fundamentals.api.Database;

public class CityDataStore
{
    public List<CityDto> Cities { get; }
    public static CityDataStore Instance { get; } = new();

    public CityDto GetCity(Guid id)
    {
        var city = Cities.SingleOrDefault(city => city.Id.Equals(id));

        if (city == null)
        {
            throw new NotFound<CityDto>(id);
        }

        return city;
    }

    private CityDataStore()
    {
        Cities = new List<CityDto>()
        {
            new CityDto
            {
                Id = Guid.Parse("b3e5ba9d-4907-4779-a5ba-9d4907d7795b"),
                Name = "New New York",
                Description = "New New York is a major city on the Planet Earth located in the State of New New York," +
                              " which is built directly on top of the ruins of Old New York, which is still" +
                              " accessible via the New New York Sewer System."
            },
            new CityDto
            {
                Id = Guid.Parse("4ceb8b9d-bc99-4a92-ab8b-9dbc997a9243"),
                Name = "Future-Roma",
                Description = "Future-Roma is a city in Italy. It is home to Vatican City and the Space Pope, " +
                              "as well as many other well-known locations."
            },
            new CityDto
            {
                Id = Guid.Parse("ab853bf2-f271-4aa5-853b-f2f271daa5dc"),
                Name = "Mars Vegas",
                Description = "Mars Vegas is the martian Las Vegas with everything the real Las Vegas has: casinos," +
                              " expensive hotels, night clubs and roller coasters."
            },
        };
    }
}