using AutoMapper;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels.User;
using SevenPeaksSoftware.VehicleTracking.Domain.Models;

namespace SevenPeaksSoftware.VehicleTracking.Application.AutoMapperConfigs
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperServices()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<InputAddUserDto, UserModel>();
                cfg.CreateMap<UserModel, OutputUserDto>();

            });
        }
    }
}
