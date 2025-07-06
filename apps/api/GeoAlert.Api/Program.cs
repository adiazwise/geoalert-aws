var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapGet("/health", () => "The GeoAlert API is running!");

app.MapHealthChecks("/health-check");
app.Run();
