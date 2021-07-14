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
                if (MouseManager.X > this.X && MouseManager.X < this.X + this.W - Bar && MouseManager.Y > this.Y && MouseManager.Y < this.Y + Bar && !Kernel.WindowsMoving)
                {
                    Move = true;
                    Kernel.WindowsMoving = true;

                    OX = (int)(MouseManager.X - this.X);
                    OY = (int)(MouseManager.Y - this.Y);

                    Focus();
                }
                else if (MouseManager.X > this.X + this.W - Bar && MouseManager.X < this.X + this.W && MouseManager.Y > this.Y && MouseManager.Y < this.Y + Bar && !Kernel.WindowsMoving)
                {
                    this.Opened = false;
                }
                if (MouseManager.X > this.X && MouseManager.X < this.X + this.W && MouseManager.Y > this.Y && MouseManager.Y < this.Y + this.H && !Kernel.WindowsMoving)
                {
                    Focus();
                }
            }
            else
            {
                Move = false;
                Kernel.WindowsMoving = false;
            }

            if (Move)
            {
                Move = ShouldMove(); 
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
            if (Move && MouseManager.Y < 25)
            {
                MouseManager.Y = 25;
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
            Update();
        }

        public virtual void Update()
        {
        }

        public virtual void InputUpdate()
        {
        }

        public void Open()
        {
            this.Opened = true;
            Kernel.Focused = GUI.GetIndex(this.Title);
        }

        public void Focus()
        {
            foreach (var w in Kernel.windows)
            {
                if (this.X < w.X + w.W && this.X + this.W > w.X && this.Y < w.Y + w.H && this.Y + this.H > w.Y)
                {
                    Rectangle Wrectangle = new Rectangle(w.X, w.Y, w.W, w.H);
                    Rectangle Trectangle = new Rectangle(this.X, this.Y, this.W, this.H);
                    if (MouseManager.X > Rectangle.Intersect(Wrectangle, Trectangle).X && MouseManager.X < Rectangle.Intersect(Wrectangle, Trectangle).X + Rectangle.Intersect(Wrectangle, Trectangle).Width && MouseManager.Y > Rectangle.Intersect(Wrectangle, Trectangle).Y && MouseManager.Y < Rectangle.Intersect(Wrectangle, Trectangle).Y + Rectangle.Intersect(Wrectangle, Trectangle).Height)
                    {
                        if (!w.Opened)
                        {
                            Kernel.Focused = GUI.GetIndex(this.Title);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        Kernel.Focused = GUI.GetIndex(this.Title);
                    }
                }
                else
                {
                    Kernel.Focused = GUI.GetIndex(this.Title);
                }
            }
        }

        public bool ShouldMove()
        {
            if (Kernel.Focused == GUI.GetIndex(this.Title))
            {
                return true;
            }
            foreach (var w in Kernel.windows)
            {
                Rectangle Wrectangle = new Rectangle(w.X, w.Y, w.W, w.H);
                Rectangle Trectangle = new Rectangle(this.X, this.Y, this.W, this.Bar);
                if (MouseManager.X > Rectangle.Intersect(Wrectangle, Trectangle).X && MouseManager.X < Rectangle.Intersect(Wrectangle, Trectangle).X + Rectangle.Intersect(Wrectangle, Trectangle).Width && MouseManager.Y > Rectangle.Intersect(Wrectangle, Trectangle).Y && MouseManager.Y < Rectangle.Intersect(Wrectangle, Trectangle).Y + Rectangle.Intersect(Wrectangle, Trectangle).Height)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
    }
}