using System;

namespace Labirint.MazeSolvers
{
    public static class MapDraw
    {
        public static void PrintMap<T>(T[,] map, (int x, int y) position, Func<T, CellType> typeSelector)
        {
            Console.Clear();
            for (var i = 0; i < map.GetLength(0); i++)
            {
                for (var j = 0; j < map.GetLength(1); j++)
                    switch (map[i, j])
                    {
                        case T cell when i == position.y && j == position.x:
                            Console.Write('R');
                            break;
                        case T cell when typeSelector(cell) == CellType.Wall:
                            Console.Write('1');
                            break;
                        case T cell when typeSelector(cell) == CellType.Exit:
                            Console.Write('Q');
                            break;
                        default:
                            Console.Write(' ');
                            break;
                    }
                Console.WriteLine();
            }
        }
    }
}