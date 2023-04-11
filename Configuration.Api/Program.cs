using Configuration.Api;
using Configuration.Common.Exceptions;
using Configuration.RepositoryHandler.MsSql.EF.CoreSql;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSqlEfRepositoryServices(builder.Configuration);
builder.Services.ServiceCollectionExtension();

builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true).AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//for Globally Hand Data annotation and application level validations
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = ctx => new ValidationProblemDetailsResultHandler();
});

//builder.Services.AddHealthChecks().AddCheck("Configuration Health Check", () => { return HealthCheckResult.Degraded(); });
//builder.Services.AddHealthChecks().AddCheck<ConfigurationHealthCheck>("Configuration Health Check v2");
//builder.Services.AddHealthChecks().AddTypeActivatedCheck<ConfigurationHealthCheckWithParam>("Configuration API Health Check v3", args: new object[] { "This is param" });

//builder.Services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>("DB Health Check");
//builder.Services.AddHealthChecks().AddCheck<CustomHealthCheck>(nameof(CustomHealthCheck));

//builder.Services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>("Configuration API DB Health Check V3",
//    customTestQuery: async (ApplicationDbContext context, CancellationToken CancellationToken) =>
//    {
//        return await Task.FromResult(context.ConfigtblVerticalMaster.Count() > 1);
//    });


var app = builder.Build();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

/*
app.MapHealthChecks("/healthz", new HealthCheckOptions
{
    Predicate = healthchCheck => healthchCheck.Name == "Configuration API Health Check v3",
    AllowCachingResponses = false,
    ResponseWriter = ConfigurationHealthCheckWithParam.WriteResponse
});

app.MapHealthChecks("/healthzDb", new HealthCheckOptions
{
    Predicate = healthchCheck => healthchCheck.Name == "Configuration API DB Health Check V3",
    AllowCachingResponses = false,
    ResponseWriter = ConfigurationHealthCheckWithParam.WriteResponse
});


app.MapGet("/GetIP", (HttpContext context) =>
{
    var remoteIp = context.Connection.RemoteIpAddress;
    string clientIp = "";
    if (remoteIp != null)
    {
        clientIp = remoteIp.ToString();
    }
    context.Response.WriteAsync($"Ip Address: {clientIp}");
});

app.MapGet("/GetAppMemoryUsage", (HttpContext context) =>
{
    //string prcName = Process.GetCurrentProcess().ProcessName;
    //var counter_Exec = new PerformanceCounter("Process", "Working Set - Private", prcName);
    //double dWsp_Exec = (double)counter_Exec.RawValue / 1024.0; < ---that is the value in KB
    var memory = 0.0;
    Process proc = Process.GetCurrentProcess();
    memory = Math.Round(Convert.ToDouble(proc.PrivateMemorySize64 / (1024 * 1024)), 2);
    proc.Dispose();
});
*/

app.Run();
