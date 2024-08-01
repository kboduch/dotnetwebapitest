namespace core_web_api_fundamentals.api.Controllers.QueryJson;


public enum LogicEnum
{
    And,
    Or
}

public class RootFilter
{
    public List<Filter> Filters { get; set; } = [];

    public LogicEnum LogicE { get; set; } = LogicEnum.And;
    public string Logic { get; set; } = string.Empty;
}

public class Filter
{
    public string Field { get; set; } = string.Empty;
    public string Operator { get; set; } = string.Empty;
    public object Value { get; set; } = new();
    public string Logic { get; set; } = string.Empty; // This is the nested "logic" property for the inner filters array
    public List<Filter> Filters { get; set; } = []; // Nested filters array
}
