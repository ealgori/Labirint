using Labirint.FakeLab;
using Xunit;

namespace Labirint.Tests
{
    public class ParserTests
    {
        [Fact]
        public void ShouldParseMapToCorrectLabirint()
        {
            var map = @"
1R1 
1 1 
1Q1";
            var lab = LabirintParser.Parse(map);
            var expectedLab = new CellType[3, 4]
            {
                {CellType.Wall, CellType.Empty, CellType.Wall, CellType.Empty},
                {CellType.Wall, CellType.Empty, CellType.Wall, CellType.Empty},
                {CellType.Wall, CellType.Exit, CellType.Wall, CellType.Empty}
            };
            Assert.Equal(expectedLab, lab.Cells);
            Assert.Equal((1, 0), lab.RobotPosition);
        }
    }
}