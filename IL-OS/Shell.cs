using System;
using System.Collections.Generic;
using Console = System.Console;
using System.IO;

namespace IL_OS
{
    public static class Shell
    {
        #region Database
        public struct Command
        {
            public string Name, Help;
            public function func;
        }
        
        public static List<Command> cmds = new List<Command>();
        public delegate void function(string[] args);
        #endregion

        #region Main
        public static void Run()
        {
            var current_dir = Directory.GetCurrentDirectory();
            RegisterCommands();

            string input = string.Empty;
            if (Kernel.FS == true)
            {
                Console.Write($"0:\\{current_dir} $ "); 
            }
            else
            {
                Console.Write("$ ");
            }
            input = Console.ReadLine() + " ";

            if ((string.IsNullOrWhiteSpace(input)) || (input.Length == 0))
            {
                return;
            }

            string command = input.Split(" ")[0].ToLower();
            int i = input.IndexOf(" ") + 1;
            string[] args = input.Substring(i).Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            foreach (Command cmd in cmds)
            {
                if (command.Equals(cmd.Name))
                {
                    cmd.func(args);
                    return;
                }
            }
            Tools.SetColor(ConsoleColor.Red);
            Tools.Message("Command not found!");
            Tools.SetColor(ConsoleColor.White);
        }
        #endregion

        #region Add commands
        private static void Add(string name, string desc, function func)
        {
            Command a = new Command();
            a.Name = name;
            a.Help = desc;
            a.func = func;

            cmds.Add(a);
        }

        public static void RegisterCommands()
        {
            Add("test", "Test Command", Test.Run);
        }
        #endregion
    }
}