using System;

namespace CommandEngine
{
    public enum CommandResponseCode
    {
        Quit = 100,
        Success = 200,
        GameOver = 300,
        Warning = 400,
        Error = 500
    }

    public class CommandEngineResponse
    {
        public CommandResponseCode ResponseCode { get; set; }
        public string ResponseBody { get; set; }

        public override string ToString()
        {
            return $"{ResponseCode} : {ResponseBody}";
        }

        protected string SerialiseGrid(bool[,] grid)
        {
            if (grid == null)
            {
                return "";
            }
            var gridString = "";
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    if (grid[i, j] == true)
                    {
                        gridString += "*";
                    }
                    else
                    {
                        gridString += ".";
                    }
                }
                gridString += Environment.NewLine;
            }
            return gridString;
        }
    }
}
