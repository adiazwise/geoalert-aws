
using Amazon.Lambda.AspNetCoreServer;
using GeoAlert.Api.Endpoints;
using GeoAlert.Api.Services;

var builder =  WebApplication.CreateBuilder(args);


builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);
builder.Services.AddGeofencesServices();

var app = builder.Build();

app.AddMapGeoFences();

app.Run();

 