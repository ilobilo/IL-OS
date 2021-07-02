using System;
using Sys = Cosmos.System;
using System.Drawing;
using nifanfa.CosmosDrawString;
using Cosmos.System.Graphics;
using GUI;
using Controls;

namespace IL_OS
{
    public class Kernel : Sys.Kernel
    {
        // Filesystem
        Sys.FileSystem.CosmosVFS fs = new Sys.FileSystem.CosmosVFS();

        // Screen Resolution
        //public static uint screenWidth = 640;
        //public static uint screenHeight = 480;
        public static uint screenWidth = 1024;
        public static uint screenHeight = 768;
        
        public static DoubleBufferedVMWareSVGAII vMWareSVGAII;

        int[] cursor = new int[]
            {
                1,0,0,0,0,0,0,0,0,0,0,0,
                1,1,0,0,0,0,0,0,0,0,0,0,
                1,2,1,0,0,0,0,0,0,0,0,0,
                1,2,2,1,0,0,0,0,0,0,0,0,
                1,2,2,2,1,0,0,0,0,0,0,0,
                1,2,2,2,2,1,0,0,0,0,0,0,
                1,2,2,2,2,2,1,0,0,0,0,0,
                1,2,2,2,2,2,2,1,0,0,0,0,
                1,2,2,2,2,2,2,2,1,0,0,0,
                1,2,2,2,2,2,2,2,2,1,0,0,
                1,2,2,2,2,2,2,2,2,2,1,0,
                1,2,2,2,2,2,2,2,2,2,2,1,
                1,2,2,2,2,2,2,1,1,1,1,1,
                1,2,2,2,1,2,2,1,0,0,0,0,
                1,2,2,1,0,1,2,2,1,0,0,0,
                1,2,1,0,0,1,2,2,1,0,0,0,
                1,1,0,0,0,0,1,2,2,1,0,0,
                0,0,0,0,0,0,1,2,2,1,0,0,
                0,0,0,0,0,0,0,1,1,0,0,0
            };

        public static bool Pressed = false;
        public static bool Opened = false;

        protected override void BeforeRun()
        {
            // Start GUI
            vMWareSVGAII = new DoubleBufferedVMWareSVGAII();
            vMWareSVGAII.SetMode(screenWidth, screenHeight, 32);
            
            // Initialize Mouse
            Sys.MouseManager.ScreenWidth = screenWidth;
            Sys.MouseManager.ScreenHeight = screenHeight;

            Sys.MouseManager.X = screenWidth / 2;
            Sys.MouseManager.Y = screenHeight / 2;

            // Initialize Filesystem
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);

            //Console.Clear();
        }
        
        protected override void Run()
        {
            switch (Sys.MouseManager.MouseState)
            {
                case Sys.MouseState.Left:
                    Pressed = true;
                    break;
                case Sys.MouseState.None:
                    Pressed = false;
                    break;
            }

            vMWareSVGAII.DoubleBuffer_Clear((uint)Color.FromArgb(4, 89, 202).ToArgb());

            // Taskbar
            vMWareSVGAII.DoubleBuffer_DrawFillRectangle(0, 0, screenWidth, 23, (uint)Color.FromArgb(0, 0, 0, 40).ToArgb());
            vMWareSVGAII.DoubleBuffer_DrawLine((uint)Color.LightGray.ToArgb(), 0, (int)(24), (int)screenWidth, (int)(24));
            vMWareSVGAII.DoubleBuffer_DrawRectangle((uint)Color.LightGray.ToArgb(), 0, 0, (int)screenWidth, (int)screenHeight - 1);

            // Power Off Button
            Button  button = new Button(vMWareSVGAII, "Power Off", 4, 4, Color.White, Color.FromArgb(0, 0, 40), Color.DarkRed);
            button.OnClick += delegate (object s, EventArgs e)
            {
                Sys.Power.Shutdown();
            };
            button.DrawAndUpdate();
            
            // Draw Cursor
            DrawCursor(vMWareSVGAII, Sys.MouseManager.X, Sys.MouseManager.Y);

            // Display Everything From Above
            vMWareSVGAII.DoubleBuffer_Update();

            //Shell.Run();
        }

        public void DrawCursor(DoubleBufferedVMWareSVGAII driver, uint x, uint y)
        {
            for (uint h = 0; h < 19; h++)
            {
                for (uint w = 0; w < 12; w++)
                {
                    if (cursor[h * 12 + w] == 1)
                    {
                        driver.DoubleBuffer_SetPixel(w + x, h + y, (uint)Color.Black.ToArgb());
                    }
                    if (cursor[h * 12 + w] == 2)
                    {
                        driver.DoubleBuffer_SetPixel(w + x, h + y, (uint)Color.White.ToArgb());
                    }
                }
            }
        }
    }
}
