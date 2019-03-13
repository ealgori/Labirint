using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Labirint.FakeLab;
using Labirint.MazeSolvers;

namespace Labirint
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var map = @"
1 1R   1111    1111 11 1 111  
111    1  1111   1 1   11  1 1
1111111           111 
1 111     1111     11 
1 11 11   1  111  1111 
11 1 11     1111    1Q    11 ";


            var cellsProvider = new FakeCellsProvider(map);
            IEnumerable<Cell> cells = null;
            IRobot robot = new Robot();
            await ((Robot)robot).Init("тест");

            var mazeSolver = new MazeSolver(robot);


            //await mazeSolver.Run();
            await mazeSolver.Run(f =>
            {
                Console.Clear();
                Console.WriteLine(f.ToString());
            });
        }
    }
}