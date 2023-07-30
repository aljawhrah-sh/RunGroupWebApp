
using System.Diagnostics;
using RunGroupWebApp1.Models;

namespace RunGroupWebApp1.Interface
{
    public interface IRaceRepository
    {

        Task<IEnumerable<Race>> GetAll();
        Task<Race> GetByIdAsyn(int id);

        Task<IEnumerable<Race>> GetRaceByCity(string city);
        bool Add(Race race);
        bool update(Race race);
        bool delete(Race race);
        bool Save();
    }
}
