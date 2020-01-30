
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SevenPeaksSoftware.VehicleTracking.Application.Enums;
using SevenPeaksSoftware.VehicleTracking.Application.Interfaces;
using SevenPeaksSoftware.VehicleTracking.Domain.InfrastructureInterfaces.InMemoryInterfaces;

namespace SevenPeaksSoftware.VehicleTracking.Application.BackgroundTaskManagers
{
    public class TaskExecutorService : BackgroundService
    {
        private static readonly SemaphoreSlim Semaphore 
            = new SemaphoreSlim(1, 1);
        private readonly IServiceScopeFactory _serviceScopeFactory; 
        public TaskExecutorService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async  Task ExecuteAsync(CancellationToken cancellationToken)
        {

            while (!cancellationToken.IsCancellationRequested)
            {
                await Semaphore.WaitAsync(cancellationToken);
                try
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                       var inMemoryService = scope.ServiceProvider.GetRequiredService<IInMemoryRepository>();

                      var taskCount = await inMemoryService.TaskQueueInMemoryRepository
                           .TaskCount(ApplicationEnums.TaskQueueEnum.TrackVehicleQueue.ToString());

                      if (taskCount == 0)
                      {
                         await Task.Delay(1000, cancellationToken);
                          continue;
                      }

                       var vehicleTrackService =  scope.ServiceProvider
                           .GetRequiredService<IVehicleTrackService>();

                       await vehicleTrackService.TrackAsync(cancellationToken);
                    }
                }
                catch (Exception)
                {
                    //log
                }
                finally
                {
                    Semaphore.Release();
                }

            }

        }
    }
}
