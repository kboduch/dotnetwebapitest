using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Net.Mime;
using core_web_api_fundamentals.api.Database;
using CsvHelper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace core_web_api_fundamentals.api.Controllers;

/**
    ControllerBase adds Controller attribute only. ApiController attribute adds more functionality.
    For example, it forces the Route attribute on Controller actions via
    Microsoft.AspNetCore.Mvc.ApplicationModels.ApiBehaviorApplicationModelProvider.EnsureActionIsAttributeRouted

    Can be configured in Program.cs (top of the file) to annotate all *Controller classes
    using Microsoft.AspNetCore.Mvc;
    [assembly: ApiController]

    https://learn.microsoft.com/en-gb/aspnet/core/web-api/?view=aspnetcore-8.0#apicontroller-attribute
    Attribute routing requirement
    Automatic HTTP 400 responses - if the model is invalid, it will return ValidationProblem
    Binding source parameter inference - if no attribute defined it will fall back to a set of infer rules
    Multipart/form-data request inference
    Problem details for error status codes - use ValidationProblem instead of BadRequest for custom responses
 */
[ApiController]
[Route("city")]
public class CityController : ControllerBase
{
    /**
     * https://learn.microsoft.com/en-gb/aspnet/core/web-api/?view=aspnetcore-8.0#binding-source-parameter-inference
     * [FromQuery], [FromBody] etc. attributes can be used here
     * if method arg is not annotated, then the asp.net core will attempt to infer it in this order:
     * FromServices, FromBody, FromForm, FromRoute, FromQuery
     */
    [HttpGet]
    public async Task<ActionResult<List<CityDto>>> GetCities(
        // options.SuppressModelStateInvalidFilter = true; and name is missing, the name variable will be null, otherwise ValidationProblem
        string name = "any name",
        [FromQuery] bool returnCustomValidationProblem = false,
        [FromQuery] bool returnNotFound = false,
        [FromQuery] bool asCsv = false
    )
    {
        //[FromQuery] returnCustomValidationProblem it has to be 'true' or 'false' string
        if (returnCustomValidationProblem)
        {
            return ValidationProblem(
                title: "Custom error",
                detail: "some details",
                statusCode: 500 // this also changes the http response status!
            );
        }

        if (returnNotFound)
        {
            //options.SuppressMapClientErrors
            return NotFound();
        }

        //todo replace with DI
        var data = CityDataStore.Instance.Cities
            //this is method grouping -- Select(dto => CityDto.FromDbDto(dto))
            .Select(CityDto.FromDbDto)
            //xml format required the .ToList()
            .ToList();


        //not best practise given the Accept header (json,xml), but it's demo project
        if (asCsv)
        {
            //snippet from the csvHelper lib. What 'using' do? I'm guessing it disposes the object?
            // using (var writer = new StreamWriter(new MemoryStream()))
            // using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            // {
            //     csv.WriteRecords(data);
            // }
            // var stream = new MemoryStream();
            //  var writer = new StreamWriter(stream);
            //  var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            // csv.WriteRecords(data);
            // csv.Flush();

            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    await csv.WriteRecordsAsync(data);
                }

                return File(stream.ToArray(), "text/csv");
            }

            // return File(stream,"text/csv");
            // return File(stream.ToArray(),"text/csv");
        }

        return Ok(data);
    }


    //todo swagger does not register the 404 response as one of the possible responses
    [HttpGet("{id}")]
    public ActionResult<CityDto> GetCity(Guid id)
    {
        //todo replace with DI
        // var cityDto = CityDataStore.Instance.Cities
        //     .Where(dto => dto.Id.Equals(id))
        //     .Select(dto => new CityDto { Id = dto.Id, Name = dto.Name, Description = dto.Description })
        //     .SingleOrDefault();

        try
        {
            //todo find analogous solution to exception subscriber in symfony. Here I'm guessing it will be a middleware?
            return Ok(CityDto.FromDbDto(CityDataStore.Instance.GetCity(id)));
        }
        //should be more precise than that. I'm complicating the demo project again...
        catch (Exception)
        {
            return NotFound();
        }
    }

    [HttpPost]
    [Consumes("application/json")]
    public ActionResult PostCity([FromBody] PostCityRequestBody name)
    {
        // ModelState;
        //todo see the FluidValidation package for alternative validation
        return new JsonResult(name);
    }

    public class PostCityRequestBody
    {
        [Required]
        public string Name { get; set; }
    }
}