using System;
using System.Collections.Generic;
using System.Linq;

namespace Labirint
{
    public static class CellsExtentions
    {
        public static TOut[,] ToFrameArray<TOut>(this IEnumerable<Cell> cells, Func<Cell, TOut> caster)
        {
            if (cells.Count() > 8)
                throw new ArgumentException("Invalid frame size. Max 8 cells");


            var map = new TOut[3, 3];
            for (var i = 0; i < map.GetLength(0); i++)
            for (var j = 0; j < map.GetLength(1); j++)
                map[i, j] = caster(new Cell {CellType = CellType.Wall});

            foreach (var cell in cells)
                switch (cell.Direction)
                {
                    case Direction.Top:
                        map[0, 1] = caster(cell);
                        break;
                    case Direction.Bottom:
                        map[2, 1] = caster(cell);
                        break;
                    case Direction.Left:
                        map[1, 0] = caster(cell);
                        break;
                    case Direction.Right:
                        map[1, 2] = caster(cell);
                        break;
                    case Direction.TopLeft:
                        map[0, 0] = caster(cell);
                        break;
                    case Direction.TopRight:
                        map[0, 2] = caster(cell);
                        break;
                    case Direction.BottomLeft:
                        map[2, 0] = caster(cell);
                        break;
                    case Direction.BottomRight:
                        map[2, 2] = caster(cell);
                        break;
                }

            map[1, 1] = caster(new Cell {CellType = CellType.Empty});
            return map;
        }


//        public static Cell[,] ToCellsArray(this CellType[,] frame)
//        {
//            this
//        }

        public static TOut[,] Transform<TIn, TOut>(TIn[,] source, Func<TIn, TOut> caster)
        {
            var array = new TOut[source.GetLength(0), source.GetLength(1)];
            for (var i = 0; i < source.GetLength(0); i++)
            for (var j = 0; j < source.GetLength(1); j++)
                array[i, j] = caster(source[i, j]);

            return array;
        }

        public static Direction OffsetToDirection(this (int x, int y) offset)
        {
            switch (offset)
            {
                case var ofst when ofst.x == -1 && ofst.y == -1:
                    return Direction.TopLeft;
                case var ofst when ofst.x == 0 && ofst.y == -1:
                    return Direction.Top;
                case var ofst when ofst.x == 1 && ofst.y == -1:
                    return Direction.TopRight;
                case var ofst when ofst.x == -1 && ofst.y == 0:
                    return Direction.Left;
                case var ofst when ofst.x == 1 && ofst.y == 0:
                    return Direction.Right;
                case var ofst when ofst.x == -1 && ofst.y == 1:
                    return Direction.BottomLeft;
                case var ofst when ofst.x == 0 && ofst.y == 1:
                    return Direction.Bottom;
                case var ofst when ofst.x == 1 && ofst.y == 1:
                    return Direction.BottomRight;
            }

            throw new ArgumentException("Invalid offset. Allowed 1 step offset");
        }
    }
}