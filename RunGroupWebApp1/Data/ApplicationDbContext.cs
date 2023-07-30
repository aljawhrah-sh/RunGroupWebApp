
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using RunGroupWebApp1.Models;


namespace RunGroupWebApp1.Data
{
    //database pulling allowance
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Race> Races { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Address> Address { get; set; }
        
    }

}


