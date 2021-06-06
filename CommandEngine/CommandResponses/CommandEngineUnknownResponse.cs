namespace CommandEngine
{
    public class CommandEngineUnknownResponse : CommandEngineResponse
    {
        public CommandEngineUnknownResponse(string command)
        {
            ResponseCode = CommandResponseCode.Warning;
            ResponseBody = $"Unknown command [{command}]";
        }
    }
}
