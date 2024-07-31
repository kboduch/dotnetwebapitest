//This file will be used in a main function.
//Because of the top-level statement there is no need to define class and the main method.

//This will apply ApiController attribute to all *Controller classes
// using Microsoft.AspNetCore.Mvc;
// [assembly: ApiController]

using System.Threading.RateLimiting;
using core_web_api_fundamentals.api;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddMiddlewareAnalysis();

var tpToRps = "up_to_100_rps";

builder.Services.AddHealthChecks()
    .AddCheck("a_check", new MyHealthCheck(HealthCheckResult.Degraded()), HealthStatus.Unhealthy)
    .AddCheck("b_check", new MyHealthCheck(HealthCheckResult.Healthy()), HealthStatus.Unhealthy)
    ;

// builder.Services.AddRateLimiter(options =>
// {
//     options.AddFixedWindowLimiter(policyName: tpToRps, limiterOptions =>
//     {
//         limiterOptions.PermitLimit = 2;
//         limiterOptions.Window = TimeSpan.FromSeconds(1);
//         limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
//         limiterOptions.QueueLimit = 2;
//     });
// });


builder.Services.AddHttpLogging(options => { });

// Add services to the container.
builder.Services.AddProblemDetails(options => { options.CustomizeProblemDetails = context => { context.ProblemDetails.Extensions.Add("server", "local"); }; });

//AddControllers is an extension that returns IMvcBuilder
//ConfigureApiBehaviorOptions is IMvcBuilder extension
builder.Services.AddControllers(options =>
    {
        options.ReturnHttpNotAcceptable = true;
    })
    //support for xml format, just like that... default is json format only
    .AddXmlDataContractSerializerFormatters()
    .ConfigureApiBehaviorOptions(options =>
    {
        // An example of how to log 
        // To preserve the default behavior, capture the original delegate to call later.
        var builtInFactory = options.InvalidModelStateResponseFactory;

        // more on this here https://learn.microsoft.com/en-gb/aspnet/core/web-api/?view=aspnetcore-8.0#disable-automatic-400-response
        // parameter source inferring rules can be configured here among other things 
        // options.X

        // options.SuppressModelStateInvalidFilter = true;
        // options.SuppressMapClientErrors = true;

        options.InvalidModelStateResponseFactory = context =>
        {
            var logger = context.HttpContext.RequestServices
                .GetRequiredService<ILogger<Program>>();

            logger.LogInformation("Auto 400 response has happened");

            // Invoke the default behavior, which produces a ValidationProblemDetails
            // response.
            // To produce a custom response, return a different implementation of 
            // IActionResult instead.
            return builtInFactory(context);
        };
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseHttpLogging();

// app.UseRateLimiter();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
 //todo explore this
    // app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//This is what app.MapControllers(); look like in previous versions
// app.UseRouting();
// app.UseAuthorization();
// app.UseEndpoints(routeBuilder =>
// {
//     routeBuilder.MapControllers();
// });
app.MapControllers()
    // .RequireRateLimiting(tpToRps)
    ;
app.UseHealthChecks("/status");


app.Run();