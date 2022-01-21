using System;
using GUI;
using System.Drawing;
using Cosmos.System;
namespace IL_OS
{
    public class Button
    {
        private EventHandler OnClickEventHandler;
        public event EventHandler OnClick
        {
            add
            {
                OnClickEventHandler = value;
            }
            remove
            {
                OnClickEventHandler -= value;
            }
        }
        DoubleBufferedVMWareSVGAII driver;
        public Color TextColor { get; set; } = Color.Black;
        public Color BackColor { get; set; } = Color.White;
        public Color BorderColor { get; set; } = BackColor;
        public uint X { get; set; }
        public uint Y { get; set; }
        public string Text { get; set; } = "Button";
        public uint width = (uint)(Text.Length * 7 + Text.Length - 1 + 6);
        public uint height = 16;
        public Button(DoubleBufferedVMWareSVGAII vMWareSVGAII, string text, uint x, uint y)
        {
            driver = vMWareSVGAII;
            Text = text;
            X = x;
            Y = y;
        }
        public Button(DoubleBufferedVMWareSVGAII vMWareSVGAII, string text, uint x, uint y, Color txtcol, Color bckcol)
        {
            driver = vMWareSVGAII;
            Text = text;
            X = x;
            Y = y;
            TextColor = txtcol;
            BackColor = bckcol;
        }
        public Button(DoubleBufferedVMWareSVGAII vMWareSVGAII, string text, uint x, uint y, Color txtcol, Color bckcol, Color bordcol)
        {
            driver = vMWareSVGAII;
            Text = text;
            X = x;
            Y = y;
            TextColor = txtcol;
            BackColor = bckcol;
            BorderColor = bordcol;
        }
        public Button(DoubleBufferedVMWareSVGAII vMWareSVGAII, string text, uint x, uint y, uint w)
        {
            driver = vMWareSVGAII;
            Text = text;
            X = x;
            Y = y;
            if (w < width)
            {
                width = width;
            }
            else
            {
                width = w;
            }
        }
        public Button(DoubleBufferedVMWareSVGAII vMWareSVGAII, string text, uint x, uint y, uint w, Color txtcol, Color bckcol)
        {
            driver = vMWareSVGAII;
            Text = text;
            X = x;
            Y = y;
            TextColor = txtcol;
            BackColor = bckcol;
            if (w < width)
            {
                width = width;
            }
            else
            {
                width = w;
            }
        }
        public Button(DoubleBufferedVMWareSVGAII vMWareSVGAII, string text, uint x, uint y, uint w, Color txtcol, Color bckcol, Color bordcol)
        {
            driver = vMWareSVGAII;
            Text = text;
            X = x;
            Y = y;
            TextColor = txtcol;
            BackColor = bckcol;
            BorderColor = bordcol;
            if (w < width)
            {
                width = width;
            }
            else
            {
                width = w;
            }
        }

        public string _Text()
        {
            return Text;
        }
        public void Draw()
        {
            uint txtX = X + (((uint)width - ((uint)Text.Length * 7 + (uint)Text.Length - 1)) / 2);
            uint txtY = Y + 1;
            driver.DoubleBuffer_DrawFillRectangle(X, Y, width, height, (uint)BackColor.ToArgb());
            driver.DoubleBuffer_DrawRectangle((uint)BorderColor.ToArgb(), (int)X, (int)Y, (int)width, (int)height);
            driver.DrawACSIIString(_Text(), (uint)TextColor.ToArgb(), txtX, txtY);
        }
        public void Update()
        {
            if (IL_OS.Kernel.Pressed == true && MouseManager.X > this.X && MouseManager.X < (this.X + this.width) && MouseManager.Y > this.Y && MouseManager.Y < (this.Y + this.height))
            {
                if (OnClickEventHandler != null)
                {
                    OnClickEventHandler.Invoke(this, new EventArgs());
                }
            }
        }

        public void DrawAndUpdate()
        {
            Draw();
            Update();
        }
    }
}
