using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;


namespace Rocket_Elevators_Rest_API.Models
{
    public partial class Battery
    {


        [Key]
        public int id { get; set; }
        public string? status { get; set; }
        public int building_id { get; set; }
        public DateTime? date_last_inspection { get; set; }
        public string? battery_type { get; set; }
        public string? information { get; set; }
        public string? notes { get; set; }


    }
}
