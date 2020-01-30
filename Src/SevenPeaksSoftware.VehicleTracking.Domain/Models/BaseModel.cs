using System;



namespace SevenPeaksSoftware.VehicleTracking.Domain.Models
{
    public abstract class BaseModel
    {
        public DateTimeOffset CreatedDateTime { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset UpdatedDateTime { get; set; } = DateTimeOffset.Now;
        public bool IsDeleted { get; set; } = false;
    }
}
