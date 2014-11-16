using Core.Enums;
using System;

namespace Core.Model
{
    public class Board
    {
        private const int XSize = 70;
        private const int YSize = 70;
        private CellType[,] _board = new CellType[XSize, YSize];

        public Board(bool randomized = false)
        {
            if (randomized) Randomize();
        }

        public CellType[,] Presentation
        {
            get { return _board; }
            set { _board = value; }
        }

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

        private int GetNeighborsCount(CellType[,] board, int x, int y)
        {
            int neighborsCount = 0;

            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (i < 0 || i >= board.GetLength(0)) continue;
                    if (j < 0 || j >= board.GetLength(1)) continue;
                    if (i == x && j == y) continue;

                    if (board[i, j] == CellType.Alive) neighborsCount++;
                }
            }

            return neighborsCount;
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

                case 2:
                    return currentCellState == CellType.Alive ? CellType.Alive : CellType.Dead;

                case 3:
                    return CellType.Alive;

                case 4:
                    return CellType.Dead;

                default:
                    return _board[x, y];
            }
        }

        private void Randomize()
        {
            var random = new Random(Guid.NewGuid().GetHashCode());

            for (int i = 0; i < _board.GetLength(0); i++)
            {
                for (int j = 0; j < _board.GetLength(1); j++)
                {
                    _board[i, j] = random.Next(0, 100) < 10 ? CellType.Alive : CellType.Dead;
                }
            }
        }
    }
}