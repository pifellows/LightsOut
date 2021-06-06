namespace CommandEngine
{
    public class CommandEngineResetResponse : CommandEngineResponse
    {
        public CommandEngineResetResponse(bool success, bool[,] grid)
        {
            if (success)
            {
                ResponseCode = CommandResponseCode.Success;
                ResponseBody = SerialiseGrid(grid);
            } else
            {
                ResponseCode = CommandResponseCode.Error;
                ResponseBody = "Failed to Reset Game";
            }
            
        }
    }
}