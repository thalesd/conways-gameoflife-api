using Conways_GameOfLife_API.Helpers;
using Conways_GameOfLife_API.Models;
using Conways_GameOfLife_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Conways_GameOfLife_API.Controllers
{
    [ApiController]
    [Route("board")]
    public class BoardController : Controller
    {
        private readonly BoardService _boardService;

        public BoardController(BoardService gameService)
        {
            _boardService = gameService;
        }

        [HttpPost]
        public IActionResult CreateBoard([FromBody] CreateBoardDTO createBoardDto)
        {
            var id = _boardService.AddBoard(ArrayHelper.To2DArray(createBoardDto.BoardState));
            return Ok(new { id });
        }

        [HttpGet("{id}/next")]
        public IActionResult GetBoardNextState(Guid id)
        {
            var next = _boardService.GetNext(id);

            if (next == null) return NotFound();

            return Ok(ArrayHelper.ToJaggedArray(next));
        }

        [HttpGet("{id}/advance/{steps:int}")]
        public IActionResult GetBoardFutureState(Guid id, int steps)
        {
            var result = _boardService.Advance(id, steps);
            return result == null ? NotFound() : Ok(ArrayHelper.ToJaggedArray(result));
        }

        [HttpGet("{id}/final")]
        public IActionResult GetFinalBoardState(Guid id)
        {
            var (state, success, reason) = _boardService.GetFinal(id);
            if (!success || state == null)
                return BadRequest(reason);
            return Ok(new { finalState = ArrayHelper.ToJaggedArray(state), reason});
        }
    }
}
