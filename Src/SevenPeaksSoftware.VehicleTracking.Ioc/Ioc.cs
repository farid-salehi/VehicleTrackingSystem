
using Microsoft.Extensions.DependencyInjection;
using SevenPeaksSoftware.VehicleTracking.Application.BackgroundTaskManagers;
using SevenPeaksSoftware.VehicleTracking.Application.Implementations;
using SevenPeaksSoftware.VehicleTracking.Application.Interfaces;
using SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces;
using SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces.InMemoryInterfaces;
using SevenPeaksSoftware.VehicleTracking.Infrastructure.Implementations;
using SevenPeaksSoftware.VehicleTracking.Infrastructure.Implementations.InMemoryImplementations;

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
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IVehicleTrackService, VehicleTrackService>();


            services.AddSingleton<IRedisConnectionFactory, RedisConnectFactory>();
            services.AddScoped<IInMemoryRepository, InMemoryRepository>();
            services.AddScoped<IRedisConnectionFactory, RedisConnectFactory>();
            services.AddScoped<ITaskQueueInMemoryRepository, TaskQueueInMemoryRepository>();
            services.AddScoped<IVehicleTrackInMemoryRepository, VehicleTrackInMemoryRepository>();
            services.AddScoped<ILocationIqThirdPartyService, LocationIqThirdPartyService>();

            services.AddHostedService<TaskExecutorService>();

            return services;
        }
    }
}
