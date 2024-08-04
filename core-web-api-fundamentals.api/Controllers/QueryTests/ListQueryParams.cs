using System.ComponentModel;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace core_web_api_fundamentals.api.Controllers.QueryTests;

public class ListQueryParams
{
    public static ListQueryParams Example { get; } = new()
    {
        AsFile = FileFormat.Csv,
        Order = new Dictionary<string, SortDirection>
        {
            { "department_name", SortDirection.Asc },
            { "owner_last_name", SortDirection.Desc }
        },
        Filters =
        [
            new Filter { Field = "department_name", Operator = "in", Value = new List<string> { @"ą / ? \ = @", "Json & sons" } },
            new Filter { Field = "department_name", Operator = "eq", Value = @"just a string ą / ? \ = @" },
        ],
        Test = @"ą / ? \ = @"
    };

    //todo remove this property
    public string Test { get; set; } = string.Empty;
    public FileFormat? AsFile { get; set; } = null;

    [JsonPropertyName("order")] public Dictionary<string, SortDirection> Order { get; set; } = [];

    [JsonPropertyName("filters")] public List<Filter> Filters { get; set; } = [];
}

public enum FileFormat
{
    Csv,
    Xls
}

public enum SortDirection
{
    Asc,
    Desc
}

//todo https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/polymorphism?pivots=dotnet-8-0#polymorphic-type-discriminators
public class Filter
{
    [JsonPropertyName("f")] public string Field { get; set; } = string.Empty;
    [JsonPropertyName("o")] public string Operator { get; set; } = string.Empty;
    [JsonPropertyName("v")] public object Value { get; set; } = new();
}