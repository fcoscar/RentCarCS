using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RentCar.Application.Contract;
using RentCar.Application.Services;
using RentCar.Infraestructure.Context;
using RentCar.Infraestructure.Interfaces;
using RentCar.Infraestructure.Repositories;

namespace RentCar.IOC.Dependencies
{
    public static class ContextDependency
    {
        public static void AddRentCarDependency(this IServiceCollection services)
        {
            //Repositories
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<IAlquilerRepository, AlquilerRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            //Services
            services.AddTransient<ICarService, CarService>();
            services.AddTransient<IAlquilerService, AlquilerService>();
            services.AddTransient<IUserService, UserService>();
        }

        public static void AddContextDependecy(this IServiceCollection services, string connString)
        {
            services.AddDbContext<RentCarContext>(
                options => options
                    .UseMySql(connString, ServerVersion.AutoDetect(connString))
            );
        }
    }
}