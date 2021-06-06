using System;

namespace LightsOutCommandLine
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Lights Out");
            var engine = new CommandEngine.CommandEngine();

            if(args.Length != 1 || args[0].Trim().ToLower() != "start")
            {
                var help = engine.Process("help");
                Console.WriteLine(help.ResponseBody);
                return;
            }

            var reset = engine.Process("reset");
            if(reset.ResponseCode != CommandEngine.CommandResponseCode.Success)
            {
                Console.WriteLine($"Error starting game [{reset.ResponseBody}]");
                return;
            }

            Console.WriteLine(reset.ResponseBody);

            while (true)
            {
                var exit = false;
                var command = Console.ReadLine();
                var response = engine.Process(command);

                switch (response.ResponseCode)

                {
                    case CommandEngine.CommandResponseCode.Success:
                        Console.WriteLine(response.ResponseBody);
                        break;
                    case CommandEngine.CommandResponseCode.Warning:
                        Console.WriteLine("Warning: " + response.ResponseBody.Trim());
                        break;
                    case CommandEngine.CommandResponseCode.Error:
                        Console.WriteLine("Error: " + response.ResponseBody.Trim());
                        exit = true;
                        break;
                    case CommandEngine.CommandResponseCode.Quit:
                        exit = true;
                        break;
                    case CommandEngine.CommandResponseCode.GameOver:
                        Console.WriteLine("Congratulations! " + response.ResponseBody.Trim());
                        break;
                }
                if (exit)
                {
                    break;
                }
            }
            Console.WriteLine("Quitting game...");
        }
    }
}
