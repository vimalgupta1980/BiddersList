using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace BiddersList
{
    public enum RectangleCorners
    {
        None = 0, 
        TopLeft = 1, 
        TopRight = 2, 
        BottomLeft = 4, 
        BottomRight = 8,
        All = TopLeft | TopRight | BottomLeft | BottomRight
    }

    public class RoundedPanel : Panel
    {

        private const int BORDER_SIZE = 2;

        public int Radius { get; set; }
        public Color BorderColor { get; set; }
        public Color FillColor { get; set; }
        public bool Fill { get; set; }
        public bool AntiAlias { get; set; }
        public int BorderWidth { get; set; }

        public RoundedPanel()
            : base()
        {
            BackColor = Color.White;
            FillColor = Color.Transparent;
            Fill = false;
            AntiAlias = true;
            DoubleBuffered = true;
            Radius = 10;
            BorderWidth = 1;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            GraphicsPath graphicpath = RoundedRectangle.Create(0, 0, Width - 1, Height - 1, Radius, RectangleCorners.All);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.DrawPath(new Pen(BorderColor, (float)BorderWidth), graphicpath);
            if (Fill)
            {
                e.Graphics.FillPath(new SolidBrush(FillColor), graphicpath);
            }
            graphicpath.CloseFigure();
            this.Region = new Region(graphicpath);

            ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                                    Color.Black, BorderWidth, ButtonBorderStyle.Solid,
                                    Color.Black, BorderWidth, ButtonBorderStyle.Solid,
                                    Color.Black, BorderWidth, ButtonBorderStyle.Solid,
                                    Color.Black, BorderWidth, ButtonBorderStyle.Solid
                                    );
        }

        protected void OnPaint11(PaintEventArgs e)
        {

            GraphicsPath graphicpath = new GraphicsPath();

            graphicpath.StartFigure();

            graphicpath.AddArc(0, 0, Radius, Radius, 180, 90);
            graphicpath.AddLine(Radius, 0, this.Width - Radius, 0);
            graphicpath.AddArc(this.Width - Radius, 0, Radius, Radius, 270, 90);
            graphicpath.AddLine(this.Width, 25, this.Width, this.Height - Radius);
            graphicpath.AddArc(this.Width - Radius, this.Height - Radius, Radius, Radius, 0, 90);
            graphicpath.AddLine(this.Width - Radius, this.Height, 25, this.Height);
            graphicpath.AddArc(0, this.Height - Radius, Radius, Radius, 90, 90);

            graphicpath.CloseFigure();

            this.Region = new Region(graphicpath);

            ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                                    Color.Black, BORDER_SIZE, ButtonBorderStyle.Inset,
                                    Color.Black, BORDER_SIZE, ButtonBorderStyle.Inset,
                                    Color.Black, BORDER_SIZE, ButtonBorderStyle.Inset,
                                    Color.Black, BORDER_SIZE, ButtonBorderStyle.Inset
                                    );
        }
    }

     public abstract class RoundedRectangle
     {
         public static GraphicsPath Create(int x, int y, int width, int height,
                                           int radius, RectangleCorners corners)
         {
             int xw = x + width;
             int yh = y + height;
             int xwr = xw - radius;
             int yhr = yh - radius;
             int xr = x + radius;
             int yr = y + radius;
             int r2 = radius * 2;
             int xwr2 = xw - r2;
             int yhr2 = yh - r2;

             GraphicsPath p = new GraphicsPath();
             p.StartFigure();

             //Top Left Corner
             if ((RectangleCorners.TopLeft & corners) == RectangleCorners.TopLeft)
             {
                 p.AddArc(x, y, r2, r2, 180, 90);
             }
             else
             {
                 p.AddLine(x, yr, x, y);
                 p.AddLine(x, y, xr, y);
             }

             //Top Edge
             p.AddLine(xr, y, xwr, y);

             //Top Right Corner
             if ((RectangleCorners.TopRight & corners) == RectangleCorners.TopRight)
             {
                 p.AddArc(xwr2, y, r2, r2, 270, 90);
             }
             else
             {
                 p.AddLine(xwr, y, xw, y);
                 p.AddLine(xw, y, xw, yr);
             }

             //Right Edge
             p.AddLine(xw, yr, xw, yhr);

             //Bottom Right Corner
             if ((RectangleCorners.BottomRight & corners) == RectangleCorners.BottomRight)
             {
                 p.AddArc(xwr2, yhr2, r2, r2, 0, 90);
             }
             else
             {
                 p.AddLine(xw, yhr, xw, yh);
                 p.AddLine(xw, yh, xwr, yh);
             }

             //Bottom Edge
             p.AddLine(xwr, yh, xr, yh);

             //Bottom Left Corner
             if ((RectangleCorners.BottomLeft & corners) == RectangleCorners.BottomLeft)
             {
                 p.AddArc(x, yhr2, r2, r2, 90, 90);
             }
             else
             {
                 p.AddLine(xr, yh, x, yh);
                 p.AddLine(x, yh, x, yhr);
             }

             //Left Edge
             p.AddLine(x, yhr, x, yr);

             p.CloseFigure();
             return p;
         }

         public static GraphicsPath Create(Rectangle rect, int radius, RectangleCorners c)
         { return Create(rect.X, rect.Y, rect.Width, rect.Height, radius, c); }

         public static GraphicsPath Create(int x, int y, int width, int height, int radius)
         { return Create(x, y, width, height, radius, RectangleCorners.All); }

         public static GraphicsPath Create(Rectangle rect, int radius)
         { return Create(rect.X, rect.Y, rect.Width, rect.Height, radius); }

         public static GraphicsPath Create(int x, int y, int width, int height)
         { return Create(x, y, width, height, 5); }

         public static GraphicsPath Create(Rectangle rect)
         { return Create(rect.X, rect.Y, rect.Width, rect.Height); }
     }
}
