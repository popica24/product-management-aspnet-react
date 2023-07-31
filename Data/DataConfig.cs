using Business.Contracts;
using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;


namespace Data
{
    public static class DataConfig
    {
        public static void ApplyServices(this IServiceCollection services)
        {
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICategoryProductRepository, CategoryProductRepository>();
            services.AddTransient<IUserRepository,UserRepository>();
            
        }
    }
}
