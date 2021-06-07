namespace CommandEngine
{
    public class CommandEngineBadMoveMadeResponse : CommandEngineResponse
    {
        public CommandEngineBadMoveMadeResponse()
        {
            ResponseCode = CommandResponseCode.Warning;
            ResponseBody = "Move Made out of range";

        }
    }
}