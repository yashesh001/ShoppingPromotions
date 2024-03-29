﻿using app.shoppingpromotions.host.Services;
using app.shoppingpromotions.infrastructure;

namespace app.shoppingpromotions.host
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services
            , IConfiguration configuration
            , bool isDevelopmentEnvironment)
        {
            return services
                .AddScoped<IShoppingBasketService, ShoppingBasketService>()
                .AddScoped<IDiscountService, DiscountService>()
                .AddScoped<IPointsService, PointsService>()
                .AddHttpClient()
                .AddOptions()
                .AddHttpContextAccessor()
                .AddDatabaseInfrastructure(configuration, isDevelopmentEnvironment);
        }
    }
}
