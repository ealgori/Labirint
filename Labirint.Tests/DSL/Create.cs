using Labirint.FakeLab;
using Labirint.MazeSolvers;

namespace Labirint.Tests.DSL
{
    public static class Create
    {
        internal static IRobot LocalRobot(string map)
        {
            ICellsProvider cellProvider = new FakeCellsProvider(map);
            return new LocalRobot(cellProvider);
        }

        internal static MazeSolver MazeSolver(IRobot robot)
        {
            return new MazeSolver(robot);
        }
    }
}