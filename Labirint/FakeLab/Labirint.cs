using System;
using System.Collections.Generic;

namespace Labirint.FakeLab
{
    public class Labirint
    {
        public Labirint(CellType[,] cells, (int, int) robotPosition)
        {
            Cells = cells;
            RobotPosition = robotPosition;
        }

        public CellType[,] Cells { get; }
        public (int x, int y) RobotPosition { get; }

        public IEnumerable<Cell> GetSurroundCells()
        {
            if (TryGetCell(Direction.Left.Move(RobotPosition), out var ctl))
                yield return new Cell {CellType = ctl, Direction = Direction.Left};
            if (TryGetCell(Direction.Right.Move(RobotPosition), out var ctr))
                yield return new Cell {CellType = ctr, Direction = Direction.Right};
            if (TryGetCell(Direction.Bottom.Move(RobotPosition), out var ctb))
                yield return new Cell {CellType = ctb, Direction = Direction.Bottom};
            if (TryGetCell(Direction.Top.Move(RobotPosition), out var ctt))
                yield return new Cell {CellType = ctt, Direction = Direction.Top};
            if (TryGetCell(Direction.TopLeft.Move(RobotPosition), out var cttl))
                yield return new Cell {CellType = cttl, Direction = Direction.TopLeft};
            if (TryGetCell(Direction.TopRight.Move(RobotPosition), out var cttr))
                yield return new Cell {CellType = cttr, Direction = Direction.TopRight};
            if (TryGetCell(Direction.BottomLeft.Move(RobotPosition), out var ctbl))
                yield return new Cell {CellType = ctbl, Direction = Direction.BottomLeft};
            if (TryGetCell(Direction.BottomRight.Move(RobotPosition), out var ctbr))
                yield return new Cell {CellType = ctbr, Direction = Direction.BottomRight};
        }

        private bool TryGetCell((int x, int y) pos, out CellType cell)
        {
            cell = CellType.Empty;
            try
            {
                cell = Cells[pos.y, pos.x];
                return true;
            }
            catch (Exception)
            {
            }

            return false;
        }

        public bool CanMoveTo((int x, int y) position)
        {
            return
                position.x >= 0 &&
                position.y >= 0 &&
                position.x < Cells.GetLength(1) &&
                position.y < Cells.GetLength(0) &&
                Cells[position.y, position.x] != CellType.Wall;
        }
    }
}