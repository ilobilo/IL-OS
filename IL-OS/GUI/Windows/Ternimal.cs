using Cosmos.System;
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

        public static bool Opened;
        
        public static void Update(DoubleBufferedVMWareSVGAII vMWareSVGAII)
        {
            if (!Opened)
            {
                return;
            }

            OX = (int)(MouseManager.X - X);
            OY = (int)(MouseManager.Y - Y);

            if (MouseManager.MouseState == MouseState.Left && MouseManager.X > X && MouseManager.X < X + W - Bar && MouseManager.Y > Y && MouseManager.Y < Y + Bar)
            {
                X = (int)(MouseManager.X - (MouseManager.X - X));
                Y = (int)(MouseManager.Y - (MouseManager.Y - Y));
            }
            else if (Kernel.Pressed && MouseManager.X > X + W - Bar && MouseManager.X < X + W && MouseManager.Y > Y && MouseManager.Y < Y + Bar)
            {
                Opened = false;
            }
            
            // Bar
            vMWareSVGAII.DoubleBuffer_DrawFillRectangle((uint)X, (uint)Y, (uint)(W - Bar), (uint)Bar, (uint)Color.Gray.ToArgb());
            // Hide Button
            vMWareSVGAII.DoubleBuffer_DrawFillRectangle((uint)(X + W - Bar), (uint)Y, (uint)Bar, (uint)Bar, (uint)Color.Brown.ToArgb());
            // Main Form
            vMWareSVGAII.DoubleBuffer_DrawFillRectangle((uint)X, (uint)(Y + Bar), (uint)W, (uint)(H - Bar), (uint)Color.Black.ToArgb());
        }
    }
}