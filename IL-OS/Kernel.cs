using System;
using Sys = Cosmos.System;
using System.Drawing;
using nifanfa.CosmosDrawString;
using Cosmos.System.Graphics;

namespace IL_OS
{
    public class Kernel : Sys.Kernel
    {
        Sys.FileSystem.CosmosVFS fs = new Sys.FileSystem.CosmosVFS();

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
        public static bool Clicked = false;

        protected override void BeforeRun()
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

            vMWareSVGAII = new DoubleBufferedVMWareSVGAII();
            vMWareSVGAII.SetMode(screenWidth, screenHeight);
            
            Sys.MouseManager.ScreenWidth = screenWidth;
            Sys.MouseManager.ScreenHeight = screenHeight;

            Sys.MouseManager.X = screenWidth / 2;
            Sys.MouseManager.Y = screenHeight / 2;

            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            //Console.Clear();
        }

        protected override void Run()
        {
            vMWareSVGAII.DoubleBuffer_Clear((uint)Color.FromArgb(4, 89, 202).ToArgb());
            
            vMWareSVGAII.DoubleBuffer_DrawFillRectangle(0, 0, screenWidth, 24, (uint)Color.FromArgb(0, 0, 0, 40).ToArgb());
            vMWareSVGAII.DoubleBuffer_DrawLine((uint)Color.LightGray.ToArgb(), 0, (int)(24 + 1), (int)screenWidth, (int)(24 + 1));
            vMWareSVGAII.DoubleBuffer_DrawFillRectangle(4, 4, (uint)(24 - 2) * 2, 24 - 6, (uint)Color.White.ToArgb());
            vMWareSVGAII._DrawACSIIString("Start", (uint)Color.Black.ToArgb(), 4 + ((24 - 4) * 2 - 35) / 2, (24 - 2 - 10) / 2);
            vMWareSVGAII.DoubleBuffer_DrawRectangle((uint)Color.LightGray.ToArgb(), 0, 0, (int)screenWidth, (int)screenHeight - 1);

            DrawCursor(vMWareSVGAII, Sys.MouseManager.X, Sys.MouseManager.Y);
            vMWareSVGAII.DoubleBuffer_Update();

            CheckClick();

            //Shell.Run();
        }

        public void CheckClick()
        {
            if (Pressed == true)
            {
                onclick();
            }
            else
            {
                return;
            }
        }

        public void onclick()
        {
            if (Clicked == false)
            {
                if (Sys.MouseManager.X > 2 && Sys.MouseManager.X < 46)
                {
                    if (Sys.MouseManager.Y > 2 && Sys.MouseManager.Y < 24)
                    {
                        vMWareSVGAII.DoubleBuffer_DrawFillRectangle(1, 25, 150, 200, (uint)Color.White.ToArgb());
                        Clicked = true;
                    }
                } 
            }
            else
            {
                if (Sys.MouseManager.X > 2 && Sys.MouseManager.X < 46)
                {
                    if (Sys.MouseManager.Y > 2 && Sys.MouseManager.Y < 24)
                    {
                        vMWareSVGAII.DoubleBuffer_DrawFillRectangle(1, 25, 150, 200, (uint)Color.FromArgb(4, 89, 202).ToArgb());
                        Clicked = false;
                    }
                }
            }
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
