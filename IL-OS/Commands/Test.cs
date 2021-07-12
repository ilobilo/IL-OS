using System;

namespace IL_OS
{
    public static class Test
    {
        public static void Run(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("This is a test command");
            }
            else
            {
                Console.WriteLine($"This is a test command with {args.Length} arguments.");
            }

            /*for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-h":
                        Console.WriteLine(Shell.cmds[0].Help);
                        break;

                    default:
                        Console.WriteLine("Default");
                        break;
                }
            }*/
        }

        public static string RunGui(string[] args)
        {
            if (args.Length < 1)
            {
                return "This is a test command";
            }
            else
            {
                return $"This is a test command with {args.Length} arguments.";
            }
        }
    }
}