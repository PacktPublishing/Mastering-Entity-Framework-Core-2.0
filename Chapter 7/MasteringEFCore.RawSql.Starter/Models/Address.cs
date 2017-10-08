using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.RawSql.Starter.Models
{
    public class Address
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "FlatHouseInfo is required")]
        public string FlatHouseInfo { get; set; }
        [Required(ErrorMessage = "StreetName is required")]
        public string StreetName { get; set; }
        public string Locality { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }
        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }
        public string LatitudeLongitude { get; set; }
        [Required(ErrorMessage = "User is required")]
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
    }
}
