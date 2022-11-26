using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Rocket_Elevators_Rest_API.Models

{
    public class Intervention
    {

        public long Id { get; set; }
        public long? Author { get; set; }
        public long? CustomerID { get; set; }
        public long? BuildingID { get; set; }
        public long? BatteryID { get; set; }
        public long? ColumnID { get; set; }
        public long? ElevatorID { get; set; }
        public long? EmployeeID { get; set; }
        public DateTime? StartDate { get; set; } = null!;
        public DateTime? EndDate { get; set; } = null!;
        public string? Result { get; set; }
        public string? Report { get; set; }
        public string? Status { get; set; }



    }
}
