using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;


namespace Rocket_Elevators_Rest_API.Models
{
    public partial class Customer
    {



        [Key]
        public int id { get; set; }
        public string? EmailCompanyContact { get; set; }
        public string? CompanyName { get; set; }
        public string? FullNameCompanyContact { get; set; }


    }
}
