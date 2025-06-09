using Microsoft.AspNetCore.Mvc;

namespace Conways_GameOfLife_API.Controllers
{
    public class BoardController : Controller
    {
        [HttpPost]
        public IActionResult CreateBoard()
        {
            return Ok();
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
