using System.Collections.Generic;
using System.Threading.Tasks;

namespace WordleSolver.Sources
{
    public interface ISource
    {
        Task<string> GetHashAsync();
        Task<IEnumerable<string>> GetDataAsync();
    }
}
