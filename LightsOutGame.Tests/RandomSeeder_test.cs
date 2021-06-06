using NUnit.Framework;
using LightsOut_Game;
using System;

namespace RandomSeeder.Tests
{
    [TestFixture]
    class RandomSeeder_test
    {
        public bool[,] GetDefaultGrid()
        {
            return new bool[,] {
                { false, false, false, false, false},
                { false, false, false, false, false},
                { false, false, false, false, false},
                { false, false, false, false, false},
                { false, false, false, false, false},
            };
        }

        public int CountNumberOfActivatedCells(bool[,] grid)
        {
            int count = 0;

            for (int i = 0; i < grid.GetLength(1); i++)
            {
                for (int j = 0; j < grid.GetLength(0); j++)
                {
                    if (grid[i,j] == true)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        [Test]
        public void RandomSeeder_SeedsRandomly_Success()
        {
            // Arrange
            var randomSeeder1 = new LightsOut_Game.Seeder.RandomSeeder(4);
            var grid1 = GetDefaultGrid();

            var randomSeeder2 = new LightsOut_Game.Seeder.RandomSeeder(4);
            var grid2 = GetDefaultGrid();

            // Act
            randomSeeder1.Seed(grid1);
            randomSeeder2.Seed(grid2);

            // Assert
            Assert.AreNotEqual(grid1, grid2, "Initalised grids should have been different due to random seed");
        }

        [TestCase (0)]
        [TestCase (1)]
        [TestCase (3)]
        [TestCase (5)]
        public void RandomSeeder_AlterSeedCount_Values(int numberToSeed)
        {
            // Arrange
            var randomSeeder = new LightsOut_Game.Seeder.RandomSeeder(numberToSeed);
            var grid = GetDefaultGrid();


            // Act
            randomSeeder.Seed(grid);
            var actualCount = CountNumberOfActivatedCells(grid);

            // Assert
            Assert.AreEqual(numberToSeed, actualCount, $"grid has not been seeded with the correct amount of cells, want: {numberToSeed}, got: {actualCount}");
        }

        [TestCase(-1)]
        public void RandomSeeder_AlterSeedCount_ShouldReturn0(int numberToSeed)
        {
            // Arrange
            var randomSeeder = new LightsOut_Game.Seeder.RandomSeeder(numberToSeed);
            var grid = GetDefaultGrid();
            var expectedCellsActivated = 0;

            // Act
            randomSeeder.Seed(grid);
            var actualCount = CountNumberOfActivatedCells(grid);

            // Assert
            Assert.AreEqual(expectedCellsActivated, actualCount, $"grid should not have been seeded");
        }

        [Test]
        public void RandomSeeder_ClampNumberOfSeeds_Success()
        {
            // Arrange
            var numberToRequest = 25;
            int maximumCount = (int)Math.Sqrt(25);
            var randomSeeder = new LightsOut_Game.Seeder.RandomSeeder(numberToRequest);
            var grid = GetDefaultGrid();

            // Act
            randomSeeder.Seed(grid);
            var actualCount = CountNumberOfActivatedCells(grid);

            // Assert
            Assert.AreEqual(maximumCount, actualCount, $"number of seeded cells should have been clamped. wanted: {maximumCount}, got: {actualCount}");
        }
    }
}
