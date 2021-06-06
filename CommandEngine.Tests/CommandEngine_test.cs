using NUnit.Framework;
using CommandEngine;
using LightsOut_Game.Seeder;
using System;

namespace CommandEngineTests
{
    public class CommandEngineTests
    {
        [TestCase ("help")]
        [TestCase ("Help")]
        [TestCase ("HELP")]
        [TestCase ("HeLp")]
        [TestCase ("HeLp   ")]
        public void ProcessCommand_Help_Success(string command)
        {
            // Arrange
            var engine = new CommandEngine.CommandEngine(new BlankSeeder());
            var want = new CommandEngineHelpResponse();

            // Act
            var response = engine.Process(command);

            // Assert
            Assert.AreEqual(want.ToString(), response.ToString(), "help command should have returned the help response");
        }

        [TestCase("reset")]
        [TestCase("Reset")]
        [TestCase("RESET")]
        [TestCase("ReSet   ")]
        public void ProcessCommand_Reset_Success(string command)
        {
            // Arrange
            var engine = new CommandEngine.CommandEngine(new BlankSeeder());
            var want = new CommandEngineResetResponse(true, new bool[,]{
            {false, false, false, false, false },
            {false, false, false, false, false },
            {false, false, false, false, false },
            {false, false, false, false, false },
            {false, false, false, false, false },
            });

            // Act
            var response = engine.Process(command);

            // Assert
            Assert.AreEqual(want.ToString(), response.ToString(), "reset command should have returned the reset response");
        }

        [TestCase("Quit")]
        [TestCase("QUIT")]
        [TestCase("QuIt")]
        [TestCase("quit   ")]
        public void ProcessCommand_Quit_Success(string command)
        {
            // Arrange
            var engine = new CommandEngine.CommandEngine(new BlankSeeder());
            var want = new CommandEngineQuitResponse();

            // Act
            var response = engine.Process(command);

            // Assert
            Assert.AreEqual(want.ToString(), response.ToString(), "quit command should have returned the quit response");
        }

        [TestCase("1 1")]
        [TestCase("1 1   ")]
        [TestCase("   1 1   ")]
        public void ProcessCommand_Command_Success(string command)
        {
            // Arrange
            var engine = new CommandEngine.CommandEngine(new BlankSeeder());
            var want = new CommandEngineMoveMadeResponse(null);
            want.ResponseBody = @".*...
***..
.*...
.....
.....
";

            // Act
            var response = engine.Process(command);

            // Assert
            Assert.AreEqual(want.ToString(), response.ToString(), "move command should have returned a move response");
        }

        [TestCase("a b")]
        [TestCase("a b   ")]
        [TestCase("   a b   ")]
        public void ProcessCommand_Command_Fail(string command)
        {
            // Arrange
            var engine = new CommandEngine.CommandEngine(new BlankSeeder());
            var want = new CommandEngineParseMoveFailedResponse(command.Trim().ToLower());

            // Act
            var response = engine.Process(command);

            // Assert
            Assert.AreEqual(want.ToString(), response.ToString(), "move response should have returned the fail move parse string");
        }

        [Test]
        public void ProcessCommand_GameEnd_Success()
        {
            // Arrange
            var engine = new CommandEngine.CommandEngine(new BlankSeeder());
            var want = new CommandEngineGameOverResponse(2);
            var command = "2 2";

            // Act
            engine.Process(command);
            var response = engine.Process(command);

            // Assert
            Assert.AreEqual(want.ToString(), response.ToString(), "move response should have returned the game over string");
        }

        [TestCase("unknown")]
        public void ProcessCommand_Unknown_Failure(string command)
        {
            // Arrange
            var engine = new CommandEngine.CommandEngine(new BlankSeeder());
            var want = new CommandEngineUnknownResponse(command);

            // Act
            var response = engine.Process(command);

            // Assert
            Assert.AreEqual(want.ToString(), response.ToString(), "move command should have returned the unknown response");
        }
    }
}