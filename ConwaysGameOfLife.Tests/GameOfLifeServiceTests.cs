using Conways_GameOfLife_API.Services;
using Xunit;

namespace ConwaysGameOfLife.Tests
{
    public class GameOfLifeServiceTests
    {
        private readonly GameOfLifeService _service = new();

        [Fact]
        public void Blinker_Oscillates_Correctly()
        {
            var input = new bool[,]
            {
                { false, true, false },
                { false, true, false },
                { false, true, false }
            };

            var expected = new bool[,]
            {
                { false, false, false },
                { true,  true,  true  },
                { false, false, false }
            };

            var result = _service.GetNextState(input);

            Assert.True(_service.BoardsEqual(result, expected));
        }

        [Fact]
        public void Static_Block_Remains_The_Same()
        {
            var input = new bool[,]
            {
                { true, true },
                { true, true }
            };

            var result = _service.GetNextState(input);

            Assert.True(_service.BoardsEqual(input, result));
        }

        [Fact]
        public void EmptyBoard_RemainsEmpty()
        {
            var input = new bool[3, 3];
            var result = _service.GetNextState(input);
            Assert.True(_service.BoardsEqual(input, result));
        }

        [Fact]
        public void GliderPattern_MovesCorrectly()
        {
            var input = new bool[,]
            {
                { false, true, false },
                { false, false, true },
                { true, true, true }
            };

            var result = _service.GetNextState(input);
            Assert.False(_service.BoardsEqual(input, result));
        }
    }
}
