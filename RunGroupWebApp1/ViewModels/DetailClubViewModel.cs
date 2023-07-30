﻿using RunGroupWebApp1.Data.Enum;
using RunGroupWebApp1.Models;

namespace RunGroupWebApp1.ViewModels
{
    public class DetailClubViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
        public IFormFile Image { get; set; }
        public ClubCategory ClubCategory { get; set; }

    }
}
