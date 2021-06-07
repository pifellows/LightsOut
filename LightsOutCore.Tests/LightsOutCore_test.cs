using NUnit.Framework;
using LightsOutCore;
using System;

namespace LightsoutCore.Tests
{
    [TestFixture]
    public class InitaliseGame_Tests
    {
        [Test]
        public void InitaliseGame_5by5_Success()
        {
            // Arrange
            var game = new LightsOutCore();

            // Act
            var result = game.InitaliseGame(new LightsOut_Game.Seeder.BlankSeeder());

            // Assert
            Assert.IsTrue(result, "Initalising the game should have been successful");
        }

        [Test]
        public void InitaliseGame_DifferentSeeders_Success()
        {
            // Arrange
            var game1 = new LightsOutCore();
            var game2 = new LightsOutCore();

            // Act
            var result1 = game1.InitaliseGame(new LightsOut_Game.Seeder.BlankSeeder());
            var result2 = game2.InitaliseGame(new LightsOut_Game.Seeder.RandomSeeder(4));

            var grid1 = game1.GetGrid();
            var grid2 = game2.GetGrid();

            // Assert
            Assert.AreNotEqual(grid1, grid2, "games should have been initalised randomly so should not be equal");
        }
    }

    [TestFixture]
    public class GetGrid_Tests
    {
        [Test]
        public void GetGrid_Uninitalised_Error()
        {
            // Arrange
            var game = new LightsOutCore();

            // Act
            var result = game.GetGrid();

            // Assert
            Assert.IsNull(result, "should not be returning an initalised result");
        }

        [Test]
        public void GetGrid_5by5_Success()
        {
            // Arrange
            var game = new LightsOutCore();

            //Act
            var success = game.InitaliseGame(new LightsOut_Game.Seeder.BlankSeeder());
            var result = game.GetGrid();

            // Assert
            Assert.AreEqual((5, 5), (result.GetLength(1), result.GetLength(0)), $"grid should be of size (5,5), got {(result.GetLength(1), result.GetLength(0))}");
        }

        [Test]
        public void GetGrid_ReturnedGridShouldNotEffectGameGrid_Success()
        {
            // Arrange
            var game = new LightsOutCore();

            //Act
            var success = game.InitaliseGame(new LightsOut_Game.Seeder.BlankSeeder());
            var result1 = game.GetGrid();

            result1[0, 0] = true;

            var result2 = game.GetGrid();

            // Assert
            Assert.AreNotEqual(result1, result2, $"game grid should not have been modified, result1={result1}, result2={result2}");
        }
    }

    [TestFixture]
    public class MakeMove_Tests
    {
        [Test]
        public void MakeMove_FirstMoveEmptyGrid_Success()
        {
            // Arrange
            bool[,] want = new bool[,]
            {
                { false, false, false, false, false},
                { false, false, true, false, false},
                { false, true, true, true, false},
                { false, false, true, false, false},
                { false, false, false, false, false}
            };
            var game = new LightsOutCore();
            var success = game.InitaliseGame(new LightsOut_Game.Seeder.BlankSeeder());

            //Act
            game.MakeMove(2, 2);
            var result = game.GetGrid();

            // Assert
            Assert.AreEqual(want, result, $"result should be identical, want={want}, got={result}");
        }

        [Test]
        public void MakeMove_FirstMoveEmptyGridPartiallyOutOfRangeTopLeft_Success()
        {
            // Arrange
            bool[,] want = new bool[,]
            {
                { true, true, false, false, false},
                { true, false, false, false, false},
                { false, false, false, false, false},
                { false, false, false, false, false},
                { false, false, false, false, false}
            };
            var game = new LightsOutCore();
            var success = game.InitaliseGame(new LightsOut_Game.Seeder.BlankSeeder());

            //Act
            game.MakeMove(0, 0);
            var result = game.GetGrid();

            // Assert
            Assert.AreEqual(want, result, $"result should be identical, want={want}, got={result}");
        }

        [Test]
        public void MakeMove_FirstMoveEmptyGridPartiallyOutOfRangeTopRight_Success()
        {
            // Arrange
            bool[,] want = new bool[,]
            {
                { false, false, false, true, true},
                { false, false, false, false, true},
                { false, false, false, false, false},
                { false, false, false, false, false},
                { false, false, false, false, false}
            };
            var game = new LightsOutCore();
            var success = game.InitaliseGame(new LightsOut_Game.Seeder.BlankSeeder());

            //Act
            game.MakeMove(4, 0);
            var result = game.GetGrid();

            // Assert
            Assert.AreEqual(want, result, $"result should be identical, want={want}, got={result}");
        }

        [Test]
        public void MakeMove_FirstMoveEmptyGridPartiallyOutOfRangeBottomRight_Success()
        {
            // Arrange
            bool[,] want = new bool[,]
            {
                { false, false, false, false, false},
                { false, false, false, false, false},
                { false, false, false, false, false},
                { false, false, false, false, true},
                { false, false, false, true, true}
            };
            var game = new LightsOutCore();
            var success = game.InitaliseGame(new LightsOut_Game.Seeder.BlankSeeder());

            //Act
            game.MakeMove(4, 4);
            var result = game.GetGrid();

            // Assert
            Assert.AreEqual(want, result, $"result should be identical, want={want}, got={result}");
        }

        [Test]
        public void MakeMove_FirstMove_ReturnsFalse()
        {
            // Arrange
            var game = new LightsOutCore();
            var success = game.InitaliseGame(new LightsOut_Game.Seeder.BlankSeeder());

            //Act
            var result = game.MakeMove(2, 2);

            // Assert
            Assert.IsFalse(result, $"game should have reported as not finished");
        }

        [Test]
        public void MakeMove_FinalMove_ReturnsTrue()
        {
            // Arrange
            var game = new LightsOutCore();
            var success = game.InitaliseGame(new LightsOut_Game.Seeder.BlankSeeder());

            //Act
            game.MakeMove(2, 2);
            var result = game.MakeMove(2, 2);

            // Assert
            Assert.IsTrue(result, $"game should have reported as finished");
        }

        [Test]
        public void MakeMove_AfterFinalMoveMakeAdditionalMoves_ReturnsTrue()
        {
            // Arrange
            var game = new LightsOutCore();
            var success = game.InitaliseGame(new LightsOut_Game.Seeder.BlankSeeder());

            //Act
            game.MakeMove(2, 2);
            game.MakeMove(2, 2);
            var result = game.MakeMove(2, 2);

            // Assert
            Assert.IsTrue(result, $"game should have reported as finished");
        }

    }

    [TestFixture]
    public class IsGameComplete_Tests
    {
        [Test]
        public void IsGameComplete_GameNotFinished_False()
        {
            // Arrange
            var game = new LightsOutCore();

            // Act
            game.InitaliseGame(new LightsOut_Game.Seeder.BlankSeeder());
            var result = game.IsGameComplete();

            // Assert
            Assert.IsFalse(result, "game should not be marked as over");
        }

        [Test]
        public void IsGameComplete_GameIsFinished_True()
        {
            // Arrange
            var game = new LightsOutCore();
            var success = game.InitaliseGame(new LightsOut_Game.Seeder.BlankSeeder());

            //Act
            game.MakeMove(2, 2);
            game.MakeMove(2, 2);
            var result = game.IsGameComplete();

            // Assert
            Assert.IsTrue(result, $"game should have reported as finished");
        }
    }

    [TestFixture]
    public class ResettingGame_Tests
    {
        [Test]
        public void ResettingGame_ResetUnfinishedGame()
        {
            // Arrange
            var game = new LightsOutCore();
            game.InitaliseGame(new LightsOut_Game.Seeder.BlankSeeder());
            game.MakeMove(2, 2);

            // Act
            game.InitaliseGame(new LightsOut_Game.Seeder.BlankSeeder());
            var result = game.IsGameComplete();

            // Assert
            Assert.IsFalse(result, "game should have been reset");
        }

        [Test]
        public void ResettingGame_ResetFinishedGame()
        {
            // Arrange
            var game = new LightsOutCore();
            var success = game.InitaliseGame(new LightsOut_Game.Seeder.BlankSeeder());
            game.MakeMove(2, 2);
            game.MakeMove(2, 2);
            var finished = game.IsGameComplete();

            //Act

            game.InitaliseGame(new LightsOut_Game.Seeder.BlankSeeder());
            var unfinished = game.IsGameComplete();

            // Assert
            Assert.AreNotEqual(finished, unfinished, $"completed game should have been reset");
        }
    }

    [TestFixture]
    public class MoveCounter_Tests
    {
        [Test]
        public void GetMoveCounter_NoMovesMade_Zero()
        {
            // Arrange
            var game = new LightsOutCore();
            game.InitaliseGame(new LightsOut_Game.Seeder.BlankSeeder());

            // Act
            var result = game.GetMoveCounter();

            // Assert
            Assert.AreEqual(0, result, "game should not have registered any moves");
        }

        [Test]
        public void GetMoveCounter_OneMoveMade_One()
        {
            // Arrange
            var game = new LightsOutCore();
            game.InitaliseGame(new LightsOut_Game.Seeder.BlankSeeder());

            // Act
            game.MakeMove(2, 2);
            var result = game.GetMoveCounter();

            // Assert
            Assert.AreEqual(1, result, "game should have registered a single move");
        }

        [Test]
        public void GetMoveCounter_SevenMade_Seven()
        {
            // Arrange
            var game = new LightsOutCore();
            game.InitaliseGame(new LightsOut_Game.Seeder.BlankSeeder());

            // Act
            game.MakeMove(1, 0);
            game.MakeMove(1, 1);
            game.MakeMove(1, 2);
            game.MakeMove(1, 3);
            game.MakeMove(1, 4);
            game.MakeMove(4, 1);
            game.MakeMove(3, 1);
            var result = game.GetMoveCounter();

            // Assert
            Assert.AreEqual(7, result, "game registered incorrect number of moves");
        }

        [Test]
        public void GetMoveCounter_NoIncreaseAfterGameComplete_Two()
        {
            // Arrange
            var game = new LightsOutCore();
            game.InitaliseGame(new LightsOut_Game.Seeder.BlankSeeder());

            // Act
            game.MakeMove(2, 2);
            game.MakeMove(2, 2);
            var result = game.GetMoveCounter();
            game.MakeMove(2, 2);

            // Assert
            Assert.AreEqual(2, result, "game should not have registered any additional moves");
        }
    }
}