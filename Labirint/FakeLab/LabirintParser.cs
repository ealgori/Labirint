using System;
using System.Linq;

namespace Labirint.FakeLab
{
    public static class LabirintParser
    {
        public static Labirint Parse(string map)
        {
            var rows = map.Split(Environment.NewLine).Where(r => r.Length > 0).ToArray();
            var labirint = new CellType[rows.Length, rows.Max(r => r.Length)];
            var inintvalue = (0, 0);
            for (var rowInd = 0; rowInd < rows.Length; rowInd++)
            for (var cellInd = 0; cellInd < rows[rowInd].Length; cellInd++)
                switch (rows[rowInd][cellInd])
                {
                    case char c when char.IsWhiteSpace(c):
                        labirint[rowInd, cellInd] = CellType.Empty;
                        break;
                    case char c when c == '1':
                        labirint[rowInd, cellInd] = CellType.Wall;
                        break;
                    case char c when c == 'Q':
                        labirint[rowInd, cellInd] = CellType.Exit;
                        break;
                    case char c when c == 'R':
                        inintvalue = (cellInd, rowInd);
                        break;
                }

            return new Labirint(labirint, inintvalue);
        }
    }
}