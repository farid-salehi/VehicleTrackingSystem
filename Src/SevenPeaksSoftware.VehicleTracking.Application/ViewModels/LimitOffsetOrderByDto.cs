using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SevenPeaksSoftware.VehicleTracking.Application.ViewModels
{
    public class LimitOffsetOrderByDto
    {
        public bool OrderByDescending { get; set; } = true;

        [Range(1, 30)]
        public int Limit { get; set; } = 10;


        [Range(0, int.MaxValue)]

        public int Offset { get; set; } = 0;
    }
}
