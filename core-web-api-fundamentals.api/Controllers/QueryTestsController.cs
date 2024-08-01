using core_web_api_fundamentals.api.Controllers.QueryJson;
using core_web_api_fundamentals.api.Controllers.QueryTests;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace core_web_api_fundamentals.api.Controllers;

[ApiController]
[Route("query")]
public class QueryTestsController : ControllerBase
{
    [HttpGet("standard")]
    public ActionResult QueryValidationTest(
        [FromQuery] ListQueryParams listQueryParams,
        [FromQuery] bool AsFile
    )
    {
        var query = Request.QueryString;
        var query2 = Request.Query;
        var lqp = listQueryParams;

        var a = 1;

        return Ok();
    }

    [HttpGet("json")]
    public IActionResult QueryValueAsJson()
    {
        var testNull = this.testNull();
        
        var filter = Request.Query["filter"].ToString();
        var filterResult = new RootFilter();
        
        if (!string.IsNullOrEmpty(filter))
        {
            filterResult = JsonConvert.DeserializeObject<RootFilter>(filter);
        }
        
        return Ok();
    }

    private ActionResult testNull()
    {
        //why?!
        return null;
    }
}