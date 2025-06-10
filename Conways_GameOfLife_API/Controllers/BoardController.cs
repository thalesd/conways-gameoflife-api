using System.Net;
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
        private readonly IBoardService _boardService;

        public BoardController(IBoardService gameService)
        {
            _boardService = gameService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBoardAsync([FromBody] CreateBoardDTO createBoardDto)
        {
            if (createBoardDto.BoardState == null || createBoardDto.BoardState.Length == 0 || createBoardDto.BoardState.Any(row => row == null || row.Length != createBoardDto.BoardState[0].Length))
                return ErrorFormatter.Format("Invalid board structure. Ensure it's a non-empty rectangular matrix.");

            var id = await _boardService.AddBoardAsync(ArrayHelper.To2DArray(createBoardDto.BoardState));
            return Ok(new { id });
        }

        [HttpGet("{id}/next")]
        public async Task<IActionResult> GetBoardNextStateAsync(Guid id)
        {
            var next = await _boardService.GetNextAsync(id);

            if (next == null) return ErrorFormatter.Format("Board was not found", null, (int)HttpStatusCode.NotFound);

            return Ok(ArrayHelper.ToJaggedArray(next));
        }

        [HttpGet("{id}/advance/{steps:int}")]
        public async Task<IActionResult> GetBoardFutureStateAsync(Guid id, int steps)
        {
            if (steps <= 0)
                return ErrorFormatter.Format("Step count must be a positive integer.");

            var result = await _boardService.AdvanceAsync(id, steps);
            return result == null ? NotFound() : Ok(ArrayHelper.ToJaggedArray(result));
        }

        [HttpGet("{id}/final")]
        public async Task<IActionResult> GetFinalBoardStateAsync(Guid id)
        {
            var (state, success, reason) = await _boardService.GetFinalAsync(id);
            if (!success || state == null)
                return ErrorFormatter.Format(reason);
            return Ok(new { finalState = ArrayHelper.ToJaggedArray(state), reason});
        }
    }
}
