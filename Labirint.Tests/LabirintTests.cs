using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Labirint.FakeLab;
using Xunit;

namespace Labirint.Tests
{
    public class LabirintTests
    {
        [Fact]
        public void ShouldGetSurroundsCase1()
        {
            var map = @"
1 1 
1R1 
1Q1";
            var lab = LabirintParser.Parse(map);

            var surround = lab.GetSurroundCells().ToList();

            var expectedSurrounds = new List<Cell>
            {
                new Cell {CellType = CellType.Wall, Direction = Direction.Left},
                new Cell {CellType = CellType.Wall, Direction = Direction.Right},
                new Cell {CellType = CellType.Empty, Direction = Direction.Top},
                new Cell {CellType = CellType.Exit, Direction = Direction.Bottom},

                new Cell {CellType = CellType.Wall, Direction = Direction.TopLeft},
                new Cell {CellType = CellType.Wall, Direction = Direction.TopRight},
                new Cell {CellType = CellType.Wall, Direction = Direction.BottomLeft},
                new Cell {CellType = CellType.Wall, Direction = Direction.BottomRight}
            };
            expectedSurrounds.Should().BeEquivalentTo(surround);
        }

        [Fact]
        public void ShouldGetSurroundsCase2()
        {
            var map = @"
1R1 
1 1 
1Q1";
            var lab = LabirintParser.Parse(map);

            var surround = lab.GetSurroundCells().ToList();

            var expectedSurrounds = new List<Cell>
            {
                new Cell {CellType = CellType.Wall, Direction = Direction.Left},
                new Cell {CellType = CellType.Wall, Direction = Direction.Right},
                new Cell {CellType = CellType.Empty, Direction = Direction.Bottom},

                new Cell {CellType = CellType.Wall, Direction = Direction.BottomLeft},
                new Cell {CellType = CellType.Wall, Direction = Direction.BottomRight}
            };
            expectedSurrounds.Should().BeEquivalentTo(surround);
        }

        [Fact]
        public void ShouldGetSurroundsCase3()
        {
            var map = @"
1Q1 
1 1 
1R1";
            var lab = LabirintParser.Parse(map);

            var surround = lab.GetSurroundCells().ToList();

            var expectedSurrounds = new List<Cell>
            {
                new Cell {CellType = CellType.Wall, Direction = Direction.Left},
                new Cell {CellType = CellType.Wall, Direction = Direction.Right},
                new Cell {CellType = CellType.Empty, Direction = Direction.Top},

                new Cell {CellType = CellType.Wall, Direction = Direction.TopLeft},
                new Cell {CellType = CellType.Wall, Direction = Direction.TopRight}
            };
            expectedSurrounds.Should().BeEquivalentTo(surround);
        }

        [Fact]
        public void ShouldGetSurroundsCase4()
        {
            var map = @"
1Q1
1 R
1 1";
            var lab = LabirintParser.Parse(map);

            var surround = lab.GetSurroundCells().ToList();

            var expectedSurrounds = new List<Cell>
            {
                new Cell {CellType = CellType.Empty, Direction = Direction.Left},
                new Cell {CellType = CellType.Wall, Direction = Direction.Bottom},
                new Cell {CellType = CellType.Wall, Direction = Direction.Top},

                new Cell {CellType = CellType.Empty, Direction = Direction.BottomLeft},
                new Cell {CellType = CellType.Exit, Direction = Direction.TopLeft}
            };
            expectedSurrounds.Should().BeEquivalentTo(surround);
        }


        [Fact]
        public void ShouldGetSurroundsCase5()
        {
            var map = @"
1Q1 
R  
1 1";

            var lab = LabirintParser.Parse(map);
            var surround = lab.GetSurroundCells().ToList();

            var expectedSurrounds = new List<Cell>
            {
                new Cell {CellType = CellType.Empty, Direction = Direction.Right},
                new Cell {CellType = CellType.Wall, Direction = Direction.Bottom},
                new Cell {CellType = CellType.Wall, Direction = Direction.Top},

                new Cell {CellType = CellType.Exit, Direction = Direction.TopRight},
                new Cell {CellType = CellType.Empty, Direction = Direction.BottomRight}
            };

            expectedSurrounds.Should().BeEquivalentTo(surround);
        }
    }
}