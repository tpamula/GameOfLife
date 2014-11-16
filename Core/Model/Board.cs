using Core.Enums;
using System;

namespace Core.Model
{
    // TODO: neighbors' methods and variables could be pulled to an external class
    public class Board
    {
        /// <summary>
        /// Chance of cell being alive when creating a randomized board.
        /// </summary>
        private const double ChanceOfCreation = 0.17;

        private const int XSize = 60;
        private const int YSize = 60;

        /// <summary>
        /// Size of array is increased in order to skip border checks in computations;
        /// </summary>
        private readonly int[,] _neighborsCount = new int[XSize + 2, YSize + 2];

        private CellType[,] _board = new CellType[XSize, YSize];

        private bool _neighborsComputed;

        public Board(bool randomized = false)
        {
            if (randomized) RandomizeBoard();
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

        private void ComputeNeighbors(CellType[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == CellType.Alive)
                    {
                        // translated coordinates because of difference
                        // in sizes between board and _neighborsCount arrays
                        int tI = i + 1;
                        int tJ = j + 1;

                        _neighborsCount[tI - 1, tJ - 1]++;
                        _neighborsCount[tI - 1, tJ]++;
                        _neighborsCount[tI - 1, tJ + 1]++;
                        _neighborsCount[tI + 1, tJ - 1]++;
                        _neighborsCount[tI + 1, tJ]++;
                        _neighborsCount[tI + 1, tJ + 1]++;
                        _neighborsCount[tI, tJ + 1]++;
                        _neighborsCount[tI, tJ - 1]++;
                    }
                }
            }
        }

        private int GetNeighborsCount(CellType[,] board, int x, int y)
        {
            if (!_neighborsComputed)
            {
                ComputeNeighbors(board);
                _neighborsComputed = true;
            }

            return _neighborsCount[x + 1, y + 1];
        }

        private CellType GetNextState(CellType[,] board, int x, int y)
        {
            int neighborsCount = GetNeighborsCount(_board, x, y);
            var currentCellState = board[x, y];

            switch (neighborsCount)
            {
                case 0:
                case 1:
                    return CellType.Dead;

                case 2:
                    return currentCellState == CellType.Alive ? CellType.Alive : CellType.Dead;

                case 3:
                    return CellType.Alive;

                default:
                    return CellType.Dead;
            }
        }

        private void RandomizeBoard()
        {
            var random = new Random(Guid.NewGuid().GetHashCode());

            for (int i = 0; i < _board.GetLength(0); i++)
            {
                for (int j = 0; j < _board.GetLength(1); j++)
                {
                    _board[i, j] = random.Next(0, 100) < ChanceOfCreation * 100 ? CellType.Alive : CellType.Dead;
                }
            }
        }
    }
}