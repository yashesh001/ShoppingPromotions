using app.shoppingpromotions.infrastructure.Interfaces;
using app.shoppingpromotions.infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
                serviceCollection.AddSingleton<IDiscountPromotionRepository, StaticDiscountPromotionRepository>();
            }
            else
            {
                serviceCollection.AddScoped<IProductRepository, ProductRepository>();
                serviceCollection.AddScoped<IPointsPromotionRepository, PointsPromotionRepository>();
                serviceCollection.AddScoped<IDiscountPromotionRepository, DiscountPromotionRepository>();
            }

            return serviceCollection;
        }
    }
}