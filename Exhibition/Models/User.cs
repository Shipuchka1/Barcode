using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exhibition.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string NameCompany { get; set; }
        public Country Country { get; set; }
        public City City { get; set; }
        [Required]
        public string ServiceStationAddress { get; set; }
        public string Activity { get; set; }
        public string Position { get; set; }
        public bool Status { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
    }
}