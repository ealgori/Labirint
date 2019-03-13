using System.Collections.Generic;
using System.Threading.Tasks;
using Labirint.FakeLab;

namespace Labirint
{
    internal class LocalRobot : IRobot
    {
        private readonly ICellsProvider _cellProvider;

        public LocalRobot(ICellsProvider cellProvider)
        {
            _cellProvider = cellProvider;
        }


        public async Task MoveAsync(Direction direction)
        {
            await _cellProvider.MoveAsync(direction);
        }

        public async Task<IEnumerable<Cell>> GetCellsAsync()
        {
            return await _cellProvider.GetCellsAsync();
        }
    }
}