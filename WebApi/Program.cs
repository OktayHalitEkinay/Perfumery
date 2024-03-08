using Autofac;
using Serilog;
using AutoMapper;
using DataAccess.Concrete;
using Business.Middlewares;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Business.DependencyResolvers.Autofac;
using Autofac.Extensions.DependencyInjection;

IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();


var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Services.AddControllers()
    .AddJsonOptions(i => i.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve)
    .AddOData(conf =>
    {
        conf.EnableQueryFeatures();
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PerfumeryContext>((options) =>
{
    options.UseSqlServer(configuration["SqlServerConnectionString"]);
    options.EnableSensitiveDataLogging();
});
builder.Services.AddSession();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Services.AddDistributedMemoryCache();
builder.Services.AddCors();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(
   builder => builder.RegisterModule(new AutofacBusinessModule()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSession();
app.UseMiddleware<CheckUserSessionMiddleware>();

app.UseCors(builder => builder.WithOrigins("http://localhost:5107").AllowAnyHeader());
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
