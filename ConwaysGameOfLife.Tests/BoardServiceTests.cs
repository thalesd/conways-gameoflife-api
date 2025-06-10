using Conways_GameOfLife_API.Configuration;
using Conways_GameOfLife_API.Data;
using Conways_GameOfLife_API.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace ConwaysGameOfLife.Tests
{
    public class BoardServiceTests
    {
        private readonly Mock<IBoardRepository> _repoMock;
        private readonly GameOfLifeService _gameService;
        private readonly IOptions<APIConfig> _config;
        private readonly BoardService _service;

        public BoardServiceTests()
        {
            _repoMock = new Mock<IBoardRepository>(MockBehavior.Strict); // not calling EF
            _gameService = new GameOfLifeService();
            _config = Options.Create(new APIConfig
            {
                MaxIterations = 100
            });

            _service = new BoardService(_repoMock.Object, _gameService, _config, new Mock<ILogger<BoardService>>().Object);
        }

        [Fact]
        public async Task GetFinal_StaticBlock_ReturnsStatic()
        {
            var input = new bool[,]
            {
                { true, true },
                { true, true }
            };

            _repoMock.Setup(r => r.GetBoardAsync(It.IsAny<Guid>()))
                 .ReturnsAsync(input);

            var result = await _service.GetFinalAsync(Guid.NewGuid());

            result.success.Should().BeTrue();
            result.reason.Should().Be("Reached simulation static state");
        }

        [Fact]
        public async Task GetFinal_Oscillator_Repeats_ReturnsOscillation()
        {
            var input = new bool[,]
            {
                { false, true, false },
                { false, true, false },
                { false, true, false }
            };

            _repoMock.Setup(r => r.GetBoardAsync(It.IsAny<Guid>()))
                 .ReturnsAsync(input);

            var result = await _service.GetFinalAsync(Guid.NewGuid());

            result.success.Should().BeTrue();
            result.reason.Should().Be("Reached simulation oscillation state");
        }

        [Fact]
        public async Task GetFinal_ExceedsMaxIterations_ReturnsError()
        {
            var input = new bool[5, 5]; // stable empty board but fake loop

            _repoMock.Setup(r => r.GetBoardAsync(It.IsAny<Guid>()))
                 .ReturnsAsync(input);

            var config = Options.Create(new APIConfig { MaxIterations = 0 }); // force loop cutoff
            var service = new BoardService(_repoMock.Object, _gameService, config, Mock.Of<ILogger<BoardService>>());

            var result = await service.GetFinalAsync(Guid.NewGuid());

            result.success.Should().BeFalse();
            result.reason.Should().Contain("Exceeded maximum configured iterations");
        }
    }
}
