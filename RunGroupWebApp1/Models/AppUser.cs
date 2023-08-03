
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using RunGroupWebApp1.Models;
using System.Diagnostics;

namespace RunGroupWebApp1.Models
{
    public class AppUser : IdentityUser
    {   //moving rate
        public int? Pace { get; set; }
        //number of miles covered
        public int? Mileage { get; set; }

        public string? ProfileImageUrl { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }

        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public Address? Address { get; set; }

        public ICollection<Club> Clubs { get; set; }
        public ICollection<Race> Races { get; set; }


    }
}