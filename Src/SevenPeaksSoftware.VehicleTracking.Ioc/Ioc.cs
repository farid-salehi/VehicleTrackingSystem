
using Microsoft.Extensions.DependencyInjection;
using SevenPeaksSoftware.VehicleTracking.Application.Implementations;
using SevenPeaksSoftware.VehicleTracking.Application.Interfaces;
using SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces;
using SevenPeaksSoftware.VehicleTracking.Infrastructure.Implementations;

namespace SevenPeaksSoftware.VehicleTracking.Ioc
{
    public static class Ioc
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IVehicleTrackRepository, VehicleTrackRepository>();



            services.AddScoped<IUserService, UserService>();




            return services;
        }
    }
}
