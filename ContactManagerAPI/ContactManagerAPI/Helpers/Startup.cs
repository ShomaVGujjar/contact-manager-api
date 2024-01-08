using ContactManagerAPI.Helpers;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

[assembly: FunctionsStartup(typeof(Startup))]
namespace ContactManagerAPI.Helpers
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddFilter(level => true);
            });

            var config = builder.GetContext().Configuration;            

            //var config = (IConfiguration)builder.Services.FirstOrDefault(d => d.ServiceType == typeof(IConfiguration))?.ImplementationInstance;

            builder.Services.AddSingleton((s) => 
            {
                CosmosClientBuilder cosmosClientBuilder = new CosmosClientBuilder(config[Settings.COSMOS_DB_CONNECTION_STRING]);

                return cosmosClientBuilder.WithConnectionModeDirect()
                    .Build();
            });
        }
    }
}
