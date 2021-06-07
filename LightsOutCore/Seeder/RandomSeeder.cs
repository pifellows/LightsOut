using System;

namespace LightsOut_Game.Seeder
{
    public class RandomSeeder : ISeeder
    {
        public RandomSeeder(int numberToSeed)
        {
            _NumberToSeed = numberToSeed;
        }

        public int _NumberToSeed { get; }

        public void Seed(bool[,] grid)
        {
            int toSeed; // the number of cells to activate

            // if we have a negative number, do not seed
            if (_NumberToSeed < 0)
            {
                toSeed = 0;
            }

            // in order to be reasonable, we want the maximum number of allowed seeds to be 
            // a maximum of sqrt total grid size, floored
            var maximumSeedValue = (int)Math.Sqrt(grid.GetLength(0) * grid.GetLength(1));
            if (_NumberToSeed > maximumSeedValue)
            {
                toSeed = maximumSeedValue;
            }
            else
            {
                toSeed = _NumberToSeed;
            }

            var seeded = 0;
            var r = new Random();
            while (seeded < toSeed)
            {
                var x = r.Next(grid.GetLength(1));
                var y = r.Next(grid.GetLength(0));

                if (grid[x, y] == true)
                {
                    continue;
                }
                grid[x, y] = true;
                seeded++;
            }
        }
    }
}
