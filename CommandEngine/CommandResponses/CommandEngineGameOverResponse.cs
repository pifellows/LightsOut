namespace CommandEngine
{
    public class CommandEngineGameOverResponse : CommandEngineResponse
    {
        public CommandEngineGameOverResponse(int movesMade)
        {
            ResponseCode = CommandResponseCode.GameOver;
            ResponseBody = $"Game over! Completed in {movesMade} moves";
        }
    }
}
