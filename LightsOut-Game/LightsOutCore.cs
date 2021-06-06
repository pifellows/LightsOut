using System;

namespace LightsOut_Game
{

    public class LightsOutCore
    {
        private bool[,] _Grid;
        private bool _GameOver { get; set; }
        private int _MoveCounter { get; set; }

        private readonly int MaxX = 5;
        private readonly int MaxY = 5;

        public bool InitaliseGame(ISeeder gridSeeder)
        {
            _GameOver = false;
            _MoveCounter = 0;
            _Grid = new bool[MaxY, MaxX];
            gridSeeder.Seed(_Grid);
            return true;
        }

        public bool[,] GetGrid()
        {
            if (_Grid == null)
            {
                // if we are uninitalised, return null
                return null;
            }
            var returnState = new bool[MaxY, MaxX];
            Array.Copy(_Grid, returnState, _Grid.Length);
            return returnState;
        }

        public bool MakeMove(int x, int y)
        {
            if (_GameOver)
            {
                return _GameOver;
            }

            ToggleCells(x, y);
            _MoveCounter++;
            return CheckGameComplete();
        }

        public bool IsGameComplete()
        {
            return _GameOver;
        }

        private bool CheckGameComplete()
        {
            for (int i = 0; i < MaxY; i++)
            {
                for (int j = 0; j < MaxX; j++)
                {
                    if (_Grid[i, j] == true)
                    {
                        _GameOver = false;
                        return _GameOver;
                    }
                }
            }
            _GameOver = true;
            return _GameOver;
        }

        private void ToggleCells(int x, int y)
        {
            // This is the standard "plus sign" selection - 1 up, 1 down, 1 left, 1 right and self
            for (var i = x - 1; i < x + 2; i++)
            {
                // toggle the horizontal line
                if (i < 0 || MaxX <= i)
                {
                    continue;
                }
                _Grid[y, i] = !_Grid[y, i];
            }

            for (var j = y - 1; j < y + 2; j++)
            {
                // toggle the vertical line
                if (j < 0 || MaxY <= j)
                {
                    continue;
                }
                _Grid[j, x] = !_Grid[j, x];
            }

            // toggle the centrepoint, as we have toggled it twice
            _Grid[y, x] = !_Grid[y, x];
        }

        public int GetMoveCounter()
        {
            return _MoveCounter;
        }
    }
}
