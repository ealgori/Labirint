using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Labirint.MazeSolvers
{
    internal class Frame
    {
        private readonly CellType[,] _frameArray;

        public Frame(IEnumerable<Cell> cells)
        {
            _frameArray = cells.ToFrameArray(i => i.CellType);
        }

        public Frame(CellType[,] frameArray)
        {
            _frameArray = frameArray;
        }

        public bool HasExit =>
            _frameArray.Cast<CellType>().Any(t => t == CellType.Exit);

        public bool TryGetDirectionToMove(Func<CellType, bool> filter, Func<(int x, int y), bool> exclude,
            out Direction direction)
        {
            var hasMove = false;
            direction = Direction.Top;
            for (var i = 0; i < _frameArray.GetLength(0); i++)
            for (var j = 0; j < _frameArray.GetLength(1); j++)
            {
                if (i == 1 && j == 1)
                    continue;

                if (!hasMove && filter(_frameArray[i, j]))
                {
                    var offset = (j - 1, i - 1);
                    if (exclude != null && exclude(offset)) // skip visited cells
                        continue;


                    hasMove = true;
                    direction = (j - 1, i - 1).OffsetToDirection();
                }
            }

            return hasMove;
        }


        protected bool Equals(Frame other)
        {
            return _frameArray.Rank == other._frameArray.Rank &&
                   Enumerable.Range(0, _frameArray.Rank).All(dimension =>
                       _frameArray.GetLength(dimension) == other._frameArray.GetLength(dimension)) &&
                   _frameArray.Cast<CellType>().SequenceEqual(other._frameArray.Cast<CellType>());
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Frame) obj);
        }

        public override int GetHashCode()
        {
            return _frameArray != null ? _frameArray.GetHashCode() : 0;
        }

        public static bool operator ==(Frame left, Frame right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Frame left, Frame right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < _frameArray.GetLength(0); i++)
            {
                for (var j = 0; j < _frameArray.GetLength(1); j++)
                    switch (_frameArray[i, j])
                    {
                        case CellType cell when i == 1 && j == 1:
                            sb.Append('R');
                            break;
                        case CellType cell when cell == CellType.Wall:
                            sb.Append('1');
                            break;
                        case CellType cell when cell == CellType.Exit:
                            sb.Append('Q');
                            break;
                        default:
                            sb.Append(' ');
                            break;
                    }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}