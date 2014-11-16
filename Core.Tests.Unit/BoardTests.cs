using Xunit;
using Xunit.Should;

namespace Core.Tests.Unit
{
    public class BoardTests
    {
        [Fact]
        public void should_die_if_less_than_two_neighbors()
        {
            var board = new Board();
            board[1, 1] = board[0, 1] = CellType.Alive;

            board.NextGeneration();

            board[1, 1].ShouldBe(CellType.Dead);
        }

        [Fact]
        public void should_stay_alive_with_two_neighbors()
        {
            var board = new Board();
            board[0, 1] = board[0, 2] = board[1, 1] = CellType.Alive;

            board.NextGeneration();

            board[1, 1].ShouldBe(CellType.Alive);
        }

        [Fact]
        public void should_stay_alive_with_three_neighbors()
        {
            var board = new Board();
            board[0, 1] = board[0, 2] = board[1, 1] = CellType.Alive;

            board.NextGeneration();

            board[1, 1].ShouldBe(CellType.Alive);
        }

        [Fact]
        public void should_die_with_four_neighbors()
        {
            var board = new Board();
            board[0, 1] = board[0, 2] = board[0, 3] = board[1, 1] = board[2, 1] = CellType.Alive;

            board.NextGeneration();

            board[2, 1].ShouldBe(CellType.Dead);
        }

        [Fact]
        public void should_become_alive_with_three_neighbors()
        {
            var board = new Board();
            board[0, 1] = board[0, 2] = board[0, 3] = CellType.Alive;

            board.NextGeneration();

            board[1, 2].ShouldBe(CellType.Alive);
        }
    }
}