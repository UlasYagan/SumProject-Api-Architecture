using System;
using Microsoft.Extensions.DependencyInjection;
using Sum.Domain.Entity;
using Sum.Repository.Base;
using Sum.Repository.Interface;
using Sum.Repository.Repository;

namespace Sum.Repository.ServiceExtension
{
    public static class SumRepositoryServiceExtension
    {
        public static void AddInjectionForSumRepositories(this IServiceCollection services)
        {
            services.AddTransient<IBaseCrudRepository<Products, int>, ProductRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();

            services.AddTransient<IBaseCrudRepository<Users, Guid>, UserRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}   