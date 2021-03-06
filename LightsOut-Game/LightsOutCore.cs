using System;

namespace LightsOut_Game
{

    public class LightsOutCore
    {
        private bool[,] _Grid;
        private bool _GameOver { get; set; }
        private int _MoveCounter { get; set; }

        private readonly int SizeX = 5;
        private readonly int SizeY = 5;
        private readonly int MaxX = 4;
        private readonly int MaxY = 4;

        public bool InitaliseGame(ISeeder gridSeeder)
        {
            _GameOver = false;
            _MoveCounter = 0;
            _Grid = new bool[SizeY, SizeX];
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
            var returnState = new bool[SizeY, SizeX];
            Array.Copy(_Grid, returnState, _Grid.Length);
            return returnState;
        }

        public bool MakeMove(int x, int y)
        {
            if (_GameOver)
            {
                return true;
            }

            if(x < 0 || MaxX < x || y < 0 || MaxY < y)
            {
                return false;
            }

            ToggleCells(x, y);
            _MoveCounter++;
            CheckGameComplete();
            return true;
        }

        public bool IsGameComplete()
        {
            return _GameOver;
        }

        private void CheckGameComplete()
        {
            for (int i = 0; i < SizeY; i++)
            {
                for (int j = 0; j < SizeX; j++)
                {
                    if (_Grid[i, j] == true)
                    {
                        _GameOver = false;
                        return;
                    }
                }
            }
            _GameOver = true;
        }

        private void ToggleCells(int x, int y)
        {
            // This is the standard "plus sign" selection - 1 up, 1 down, 1 left, 1 right and self
            for (var i = x - 1; i < x + 2; i++)
            {
                // toggle the horizontal line
                if (i < 0 || SizeX <= i)
                {
                    continue;
                }
                _Grid[y, i] = !_Grid[y, i];
            }

            for (var j = y - 1; j < y + 2; j++)
            {
                // toggle the vertical line
                if (j < 0 || SizeY <= j)
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
