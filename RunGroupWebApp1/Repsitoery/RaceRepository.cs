using Microsoft.EntityFrameworkCore;
using RunGroupWebApp1.Data;
using RunGroupWebApp1.Interface;
using RunGroupWebApp1.Models;

namespace RunGroupWebApp1.Repsitoery
{
    public class RaceRepository : IRaceRepository
    {
        private readonly ApplicationDbContext _context;
        public RaceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Race race)
        {
            _context.Add(race);
            return Save();
        }

        public bool delete(Race race)
        {
            _context.Remove(race);
            return Save();
        }

        public async Task<IEnumerable<Race>> GetAll()
        {
            return await _context.Races.ToListAsync();
        }

        public async Task<Race> GetByIdAsyn(int id)
        {
            return await _context.Races.Include(i => i.Address).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Race>> GetRaceByCity(string city)
        {
            return await _context.Races.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool update(Race race)
        {
            _context.Update(race);
            return Save();
        }
    }
}
