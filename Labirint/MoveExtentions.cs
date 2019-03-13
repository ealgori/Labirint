using System;
using System.Collections.Generic;

namespace Labirint
{
    public static class MoveExtentions
    {
        private static readonly Dictionary<Direction, Func<(int, int), (int, int)>> _moves =
            new Dictionary<Direction, Func<(int, int), (int, int)>>
            {
                {Direction.Top, c => (c.Item1, c.Item2 - 1)},
                {Direction.Bottom, c => (c.Item1, c.Item2 + 1)},
                {Direction.Left, c => (c.Item1 - 1, c.Item2)},
                {Direction.Right, c => (c.Item1 + 1, c.Item2)},
                {Direction.TopLeft, c => (c.Item1 - 1, c.Item2 - 1)},
                {Direction.TopRight, c => (c.Item1 + 1, c.Item2 - 1)},
                {Direction.BottomLeft, c => (c.Item1 - 1, c.Item2 + 1)},
                {Direction.BottomRight, c => (c.Item1 + 1, c.Item2 + 1)}
            };

        private static readonly Dictionary<Direction, Direction> _opositeDirections =
            new Dictionary<Direction, Direction>
            {
                {Direction.Top, Direction.Bottom},
                {Direction.Bottom, Direction.Top},
                {Direction.Left, Direction.Right},
                {Direction.Right, Direction.Left},
                {Direction.TopLeft, Direction.BottomRight},
                {Direction.TopRight, Direction.BottomLeft},
                {Direction.BottomLeft, Direction.TopRight},
                {Direction.BottomRight, Direction.TopLeft}
            };

        public static (int, int) Move(this Direction direction, (int x, int y) position)
        {
            return _moves[direction](position);
        }


        public static Direction Invert(this Direction direction)
        {
            return _opositeDirections[direction];
        }
    }
}