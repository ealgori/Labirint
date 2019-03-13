using System.Collections.Generic;
using System.Threading.Tasks;

namespace Labirint.FakeLab
{
    public interface ICellsProvider
    {
        Task<IEnumerable<Cell>> GetCellsAsync();
        Task MoveAsync(Direction direction);
    }

    public class FakeCellsProvider : ICellsProvider
    {
        private Labirint _labirint;

        public FakeCellsProvider(string map)
        {
            _labirint = LabirintParser.Parse(map);
        }

        public async Task<IEnumerable<Cell>> GetCellsAsync()
        {
            return _labirint.GetSurroundCells();
        }

        public async Task MoveAsync(Direction direction)
        {
            var expectedPosition = direction.Move(_labirint.RobotPosition);

            if (_labirint.CanMoveTo(expectedPosition))
                _labirint = new Labirint(
                    _labirint.Cells,
                    expectedPosition
                );
        }
    }
}