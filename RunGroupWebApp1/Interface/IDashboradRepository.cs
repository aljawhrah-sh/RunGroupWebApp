using System;
using RunGroupWebApp1.Models;

namespace RunGroupWebApp1.Interface
{
	public interface IDashboradRepository
	{
		Task<List<Race>> GetAllUserRaces();
		Task<List<Club>> GetAllUserClubs();
		Task<AppUser> GetUserById(string id);
		Task<AppUser> GetByIdNoTracking(string id);
		bool Update(AppUser user);
		bool Save();
	}
}

