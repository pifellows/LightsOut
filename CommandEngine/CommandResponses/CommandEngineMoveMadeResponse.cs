using System;

namespace CommandEngine
{
    public class CommandEngineMoveMadeResponse : CommandEngineResponse
    {
        public CommandEngineMoveMadeResponse(bool[,] grid)
        {
            ResponseCode = CommandResponseCode.Success;
            ResponseBody = SerialiseGrid(grid);

        }
    }
}
