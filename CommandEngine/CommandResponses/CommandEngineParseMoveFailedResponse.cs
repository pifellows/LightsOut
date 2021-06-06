namespace CommandEngine
{
    public class CommandEngineParseMoveFailedResponse : CommandEngineResponse
    {
        public CommandEngineParseMoveFailedResponse(string command)
        {
            ResponseCode = CommandResponseCode.Warning;
            ResponseBody = $"Failed to parse the move command [{command}]";
        }
    }
}