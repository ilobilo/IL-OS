/*using Cosmos.System;
using System.Drawing;
using GUI;

namespace IL_OS
{
    public static class Terminal
    {
        public static int X = 100;
        public static int Y = 100;

        public static int OX;
        public static int OY;

        public static int W = 700;
        public static int H = 500;

        public static int Bar = 20;

        public static string Tittle = "Terminal";

        public static bool Opened;
        public static bool Move = false;

        public static void Update(DoubleBufferedVMWareSVGAII vMWareSVGAII)
        {
            if (Opened != true)
            {
                return;
            }

            if (Kernel.Pressed)
            {
                if (Kernel.Pressed && MouseManager.X > X && MouseManager.X < X + W - Bar && MouseManager.Y > Y && MouseManager.Y < Y + Bar)
                {
                    Move = true;

                    OX = (int)(MouseManager.X - X);
                    OY = (int)(MouseManager.Y - Y);
                }
                else if (Kernel.Pressed && MouseManager.X > X + W - Bar && MouseManager.X < X + W && MouseManager.Y > Y && MouseManager.Y < Y + Bar)
                {
                    Opened = false;
                }
            }
            else
            {
                Move = false;
            }
            
            if (Move)
            {
                X = (int)(MouseManager.X - OX);
                Y = (int)(MouseManager.Y - OY);
            }

            if (Y < 25)
            {
                Y = 25;
            }

            // Bar
            vMWareSVGAII.DoubleBuffer_DrawFillRectangle((uint)X, (uint)Y, (uint)(W - Bar), (uint)Bar, (uint)Color.Gray.ToArgb());
            // Tittle
            vMWareSVGAII.DrawACSIIString(Tittle, (uint)Color.Blue.ToArgb(), (uint)(X + 3), (uint)(Y + 3));
            // Hide Button
            vMWareSVGAII.DoubleBuffer_DrawFillRectangle((uint)(X + W - Bar), (uint)Y, (uint)Bar, (uint)Bar, (uint)Color.Brown.ToArgb());
            // Main Form
            vMWareSVGAII.DoubleBuffer_DrawFillRectangle((uint)X, (uint)(Y + Bar), (uint)W, (uint)(H - Bar), (uint)Color.Black.ToArgb());
            // Border
            vMWareSVGAII.DoubleBuffer_DrawRectangle((uint)Color.White.ToArgb(), X - 1, Y - 1, W + 1, H + 1);
        }
    }
}*/