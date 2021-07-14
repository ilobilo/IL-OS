using System.Collections.Generic;
using System.IO;
using System;
using Cosmos.System;
using System.Drawing;
using GUI;

namespace IL_OS
{
    class Terminal : Window
    {
        public string Content = "";
        public string aContent = "";

        public string Command = "";

        List<string> s;
        int MaxLine = 0;

        public Terminal()
        {
            Title = "Terminal";

            s = new List<string>();

            New();
        }

        int WI = 0;
        KeyEvent keyEvent;
        
        public override void InputUpdate()
        {
            //keyEvent.KeyChar = '\0';
            if (KeyboardManager.TryReadKey(out keyEvent))
            {
                switch (keyEvent.Key)
                {
                    case ConsoleKeyEx.Backspace:
                        if (ContinuableCommand == "")
                        {
                            if (Content.Length != 0)
                            {
                                Content = Content.Substring(0, Content.Length - 1);
                            }
                            if (Command.Length != 0)
                            {
                                Command = Command.Substring(0, Command.Length - 1);
                            }
                        }
                        break;
                    case ConsoleKeyEx.Enter:
                        if (ContinuableCommand == "")
                        {
                            Content += "\n";
                            ProcessCommand();
                            Command = "";

                            New();
                        }
                        break;
                    case ConsoleKeyEx.Escape:
                        ContinuableCommand = "";
                        break;
                    default:
                        if (ContinuableCommand == "")
                        {
                            Content += keyEvent.KeyChar;
                            Command += keyEvent.KeyChar;
                        }
                        break;
                }
            }
        }

        string ContinuableCommand = "";
        string ContinuableCommandOutput = "";

        private void ProcessCommand()
        {
            if (string.IsNullOrWhiteSpace(Command))
            {
                return;
            }
            Command = Command + " ";
            string command = Command.Split(" ", StringSplitOptions.RemoveEmptyEntries)[0].ToLower();
            int i = Command.IndexOf(" ") + 1;
            string[] args = Command.Substring(i).Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            switch (command)
            {
                case "test":
                    Content += Test.RunGui(args) + "\n";
                    break;

                case "winlist":
                    foreach (var item in Kernel.windows)
                    {
                        Content += item.Title + "\n";
                    }
                    Content += Kernel.Focused.ToString();
                    break;

                default:
                    Content += "Command not found!\n";
                    break;
            }
        }

        public void New()
        {
            var current_dir = Directory.GetCurrentDirectory();
            if (Kernel.FS)
            {
                Content += $"0:\\{current_dir} $ "; 
            }
            else
            {
                Content += "$ ";
            }
        }

        public override void UIUpdate()
        {
            MaxLine = (H - Bar) / 15;

            if (WI < 60)
            {
                WI++;
            }
            else
            {
                WI = 0;
            }

            s.Clear();

            switch (ContinuableCommand)
            {
                default:
                    ContinuableCommandOutput = "";
                    break;
            }


            if (WI < 30)
            {
                aContent = Content + ContinuableCommandOutput + "_";
            }
            else
            {
                aContent = Content + ContinuableCommandOutput;
            }

            string l = "";
            int i = 0;
            foreach (var v in aContent)
            {
                if (i < ((W - 7) / 7) && v != '\n')
                {
                    i++;
                    l += v;
                }
                else
                {
                    s.Add(l);
                    i = 0;
                    l = "";

                    if (v != '\n')
                    {
                        l += v;
                    }
                }
            }
            s.Add(l);


            if (s.Count > MaxLine)
            {
                while (s.Count != MaxLine)
                {
                    s.RemoveAt(0);
                }
            }

            int k = 0;
            foreach (var v in s)
            {
                Kernel.vMWareSVGAII.DrawACSIIString(v, (uint)Color.White.ToArgb(), (uint)(X + 3), (uint)(Y + Bar + k * 15));
                k++;
            }
        }
    }
}