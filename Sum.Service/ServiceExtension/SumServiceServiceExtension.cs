using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sum.Domain.Entities;
using Sum.Model.Feature;
using Sum.Service.Base;
using Sum.Service.Cache;
using Sum.Service.Features;
using Sum.Service.Interface;
using Sum.Service.Service;

namespace Sum.Service.ServiceExtension
{
   public static class SumServiceServiceExtension
    {
        public static void AddInjectionForSumServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddTransient<IFileService, FileService>();

            services.AddTransient<IBaseCrudService<Products, int>, ProductService>();
            services.AddTransient<IProductService, ProductService>();

            services.AddTransient<IBaseCrudService<Users, Guid>, UserService>();
            services.AddTransient<IUserService, UserService>();


            var redisSettings = new RedisCacheSettings();
            configuration.Bind(nameof(redisSettings), redisSettings);
            services.AddSingleton(redisSettings);

            if (!redisSettings.Enabled)
            {
                return;
            }

            services.AddDistributedRedisCache(options => options.Configuration = redisSettings.ConnectionString);
            services.AddSingleton<ICacheService, CacheService>();
        }
    }
}
