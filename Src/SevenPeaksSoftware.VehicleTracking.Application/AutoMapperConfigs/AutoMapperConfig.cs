using AutoMapper;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels.LocationIq;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels.Track;
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

                cfg.CreateMap<OutputReverseGeoCodingDto, PointDetailDto>()
                    .ForMember(dest => dest.AddressText,
                        act => act.MapFrom(src => src.DisplayName))

                    .ForMember(dest => dest.City,
                        act => act.MapFrom(src => src.Address.City))

                    .ForMember(dest => dest.Country,
                        act => act.MapFrom(src => src.Address.Country))

                    .ForMember(dest => dest.CountryCode,
                        act => act.MapFrom(src => src.Address.CountryCode))

                    .ForMember(dest => dest.County,
                        act => act.MapFrom(src => src.Address.County))

                    .ForMember(dest => dest.Neighbourhood,
                        act => act.MapFrom(src => src.Address.Neighbourhood))

                    .ForMember(dest => dest.Postcode,
                        act => act.MapFrom(src => src.Address.Postcode))

                    .ForMember(dest => dest.Road,
                        act => act.MapFrom(src => src.Address.Road))

                    .ForMember(dest => dest.State,
                        act => act.MapFrom(src => src.Address.State))

                    .ForMember(dest => dest.Suburb,
                        act => act.MapFrom(src => src.Address.Suburb));

            });
        }
    }
}
