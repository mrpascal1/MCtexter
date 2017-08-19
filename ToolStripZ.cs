using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
namespace AdvancedNotepad_CSharp
{
    public class ToolStripZ:ToolStrip
    {
        public ToolStripZ()
        {
            this.Renderer = new LightToolStripRenderer();
        }
    }
    public class LightToolStripRenderer : ToolStripProfessionalRenderer
    {
        // Render button selected and pressed state
        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderButtonBackground(e);
            var rectBorder = new Rectangle(0, 0, e.Item.Width - 1, e.Item.Height - 1);
            var rect = new Rectangle(1, 1, e.Item.Width - 2, e.Item.Height - 2);
            Brush b2 = new System.Drawing.Drawing2D.LinearGradientBrush(e.Item.ContentRectangle, Color.FromArgb(241, 248, 251), Color.FromArgb(120,255,200), 90);

            if (e.Item.Selected == true || (e.Item as ToolStripButton).Checked)
            {
                e.Graphics.FillRectangle(b2, rect);
                e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.FromArgb(150, 150, 210))), rectBorder);
                e.Item.ForeColor = Color.Black;
            }

            if (e.Item.Pressed)
            {
                using (var b = new LinearGradientBrush(rect, Color.FromArgb(231, 238, 240), Color.FromArgb(126, 180, 210), 90))
                {
                    using (var b3 = new SolidBrush(Color.OrangeRed))
                    {
                        e.Graphics.FillRectangle(b3, rectBorder);
                        e.Graphics.FillRectangle(b, rect);
                    }
                }
            }
        }
    }
}
