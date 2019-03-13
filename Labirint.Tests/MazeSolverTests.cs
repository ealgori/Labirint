using System;
using System.Threading.Tasks;
using Labirint.MazeSolvers;
using Labirint.Tests.DSL;
using Xunit;

namespace Labirint.Tests
{
    public class MazeSolverTests
    {
        [Fact]
        public async Task ShouldMoveToBottom()
        {
            var map = @"1111
1R1 
1 11";

            var robot = Create.LocalRobot(map);
            var solver = Create.MazeSolver(Create.LocalRobot(map));

            var frame = new Frame(await robot.GetCellsAsync());
            var hasDirection = frame.TryGetDirectionToMove(ct => ct == CellType.Empty, null, out var direction);

            Assert.True(hasDirection);
            Assert.Equal(Direction.Bottom, direction);
        }

        [Fact]
        public async Task ShouldNotFindMove()
        {
            var map = @"1111
1R1 
1111";

            var robot = Create.LocalRobot(map);
            var solver = Create.MazeSolver(Create.LocalRobot(map));

            var frame = new Frame(await robot.GetCellsAsync());
            var hasDirection = frame.TryGetDirectionToMove(ct => ct == CellType.Empty, null, out _);

            Assert.False(hasDirection);
        }

        [Fact]
        public async Task ShouldNotSuggestAnyCellIfNoUnvisited()
        {
            var map = @"1111
1R  
1111";

            var robot = Create.LocalRobot(map);
            var solver = Create.MazeSolver(Create.LocalRobot(map));

            await solver.MakeMove(Direction.Right);
            await solver.MakeMove(Direction.Left);

            Func<(int x, int y), bool> checkVisitedFunc = coord => solver.visitedCells.Contains(
                (coord.x + solver.CurrentRobotPosition.x, coord.y + solver.CurrentRobotPosition.y));
            var suggest = solver.MazeMoves.Peek().Frame
                .TryGetDirectionToMove(ct => ct == CellType.Empty, checkVisitedFunc, out _);

            Assert.False(suggest);
        }


        [Fact]
        public async Task ShouldNotSuggestVisitedCells()
        {
            var map = @"1111
1R  
1 11";

            var robot = Create.LocalRobot(map);
            var solver = Create.MazeSolver(Create.LocalRobot(map));

            await solver.MakeMove(Direction.Right);
            await solver.MakeMove(Direction.Left);

            Func<(int x, int y), bool> checkVisitedFunc = coord => solver.visitedCells.Contains(
                (coord.x + solver.CurrentRobotPosition.x, coord.y + solver.CurrentRobotPosition.y));
            solver.MazeMoves.Peek().Frame
                .TryGetDirectionToMove(ct => ct == CellType.Empty, checkVisitedFunc, out var direction);

            Assert.Equal(Direction.Bottom, direction);
        }

        [Fact]
        public async Task ShouldPopMovesFromStackWhenMovesBack()
        {
            var map = @"1111
1R  
1111";

            var robot = Create.LocalRobot(map);
            var solver = Create.MazeSolver(Create.LocalRobot(map));

            await solver.MakeMove(Direction.Right);

            await solver.MoveBack();
            Assert.Empty(solver.MazeMoves);
        }

        [Fact]
        public async Task ShouldSaveMoveToStack()
        {
            var map = @"1111
1R  
1111";

            var robot = Create.LocalRobot(map);
            var solver = Create.MazeSolver(Create.LocalRobot(map));

            await solver.MakeMove(Direction.Right);

            var mazeMove = solver.MazeMoves.Peek();
            var expectedMove = new MazeMove(Direction.Right, new Frame(new CellType[3, 3]
            {
                {CellType.Wall, CellType.Wall, CellType.Wall},
                {CellType.Empty, CellType.Empty, CellType.Empty},
                {CellType.Wall, CellType.Wall, CellType.Wall}
            }));


            Assert.Single(solver.MazeMoves);
            Assert.Equal(expectedMove, mazeMove);
        }

        [Fact]
        public async Task ShouldSaveVisitedCells()
        {
            var map = @"1111
1R  
1111";

            var robot = Create.LocalRobot(map);
            var solver = Create.MazeSolver(Create.LocalRobot(map));

            await solver.MakeMove(Direction.Right);

            Assert.True(solver.visitedCells.Count == 2);
            Assert.True(solver.visitedCells.Contains((0, 0)));
            Assert.True(solver.visitedCells.Contains((1, 0)));
        }

        [Fact]
        public async Task ShouldSawExit()
        {
            var map = @"1111
1R1 
11Q1";

            var robot = Create.LocalRobot(map);
            var solver = Create.MazeSolver(Create.LocalRobot(map));

            var frame = new Frame(await robot.GetCellsAsync());


            Assert.True(frame.HasExit);
        }

        [Fact]
        public async Task ShouldThrowWhenNoMoreMovesBack()
        {
            var map = @"1111
1R  
1111";

            var robot = Create.LocalRobot(map);
            var solver = Create.MazeSolver(Create.LocalRobot(map));

            await solver.MakeMove(Direction.Right);

            await solver.MoveBack();
            await Assert.ThrowsAsync<InvalidOperationException>(() => solver.MoveBack());
        }
    }
}