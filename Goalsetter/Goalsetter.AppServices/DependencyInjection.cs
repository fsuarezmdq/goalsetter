using Goalsetter.AppServices.Clients;
using Goalsetter.AppServices.Rentals;
using Goalsetter.AppServices.Utils;
using Goalsetter.AppServices.Vehicles;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanArchitecture.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton(new Config(3));
            services.AddSingleton<IMessages, Messages>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<IRentalService, RentalService>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IClientService, ClientService>();

            return services;
        }
    }
}
