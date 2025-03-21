using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

namespace Web;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.Services.AddMudServices();
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        builder.Services.AddSingleton<BasicConfiguration>(sp =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            return new BasicConfiguration(configuration);
        });

        RegisterMicroServices(builder);

        await builder.Build().RunAsync();
    }

    private static void RegisterMicroServices(WebAssemblyHostBuilder builder)
    {
        var config = new BasicConfiguration(builder.Configuration);

        builder!.Services.AddHttpClient(config!.IdentityApiName, client =>
        {
            client.BaseAddress = new Uri(config.IdentityApiUrl);
        });

        builder.Services.AddScoped<UniversalApiManager>();
    }
}
