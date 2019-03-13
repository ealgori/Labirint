using Labirint.MazeSolvers;
using Xunit;

namespace Labirint.Tests
{
    public class MazeMoveTests
    {
        [Fact]
        public void ShoudBeEquals()
        {
            var move1 = new MazeMove(Direction.Right, new Frame(new CellType[3, 3]
            {
                {CellType.Wall, CellType.Wall, CellType.Wall},
                {CellType.Empty, CellType.Empty, CellType.Empty},
                {CellType.Wall, CellType.Wall, CellType.Wall}
            }));

            var move2 = new MazeMove(Direction.Right, new Frame(new CellType[3, 3]
            {
                {CellType.Wall, CellType.Wall, CellType.Wall},
                {CellType.Empty, CellType.Empty, CellType.Empty},
                {CellType.Wall, CellType.Wall, CellType.Wall}
            }));

            Assert.Equal(move1, move2);
        }

        [Fact]
        public void ShoudNotBeEqualsByDirection()
        {
            var move1 = new MazeMove(Direction.Right, new Frame(new CellType[3, 3]
            {
                {CellType.Wall, CellType.Wall, CellType.Wall},
                {CellType.Empty, CellType.Empty, CellType.Empty},
                {CellType.Wall, CellType.Wall, CellType.Wall}
            }));

            var move2 = new MazeMove(Direction.Left, new Frame(new CellType[3, 3]
            {
                {CellType.Wall, CellType.Wall, CellType.Wall},
                {CellType.Empty, CellType.Empty, CellType.Empty},
                {CellType.Wall, CellType.Wall, CellType.Wall}
            }));

            Assert.NotEqual(move1, move2);
        }

        [Fact]
        public void ShoudNotBeEqualsByFrame()
        {
            var move1 = new MazeMove(Direction.Right, new Frame(new CellType[3, 3]
            {
                {CellType.Wall, CellType.Wall, CellType.Wall},
                {CellType.Empty, CellType.Empty, CellType.Empty},
                {CellType.Wall, CellType.Wall, CellType.Wall}
            }));

            var move2 = new MazeMove(Direction.Right, new Frame(new CellType[3, 3]
            {
                {CellType.Wall, CellType.Wall, CellType.Wall},
                {CellType.Empty, CellType.Empty, CellType.Wall},
                {CellType.Wall, CellType.Wall, CellType.Wall}
            }));

            Assert.NotEqual(move1, move2);
        }
    }
}