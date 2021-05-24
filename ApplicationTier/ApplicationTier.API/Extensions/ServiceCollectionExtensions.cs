using ApplicationTier.Domain.Context;
using ApplicationTier.Domain.Entities.Services;
using ApplicationTier.Domain.Interfaces;
using ApplicationTier.Domain.Models;
using ApplicationTier.Infrastructure;
using ApplicationTier.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationTier.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddDbContext<DemoContext>(options =>
            {
                options.UseSqlServer(AppSettings.ConnectionString, sqlOptions => sqlOptions.CommandTimeout(120));
                options.UseLazyLoadingProxies();
            });

            services.AddScoped<Func<DemoContext>>((provider) => () => provider.GetService<DemoContext>());
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<DbFactory>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IActorService, ActorService>();

            return services;
        }

        public static IServiceCollection AddCORS(this IServiceCollection services)
        {
            return // CORS
                services.AddCors(options => {
                    options.AddPolicy("CorsPolicy",
                        builder => {
                            builder.WithOrigins(AppSettings.CORS)
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                .AllowCredentials();
                        });
                });
        }
    }
}
