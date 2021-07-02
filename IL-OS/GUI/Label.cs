using System;
using GUI;
using System.Drawing;
using nifanfa.CosmosDrawString;
using Cosmos.System.Graphics;

namespace Controls
{
    public class Label
    {
        DoubleBufferedVMWareSVGAII driver;

        public static Color TextColor { get; set; } = Color.Black;
        public static Color BackColor { get; set; } = Color.White;
        public static Color BorderColor { get; set; } = BackColor;

        public uint X { get; set; }
        public uint Y { get; set; }

        public static string Text { get; set; } = "";

        public uint width = (uint)(Text.Length * 7 + Text.Length - 1 + 6);
        public uint height = 16;

        public Label(DoubleBufferedVMWareSVGAII vMWareSVGAII, string text, uint x, uint y)
        {
            driver = vMWareSVGAII;
            Text = text;
            X = x;
            Y = y;
        }

        public Label(DoubleBufferedVMWareSVGAII vMWareSVGAII, string text, uint x, uint y, Color txtcol, Color bckcol)
        {
            driver = vMWareSVGAII;
            Text = text;
            X = x;
            Y = y;
            TextColor = txtcol;
            BackColor = bckcol;
        }

        public Label(DoubleBufferedVMWareSVGAII vMWareSVGAII, string text, uint x, uint y, Color txtcol, Color bckcol, Color bordcol)
        {
            driver = vMWareSVGAII;
            Text = text;
            X = x;
            Y = y;
            TextColor = txtcol;
            BackColor = bckcol;
            BorderColor = bordcol;
        }

        public void Draw()
        {
            uint txtX = X + 3;
            uint txtY = Y + 1;

            driver.DoubleBuffer_DrawFillRectangle(X, Y, width, height, (uint)BackColor.ToArgb());
            driver.DoubleBuffer_DrawRectangle((uint)BorderColor.ToArgb(), (int)X, (int)Y, (int)width, (int)height);
            driver._DrawACSIIString(Text, (uint)TextColor.ToArgb(), txtX, txtY);
        }
    }
}