namespace CommandEngine
{
    public class CommandEngineQuitResponse : CommandEngineResponse
    {
        public CommandEngineQuitResponse()
        {
            ResponseCode = CommandResponseCode.Quit;
            ResponseBody = "Quitting Game";
        }
    }
}
