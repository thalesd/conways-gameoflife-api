using Conways_GameOfLife_API.Helpers;
using Conways_GameOfLife_API.Models;
using Conways_GameOfLife_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Conways_GameOfLife_API.Controllers
{
    [Route("board")]
    public class BoardController : Controller
    {
        private readonly GameOfLifeService _gameService;

        public BoardController(GameOfLifeService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost]
        public IActionResult CreateBoard([FromBody] CreateBoardDTO createBoardDto)
        {
            var id = _gameService.AddBoard(ArrayHelper.To2DArray(createBoardDto.BoardState));
            return Ok(new { id });
        }

        [HttpGet("{id}/next")]
        public IActionResult GetBoardNextState()
        {
            return Ok();
        }

        [HttpGet("{id}/advance/{steps}")]
        public IActionResult GetBoardFutureState()
        {
            return Ok();
        }

        [HttpGet("{id}/final")]
        public IActionResult GetFinalBoardState()
        {
            return Ok();
        }
    }
}
