using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RentCar.Infraestructure.Context;
using RentCar.Infraestructure.Interfaces;
using RentCar.Infraestructure.Repositories;

namespace RentCar.IOC.Dependencies
{
    public static class ContextDependency
    {
        public static void AddRepoDependency(this IServiceCollection services) 
        {
            //Repositories
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<IAlquilerRepository, AlquilerRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

        }
    
        public static void AddContextDependecy(this IServiceCollection services, string connString)
        {
            services.AddDbContext<RentCarContext>(
                options => options
                    .UseMySql(connString, ServerVersion.AutoDetect(connString))
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
                );
        }
    }
}