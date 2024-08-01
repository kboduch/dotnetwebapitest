using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace core_web_api_fundamentals.api.Controllers.QueryTests;

public class ListQueryParams
{
    public bool AsFile { get; set; } = false;
    public string[] C { get; set; } = [];
    //todo resume here -- niech ta lista zawiera ladnie pogrupowane dane
    public Dictionary<string, Dictionary<string,string>> f { get; set; } = new ();
    // public Dictionary<department, Dictionary<o|v,(in|eq|gt)|(1|1,2)>> f { get; set; } = new ();
}

public class Filter
{
    public string o { get; set; } = "undefined";
}