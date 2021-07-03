using System;
using Sys = Cosmos.System;
using System.Drawing;
using GUI;
using Controls;
using nifanfa.CosmosDrawString;

namespace IL_OS
{
    public static class GUI
    {
        public static void Taskbar(DoubleBufferedVMWareSVGAII vMWareSVGAII, uint screenWidth, uint screenHeight)
        {
            // Clear Screen
            vMWareSVGAII.DoubleBuffer_Clear((uint)Color.FromArgb(4, 89, 202).ToArgb());

            // Taskbar
            vMWareSVGAII.DoubleBuffer_DrawFillRectangle(0, 0, screenWidth, 24, (uint)Color.FromArgb(0, 0, 0, 40).ToArgb());
            vMWareSVGAII.DoubleBuffer_DrawLine((uint)Color.LightGray.ToArgb(), 0, (int)(24), (int)screenWidth, (int)(24));
            vMWareSVGAII.DoubleBuffer_DrawRectangle((uint)Color.LightGray.ToArgb(), 0, 0, (int)screenWidth, (int)screenHeight - 1);
            vMWareSVGAII.DoubleBuffer_DrawLine((uint)Color.LightGray.ToArgb(), 85, 3, 85, 22);

            // Power Off Button
            Button button = new Button(vMWareSVGAII, "Power Off", 4, 4, Color.White, Color.FromArgb(0, 0, 40), Color.DarkRed);
            button.OnClick += delegate (object s, EventArgs e)
            {
                Sys.Power.Shutdown();
            };
            button.DrawAndUpdate();

            Button terminal = new Button(vMWareSVGAII, "Terminal", 89, 4, Color.White, Color.FromArgb(0, 0, 40), Color.DarkRed);
            terminal.OnClick += delegate (object s, EventArgs e)
            {
                vMWareSVGAII._DrawACSIIString("Not implemented!", (uint)Color.Red.ToArgb(), (Kernel.screenWidth - 112) / 2, (Kernel.screenHeight - 10) / 2);
            };
            terminal.DrawAndUpdate();
        }
    }
}