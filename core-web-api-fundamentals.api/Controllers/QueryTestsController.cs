using System.Text.Json;
using System.Text.Json.Serialization;
using core_web_api_fundamentals.api.Controllers.QueryJson;
using core_web_api_fundamentals.api.Controllers.QueryTests;
using Microsoft.AspNetCore.Mvc;

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

        var listParams = Request.Query["list_params"].ToString();
        var listQueryParams = new ListQueryParams();

        if (!string.IsNullOrEmpty(listParams))
        {
            listQueryParams = WebSerializer.Deserialize<ListQueryParams>(listParams);
        }

        return Ok(WebSerializer.Serialize(listQueryParams));
    }

    [HttpGet("json-example")]
    public IActionResult GetJsonExample()
    {
        return Ok(WebSerializer.Serialize(ListQueryParams.Example));
    }

    private ActionResult testNull()
    {
        //why?!
        return null;
    }
}