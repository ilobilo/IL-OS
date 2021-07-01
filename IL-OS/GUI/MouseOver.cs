using System;
using GUI;
using System.Drawing;
using Cosmos.System;

namespace Controls
{
    public class MouseOver
    {
        public static bool Button(Button button)
        {
            if (IL_OS.Kernel.HasClicked() == true)
            {
                if (MouseManager.X > button.X && MouseManager.X < (button.X + button.width))
                {
                    if (MouseManager.Y > button.Y && MouseManager.Y < (button.Y + button.height))
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            return false;
        }
    }
}