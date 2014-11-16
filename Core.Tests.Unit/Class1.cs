using Xunit;
using Xunit.Should;

namespace Core.Tests.Unit
{
    public class Cell
    {
        [Fact]
        public void should_die_if_less_than_two_neighbors()
        {
            var board = new Board();
            board[1, 1] = CellType.Alive;
            board[0, 1] = CellType.Alive;

            board.NextGeneration();

            board[1, 1].ShouldBe(CellType.Dead);
        }

        [Fact]
        public void should_stay_alive_with_two_neighbors()
        {
            var board = new Board();
            board[0, 1] = board[0, 2] = board[0, 3] = CellType.Alive;

            board.NextGeneration();

            board[0, 3].ShouldBe(CellType.Alive);
        }

        [Fact]
        public void should_die_with_four_neighbors()
        {
            var board = new Board();
            board[0, 1] = board[0, 2] = board[0, 3] = board[1, 1] = CellType.Alive;

            board.NextGeneration();

            board[1, 1].ShouldBe(CellType.Dead);
        }

        [Fact]
        public void should_become_alive_with_three_neighbors()
        {
            var board = new Board();
            board[0, 1] = board[0, 2] = board[0, 3] = CellType.Alive;

            board.NextGeneration();

            board[2, 1].ShouldBe(CellType.Alive);
        }
    }

    public class Board
    {
        private const int XSize = 10;
        private const int YSize = 10;
        private CellType[,] _board = new CellType[XSize, YSize];

        public CellType this[int x, int y]
        {
            get { return _board[x, y]; }
            set { _board[x, y] = value; }
        }

        public void NextGeneration()
        {
            var newBoardState = new CellType[XSize, YSize];

            for (int i = 0; i < _board.GetLength(0); i++)
            {
                for (int j = 0; j < _board.GetLength(1); j++)
                {
                    newBoardState[i, j] = GetNextState(_board, i, j);
                }
            }

            _board = newBoardState;
        }

        private CellType GetNextState(CellType[,] board, int x, int y)
        {
            int neighborsCount = GetNeighborsCount(_board, x, y);
            var currentCellState = board[x, y];

            switch (neighborsCount)
            {
                case 0:
                    goto case 1;
                case 1:
                    return CellType.Dead;

                default:
                    return _board[x, y];
            }
        }

        private int GetNeighborsCount(CellType[,] board, int x, int y)
        {
            int neighborsCount = 0;

            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (i < 0 || i >= board.GetLength(0)) continue;
                    if (j < 0 || j >= board.GetLength(1)) continue;
                    if (i == x || j == y) continue;

                    if (board[i, j] == CellType.Alive) neighborsCount++;
                }
            }

            return neighborsCount;
        }
    }

    public enum CellType
    {
        Dead = 0,
        Alive = 1
    }
}