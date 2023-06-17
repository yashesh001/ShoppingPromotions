using app.shoppingpromotions.infrastructure.Interfaces;
using app.shoppingpromotions.infrastructure.Repositories;
using Enyim.Caching;
using Enyim.Caching.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;

namespace app.shoppingpromotions.infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabaseInfrastructure(this IServiceCollection serviceCollection
            , IConfiguration configuration
            , bool isDevelopmentEnvironment)
        {
            serviceCollection.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SQLServerConnection")));

            if (isDevelopmentEnvironment)
            {
                serviceCollection.AddSingleton<IProductRepository, StaticProductRepository>();
                serviceCollection.AddSingleton<IPointsPromotionRepository, StaticPointsPromotionRepository>();
                serviceCollection.AddSingleton<IProductDiscountPromotionRepository, StaticProductDiscountPromotionRepository>();
                serviceCollection.AddSingleton<ICacheWrapper, InMemoryCacheWrapper>();
            }
            else
            {
                // Bind Memcached options from appsettings.json
                serviceCollection.Configure<MemcachedOptions>(configuration.GetSection("Memcached"));

                // Register Memcached client
                serviceCollection.AddSingleton<IMemcachedClient>(provider =>
                {
                    MemcachedOptions options = provider.GetRequiredService<IOptions<MemcachedOptions>>().Value;
                    ILoggerFactory loggerFactory = provider.GetRequiredService<ILoggerFactory>();

                    var memcachedConfig = new MemcachedClientConfiguration(loggerFactory, provider.GetRequiredService<IOptions<MemcachedClientOptions>>(), configuration, null, null);
                    foreach (var server in options.Servers)
                    {
                        memcachedConfig.Servers.Add(new IPEndPoint(IPAddress.Parse(server.Address), server.Port));
                    }

                    return new MemcachedClient(loggerFactory, memcachedConfig);
                });

                // Register cache wrapper
                serviceCollection.AddSingleton<ICacheWrapper, MemcachedCacheWrapper>();

                serviceCollection.AddScoped<IProductRepository, ProductRepository>();
                serviceCollection.AddScoped<IPointsPromotionRepository, PointsPromotionRepository>();
                serviceCollection.AddScoped<IProductDiscountPromotionRepository, ProductDiscountPromotionRepository>();
            }

            serviceCollection.AddScoped<ICartRepository, CartRepository>();

            return serviceCollection;
        }
    }
}