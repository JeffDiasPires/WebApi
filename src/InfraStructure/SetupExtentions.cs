using Infra;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SetupExtentions
    {
        public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoClusterDatabaseSettings>(options =>
            {
                options.DatabaseName = configuration.GetSection("ClientClusterDatabase:DatabaseName").Value;
                options.ClientCollectionName = configuration.GetSection("ClientClusterDatabase:CollectionName").Value;
                options.ConnectionString = configuration.GetSection("ClientClusterDatabase:ConnectionString").Value; 
            });

            return services;
        }

    }
}

