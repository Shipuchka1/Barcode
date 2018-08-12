using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exhibition.Models
{
    public class City
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}