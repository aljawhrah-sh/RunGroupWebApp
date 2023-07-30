using Microsoft.EntityFrameworkCore;
using RunGroupWebApp1.Data;
using RunGroupWebApp1.Interface;
using RunGroupWebApp1.Models;

namespace RunGroupWebApp1.Repsitoery
{
    public class ClubRepository : IClubReposiroty
    {
        private readonly ApplicationDbContext _context;
        public ClubRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Club club)
        {
            _context.Add(club);
            return Save();
        }

        public bool delete(Club club)
        {
            _context.Remove(club);
            return Save();
        }
        //returns a list
        public async Task<IEnumerable<Club>> GetAll()
        {
            var result = await _context.Clubs.ToListAsync();
            return result;
        }
        //returns Id
        public async Task<Club> GetByIdAsync(int Id)
        {
            return await _context.Clubs.Include(i => i.Address).FirstOrDefaultAsync(i => i.Id == Id);
        }

        public async Task<IEnumerable<Club>> GetClubByCity(string city)
        {
            return await _context.Clubs.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool update(Club club)
        {
            _context.Update(club);
            return Save();

        }
    }
}
