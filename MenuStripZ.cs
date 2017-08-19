using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
namespace AdvancedNotepad_CSharp
{
   public class MenuStripZ : MenuStrip
    {
       public MenuStripZ()
       {
           this.Renderer = new MyMenuRenderer();
       }
    }

   public class MyMenuRenderer : ToolStripRenderer
   {
       public static void DrawRoundedRectangle(Graphics g, int x, int y, int width, int height, int m_diameter, Color color)
       {
           using (Pen pen = new Pen(color))
           {
               //Dim g As Graphics
               var BaseRect = new RectangleF(x, y, width, height);
               var ArcRect = new RectangleF(BaseRect.Location, new SizeF(m_diameter, m_diameter));
               //top left Arc
               g.DrawArc(pen, ArcRect, 180, 90);
               g.DrawLine(pen, x + Convert.ToInt32(m_diameter / 2), y, x + width - Convert.ToInt32(m_diameter / 2), y);

               // top right arc
               ArcRect.X = BaseRect.Right - m_diameter;
               g.DrawArc(pen, ArcRect, 270, 90);
               g.DrawLine(pen, x + width, y + Convert.ToInt32(m_diameter / 2), x + width, y + height - Convert.ToInt32(m_diameter / 2));

               // bottom right arc
               ArcRect.Y = BaseRect.Bottom - m_diameter;
               g.DrawArc(pen, ArcRect, 0, 90);
               g.DrawLine(pen, x + Convert.ToInt32(m_diameter / 2), y + height, x + width - Convert.ToInt32(m_diameter / 2), y + height);

               // bottom left arc
               ArcRect.X = BaseRect.Left;
               g.DrawArc(pen, ArcRect, 90, 90);
               g.DrawLine(pen, x, y + Convert.ToInt32(m_diameter / 2), x, y + height - Convert.ToInt32(m_diameter / 2));
           }
       }
       protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
       {

           base.OnRenderMenuItemBackground(e);
           if (e.Item.Enabled)
           {
               if (e.Item.IsOnDropDown == false && e.Item.Selected)
               {
                   var rect = new Rectangle(2, 2, e.Item.Width - 5, e.Item.Height);
                   Brush b2 = new System.Drawing.Drawing2D.LinearGradientBrush(e.Item.ContentRectangle, Color.FromArgb(255, 255, 255, 255), Color.FromArgb(255, 120, 200, 255), 90);
                   e.Graphics.FillRectangle(b2, rect);
                   DrawRoundedRectangle(e.Graphics, rect.Left - 1, rect.Top - 1, rect.Width + 1, rect.Height - 3, 2, Color.FromArgb(80,150,200));
                   e.Item.ForeColor = Color.Black;
               }
               else if (e.Item.IsOnDropDown && e.Item.Selected)
               {
                   var rect = new Rectangle(3, 1, e.Item.Width - 4, e.Item.Height - 2);
                   Brush b2 = new System.Drawing.Drawing2D.LinearGradientBrush(e.Item.ContentRectangle, Color.FromArgb(255, 255, 255, 255), Color.FromArgb(255, 120,200,255), 90);
                   e.Graphics.FillRectangle(b2, rect);
                   DrawRoundedRectangle(e.Graphics, rect.Left - 1, rect.Top - 1, rect.Width, rect.Height + 1, 4, Color.FromArgb(80,150,200));
                   e.Item.ForeColor = Color.Black;
               }
               else
               {
                   e.Item.ForeColor = Color.Black;
               }
               if ((e.Item as ToolStripMenuItem).DropDown.Visible && e.Item.IsOnDropDown == false)
               {
                   var rect = new Rectangle(2, 1, e.Item.Width - 4, e.Item.Height - 2);
                   var rect2 = new Rectangle(2, 1, e.Item.Width - 4, e.Item.Height - 2);
                   Brush b2 = new System.Drawing.Drawing2D.LinearGradientBrush(e.Item.ContentRectangle, Color.FromArgb(252, 252, 252, 252), Color.FromArgb(255, 120,200,255), 90);
                   e.Graphics.FillRectangle(b2, rect);
                   DrawRoundedRectangle(e.Graphics, rect.Left - 1, rect.Top - 1, rect.Width, rect.Height + 1, 4, Color.FromArgb(136, 190, 230));
                   e.Item.ForeColor = Color.Black;
               }
           }
       }
       protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
       {
           base.OnRenderItemCheck(e);

           if (e.Item.Selected)
           {
               var rect = new Rectangle(3, 1, 20, 20);
               var rect2 = new Rectangle(4, 2, 18, 18);
               SolidBrush b = new SolidBrush(Color.Orange);
               SolidBrush b2 = new SolidBrush(Color.FromArgb(255, 220, 230, 230));

               e.Graphics.FillRectangle(b, rect);
               e.Graphics.FillRectangle(b2, rect2);
               e.Graphics.DrawImage(e.Image, new Point(5, 3));
           }
           else
           {
               var rect = new Rectangle(3, 1, 20, 20);
               var rect2 = new Rectangle(4, 2, 18, 18);
               SolidBrush b = new SolidBrush(Color.Blue);
               SolidBrush b2 = new SolidBrush(Color.FromArgb(255, 240, 250, 250));

               e.Graphics.FillRectangle(b, rect);
               e.Graphics.FillRectangle(b2, rect2);
               e.Graphics.DrawImage(e.Image, new Point(5, 3));
           }
       }

       protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
       {
           base.OnRenderSeparator(e);
           var DarkLine = new SolidBrush(Color.FromArgb(200, 200, 200));
           var WhiteLine = new SolidBrush(Color.FromArgb(200, 200, 200));
           var rect = new Rectangle(30, 3, e.Item.Width - 32, 1);
           e.Graphics.FillRectangle(DarkLine, rect);
           e.Graphics.FillRectangle(WhiteLine, rect);
       }

       protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
       {
           base.OnRenderImageMargin(e);

           var rect = new Rectangle(0, 0, e.ToolStrip.Width, e.ToolStrip.Height);
           e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 255, 255)), rect);

           var DarkLine = new SolidBrush(Color.FromArgb(255, 240, 250, 255));
           var rect2 = new Rectangle(1, 2, 24, e.AffectedBounds.Height);
           e.Graphics.FillRectangle(DarkLine, rect2);

           var rect3 = new Rectangle(0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1);
           e.Graphics.DrawRectangle(new Pen(Brushes.DarkGray), rect3);
       }
   }
}
