using Amazon.Lambda.AspNetCoreServer;


namespace GeoAlert.Api;

public class LambdaEntryPoint : APIGatewayProxyFunction
{
    protected override void Init(IWebHostBuilder builder)
    {
        builder
        .ConfigureAppConfiguration((context, config) =>
        {
            config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        })
        .UseStartup<Startup>();
    }
}
