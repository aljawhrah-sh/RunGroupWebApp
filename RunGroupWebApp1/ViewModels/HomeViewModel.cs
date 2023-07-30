using System;
using RunGroupWebApp1.Models;

namespace RunGroupWebApp1.ViewModels
{
	public class HomeViewModel
	{
		public IEnumerable<Club> Clubs { get; set; }
		public string City { get; set; }
		public string State { get; set; }
        public RegisterViewModel Register { get; set; } = new RegisterViewModel();
    }
}

