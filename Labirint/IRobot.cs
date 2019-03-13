using System.Collections.Generic;
using System.Threading.Tasks;

namespace Labirint
{
    internal interface IRobot
    {
        Task MoveAsync(Direction direction);
        Task<IEnumerable<Cell>> GetCellsAsync();
    }
}