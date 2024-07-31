using core_web_api_fundamentals.api.Controllers.QueryTests;
using Microsoft.AspNetCore.Mvc;

namespace core_web_api_fundamentals.api.Controllers;

[ApiController]
[Route("query")]
public class QueryTestsController : ControllerBase
{
    [HttpGet]
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
}