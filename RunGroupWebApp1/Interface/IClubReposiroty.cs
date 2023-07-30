
using RunGroupWebApp1.Models;

namespace RunGroupWebApp1.Interface
{
    public interface IClubReposiroty
    {
        Task<IEnumerable<Club>> GetAll();
        Task<Club> GetByIdAsync(int id);

        Task<IEnumerable<Club>> GetClubByCity(string city);
        bool Add(Club club);
        bool update(Club club);
        bool delete(Club club);
        bool Save();
    }
}
