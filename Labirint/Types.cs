using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Labirint
{
    public enum Direction
    {
        Top,
        TopRight,
        Right,
        BottomRight,
        Bottom,
        BottomLeft,
        Left,
        TopLeft
    }

    public enum CellType
    {
        Empty,
        Wall,
        Exit
    }

    public class SessionCreateResponse
    {
        public Guid SessionId { get; set; }
    }

    public class GetCellsResponse
    {
        public IEnumerable<Cell> Cells;
    }

    [DebuggerDisplay("Direction= {Direction}: Type= {CellType}")]
    public class Cell
    {
        public Direction Direction { get; set; }
        public CellType CellType { get; set; }
    }
}