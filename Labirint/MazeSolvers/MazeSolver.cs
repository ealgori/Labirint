using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Labirint.Tests")]

namespace Labirint.MazeSolvers
{
    internal class MazeSolver
    {
        public MazeSolver(IRobot robot)
        {
            Robot = robot;
            visitedCells.Add(CurrentRobotPosition);
        }

        public IRobot Robot { get; }
        public (int x, int y) CurrentRobotPosition { get; private set; } = (0, 0);

        public Stack<MazeMove> MazeMoves { get; } = new Stack<MazeMove>();
        internal HashSet<(int x, int y)> visitedCells { get; } = new HashSet<(int x, int y)>();


        internal async Task MakeMove(Direction direction)
        {
            await Robot.MoveAsync(direction);
            var frame = new Frame(await Robot.GetCellsAsync());

            MazeMoves.Push(new MazeMove(direction, frame));
            CurrentRobotPosition = direction.Move(CurrentRobotPosition);
            visitedCells.Add(CurrentRobotPosition);
        }

        internal async Task MoveBack()
        {
            if (MazeMoves.Count == 0)
                throw new InvalidOperationException("There are no more moves back");

            var move = MazeMoves.Pop();
            await Robot.MoveAsync(move.Direction.Invert());
            CurrentRobotPosition = move.Direction.Invert().Move(CurrentRobotPosition);
        }

        public async Task Run(Action<Frame> traceAction = null)
        {
            while (true)
            {
                var frame = new Frame(await Robot.GetCellsAsync());
                traceAction?.Invoke(frame);
                if (frame.HasExit)
                {
                    frame.TryGetDirectionToMove(ct => ct == CellType.Exit, null, out var direction);
                    await MakeMove(direction);
                    Console.WriteLine($"Exit found at:{CurrentRobotPosition}; visited:{visitedCells.Count} cells");
                    break;
                }

                await Iterate(frame);
            }
        }

        internal async Task Iterate(Frame frame)
        {
            var hasMoves = frame.TryGetDirectionToMove(ct =>
                    ct == CellType.Empty,
                coord => visitedCells.Contains(
                    (coord.x + CurrentRobotPosition.x, coord.y + CurrentRobotPosition.y)),
                out var direction);
            if (hasMoves)
                await MakeMove(direction);
            else
                await MoveBack();
        }
    }
}