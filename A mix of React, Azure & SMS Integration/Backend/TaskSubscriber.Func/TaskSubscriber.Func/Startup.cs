using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskSubscriber.Func.Configuration;
using TaskSubscriber.Func.Services;

[assembly: FunctionsStartup(typeof(TaskSubscriber.Func.Startup))]

namespace TaskSubscriber.Func
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddOptions<ApiSettings>()
               .Configure<IConfiguration>((settings, configuration) =>
               {
                   configuration.GetSection("ApiSettings").Bind(settings);
               });

            builder.Services.AddTransient<IMessagingService, MessagingService>();
        }
    }
}
