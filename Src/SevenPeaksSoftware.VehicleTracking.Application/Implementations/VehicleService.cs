using System;
using System.Threading;
using System.Threading.Tasks;
using SevenPeaksSoftware.VehicleTracking.Application.ApplicationUtils;
using SevenPeaksSoftware.VehicleTracking.Application.Enums;
using SevenPeaksSoftware.VehicleTracking.Application.Interfaces;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels.Vehicle;
using SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces;
using SevenPeaksSoftware.VehicleTracking.Domain.Models;

namespace SevenPeaksSoftware.VehicleTracking.Application.Implementations
{
    public class VehicleService : IVehicleService
    {
        private readonly IUnitOfWork _unitOfWork;
       


        public VehicleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
       
        }

        public async Task<ResponseDto<OutputRegisterVehicleDto>> RegisterVehicleAsync(InputVehicleDto vehicle,
            CancellationToken cancellationToken)
        {
            if ((await _unitOfWork.VehicleRepository.GetVehicle(vehicle.VehicleRegistrationNumber,
                    cancellationToken)) != null)
            {
                return ResponseDto<OutputRegisterVehicleDto>.UnsuccessfulResponse
                    (ResponseEnums.ErrorEnum.Conflict, "The vehicle already exists.");
            }

            var password = Guid.NewGuid().ToString().Substring(0, 8);
            var hashPasswordArray = password.HashPassword();

            var newVehicle = new VehicleModel()
            {
                VehicleRegistrationNumber = vehicle.VehicleRegistrationNumber,
                Password = hashPasswordArray.Item1,
                Salt = hashPasswordArray.Item2
            };

            await _unitOfWork.VehicleRepository.AddAsync(newVehicle,
                cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

            return ResponseDto<OutputRegisterVehicleDto>.SuccessfulResponse(new OutputRegisterVehicleDto()
            {
                  VehicleRegisterNumber = newVehicle.VehicleRegistrationNumber,
                  Password = password
            });
        }

        public async Task<ResponseDto<OutputRegisterVehicleDto>> GetVehicleNewPassword(InputVehicleDto vehicle,
            CancellationToken cancellationToken)
        {
            var vehicleModel = await _unitOfWork.VehicleRepository
                .GetVehicle(vehicle.VehicleRegistrationNumber,
                    cancellationToken);
            if (vehicleModel == null)
            {
                return ResponseDto<OutputRegisterVehicleDto>.UnsuccessfulResponse
                    (ResponseEnums.ErrorEnum.Conflict, "Vehicle is not found.");
            }


            _unitOfWork.VehicleRepository.Remove(vehicleModel);

            var password = Guid.NewGuid().ToString().Substring(0, 8);
            var hashPasswordArray = password.HashPassword();

            var newVehicle = new VehicleModel()
            {
                VehicleRegistrationNumber = vehicle.VehicleRegistrationNumber,
                Password = hashPasswordArray.Item1,
                Salt = hashPasswordArray.Item2
            };

            await _unitOfWork.VehicleRepository.AddAsync(newVehicle,
                cancellationToken);

            await _unitOfWork.CommitAsync(cancellationToken);

            return ResponseDto<OutputRegisterVehicleDto>.SuccessfulResponse(new OutputRegisterVehicleDto()
            {
                VehicleRegisterNumber = newVehicle.VehicleRegistrationNumber,
                Password = password
            });
        }


    }
}
