using System;

namespace CommandEngine
{
    public class CommandEngine
    {
        private readonly LightsOut_Game.LightsOutCore _Game;
        private readonly LightsOut_Game.ISeeder _GameSeeder;

        public CommandEngine()
        {
            _GameSeeder = new LightsOut_Game.Seeder.RandomSeeder(5);
            _Game = new LightsOut_Game.LightsOutCore();
            _Game.InitaliseGame(_GameSeeder);
        }

        public CommandEngine(LightsOut_Game.ISeeder gameSeeder)
        {
            _GameSeeder = gameSeeder;
            _Game = new LightsOut_Game.LightsOutCore();
            _Game.InitaliseGame(_GameSeeder);
        }

        public CommandEngineResponse Process(string command)
        {
            command = command.Trim().ToLower();
            if (command == "help")
            {
                return new CommandEngineHelpResponse();
            }
            if (command == "quit")
            {
                return new CommandEngineQuitResponse();
            }
            if (command == "reset")
            {
                var success = _Game.InitaliseGame(_GameSeeder);
                var grid = _Game.GetGrid();
                return new CommandEngineResetResponse(success, grid);
            }

            var tokens = command.Split();
            if (tokens.Length == 2)
            {
                if (!int.TryParse(tokens[0], out var x) ||
                    !int.TryParse(tokens[1], out var y))
                {
                    return new CommandEngineParseMoveFailedResponse(command);
                }
                var successfulMove = _Game.MakeMove(x, y);
                if (successfulMove)
                {
                    if (_Game.IsGameComplete())
                    {
                        return new CommandEngineGameOverResponse(_Game.GetMoveCounter());
                    }
                    return new CommandEngineMoveMadeResponse(_Game.GetGrid());
                }
                return new CommandEngineBadMoveMadeResponse();
            }

            return new CommandEngineUnknownResponse(command);

        }
    }
}
