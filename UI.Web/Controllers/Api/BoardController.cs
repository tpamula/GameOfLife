using System.Diagnostics.Eventing.Reader;
using Core.Model;
using System.Web.Http;

namespace UI.Web.Controllers.Api
{
    public class BoardController : ApiController
    {
        [HttpPost]
        public Board NextState([FromBody]Board board)
        {
            if (board == null) board = new Board(randomized: true);
            else board.NextGeneration();

            return board;
        }
    }
}