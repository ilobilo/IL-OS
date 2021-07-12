using Cosmos.System;
using System.Drawing;
using GUI;

namespace IL_OS
{
    public class Window
    {
        public int X, Y, W, H, OX, OY;

        public int Bar = 20;

        public string Title = "Window";

        public Color BarCol = Color.Gray;
        public Color BordCol = Color.White;
        public Color MainCol = Color.Black;
        public Color CloseCol = Color.Brown;

        public bool Opened;
        public bool Move = false;

        public void Update(DoubleBufferedVMWareSVGAII vMWareSVGAII)
        {
            if (Opened != true)
            {
                return;
            }

            if (Kernel.Pressed)
            {
                if (MouseManager.X > this.X && MouseManager.X < this.X + this.W - Bar && MouseManager.Y > this.Y && MouseManager.Y < this.Y + Bar)
                {
                    Move = true;

                    OX = (int)(MouseManager.X - this.X);
                    OY = (int)(MouseManager.Y - this.Y);

                    Kernel.Focused = GUI.GetIndex(this.Title);
                }
                else if (MouseManager.X > this.X + this.W - Bar && MouseManager.X < this.X + this.W && MouseManager.Y > this.Y && MouseManager.Y < this.Y + Bar)
                {
                    this.Opened = false;
                }
                if (MouseManager.X > this.X && MouseManager.X < this.X + this.W && MouseManager.Y > this.Y && MouseManager.Y < this.Y + this.H)
                {
                    Kernel.Focused = GUI.GetIndex(this.Title);
                }
            }
            else
            {
                Move = false;
            }

            if (Move)
            {
                this.X = (int)(MouseManager.X - OX);
                this.Y = (int)(MouseManager.Y - OY);
            }

            if (this.Y < 25)
            {
                this.Y = 25;
            }

            // Bar
            vMWareSVGAII.DoubleBuffer_DrawFillRectangle((uint)X, (uint)Y, (uint)(W - Bar), (uint)Bar, (uint)BarCol.ToArgb());
            // Tittle
            if (Kernel.Focused == GUI.GetIndex(this.Title))
            {
                vMWareSVGAII.DrawACSIIString("(Active) " + Title, (uint)Color.Blue.ToArgb(), (uint)(X + 3), (uint)(Y + 3)); 
            }
            else
            {
                vMWareSVGAII.DrawACSIIString(Title, (uint)Color.Blue.ToArgb(), (uint)(X + 3), (uint)(Y + 3));
            }
            // Close Button
            vMWareSVGAII.DoubleBuffer_DrawFillRectangle((uint)(X + W - Bar), (uint)Y, (uint)Bar, (uint)Bar, (uint)CloseCol.ToArgb());
            // Main Form
            vMWareSVGAII.DoubleBuffer_DrawFillRectangle((uint)X, (uint)(Y + Bar), (uint)W, (uint)(H - Bar), (uint)MainCol.ToArgb());
            // Border
            vMWareSVGAII.DoubleBuffer_DrawRectangle((uint)BordCol.ToArgb(), X - 1, Y - 1, W + 1, H + 1);

            if (Kernel.Focused == GUI.GetIndex(this.Title))
            {
                InputUpdate();
            }
            UIUpdate();
        }

        public virtual void UIUpdate()
        {
        }

        public virtual void InputUpdate()
        {
        }
    }
}