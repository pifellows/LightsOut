namespace CommandEngine
{
    public class CommandEngineHelpResponse : CommandEngineResponse
    {
        public CommandEngineHelpResponse()
        {
            ResponseCode = CommandResponseCode.Success;
            ResponseBody = @"Lights Out
Start a game by calling the application with the 'start' arguement
When in a game:
    Submit a command by pressing enter
    Commands:
        - help  displays this help message
        - reset discard all progress and start a new game
        - quit  quit the application
        - x y   make a move at the specified coordinate (zero indexed)";
        }
    }
}
