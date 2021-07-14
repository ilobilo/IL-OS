using System;
using Cosmos.System;
using System.Drawing;
using GUI;
using System.Collections.Generic;

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

            string poweroff = "Power Off";
            vMWareSVGAII.DoubleBuffer_DrawFillRectangle(4, 4, (uint)(poweroff.Length * 7 + poweroff.Length - 1 + 6), 16, (uint)Color.FromArgb(0, 0, 40).ToArgb());
            vMWareSVGAII.DrawACSIIString(poweroff, (uint)Color.White.ToArgb(), 4 + (((uint)(poweroff.Length * 7 + poweroff.Length - 1 + 6) - ((uint)poweroff.Length * 7 + (uint)poweroff.Length - 1)) / 2), 4 + 1);
            if (IL_OS.Kernel.Pressed == true && MouseManager.X > 4 && MouseManager.X < (4 + (uint)(poweroff.Length * 7 + poweroff.Length - 1 + 6)) && MouseManager.Y > 4 && MouseManager.Y < (4 + 16))
            {
                Power.Shutdown();
            }

            vMWareSVGAII.DoubleBuffer_DrawLine((uint)Color.LightGray.ToArgb(), 84, 4, 84, 21);

            string term = "Terminal";
            vMWareSVGAII.DoubleBuffer_DrawFillRectangle(88, 4, (uint)(term.Length * 7 + term.Length - 1 + 6), 16, (uint)Color.FromArgb(0, 0, 40).ToArgb());
            vMWareSVGAII.DrawACSIIString(term, (uint)Color.White.ToArgb(), 88 + (((uint)(term.Length * 7 + term.Length - 1 + 6) - ((uint)term.Length * 7 + (uint)term.Length - 1)) / 2), 4 + 1);
            if (Kernel.Pressed == true && MouseManager.X > 88 && MouseManager.X < (88 + (uint)(term.Length * 7 + term.Length - 1 + 6)) && MouseManager.Y > 4 && MouseManager.Y < (4 + 16))
            {
                Kernel.windows[GetIndex("Terminal")].Open();
            }

            vMWareSVGAII.DoubleBuffer_DrawLine((uint)Color.LightGray.ToArgb(), 160, 4, 160, 21);

            string note = "Notepad";
            vMWareSVGAII.DoubleBuffer_DrawFillRectangle(165, 4, (uint)(note.Length * 7 + note.Length - 1 + 6), 16, (uint)Color.FromArgb(0, 0, 40).ToArgb());
            vMWareSVGAII.DrawACSIIString(note, (uint)Color.White.ToArgb(), 165 + (((uint)(note.Length * 7 + note.Length - 1 + 6) - ((uint)note.Length * 7 + (uint)note.Length - 1)) / 2), 4 + 1);
            if (Kernel.Pressed == true && MouseManager.X > 165 && MouseManager.X < (165 + (uint)(note.Length * 7 + note.Length - 1 + 6)) && MouseManager.Y > 4 && MouseManager.Y < (4 + 16))
            {
                Kernel.windows[GetIndex("Notepad")].Open();
            }
        }

        public static int GetIndex(string Title)
        {
            int i = 0;
            foreach (var window in Kernel.windows)
            {
                if (window.Title == Title)
                {
                    return i;
                }
                i++;
            }
            return -1;
        }
    }
}