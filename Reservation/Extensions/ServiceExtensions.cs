using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities;
using LoggerService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;



namespace Reservation.Extensions
{
    public static class ServiceExtensions
    {


        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
        }


        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }


        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<RepositoryContext>(o => o.UseSqlServer(connectionString));

            //services.AddIdentity<User, IdentityRole>(opt =>
            //    {
            //        opt.Password.RequiredLength = 7;
            //        opt.Password.RequireDigit = false;
            //        opt.Password.RequireUppercase = false;
            //        opt.User.RequireUniqueEmail = true;

            //    })
            //    .AddEntityFrameworkStores<RepositoryContext>()
            //    .AddDefaultTokenProviders()
            //    .AddRoles<IdentityRole>();
            //services.AddScoped<IUserClaimsPrincipalFactory<User>, CustomClaimsFactory>();

        }


        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }


    }
}
