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
                options.DatabaseName = configuration.GetSection("ClientDatabase:DatabaseName").Value;
                options.ClientCollectionName = configuration.GetSection("ClientDatabase:CollectionName").Value;
                options.ConnectionString = configuration.GetSection("ClientDatabase:ConnectionString").Value; 
            });

            services.Configure<MongoDatabaseSettings>(options => configuration.GetSection("ClientDatabase"));


            return services;
        }

    }
}

