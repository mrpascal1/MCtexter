using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace AdvancedNotepad_CSharp
{
    public partial class MyRichTextBox : UserControl
    {
        public MyRichTextBox()
        {
            InitializeComponent();
        }

        public int getWidth()
        {
            int w = 25;
            // get total lines of richTextBox1
            int line = richTextBox1.Lines.Length;

            if (line <= 99)
            {
                w = 20 + (int)richTextBox1.Font.Size;
            }
            else if (line <= 999)
            {
                w = 30 + (int)richTextBox1.Font.Size;
            }
            else
            {
                w = 50 + (int)richTextBox1.Font.Size;
            }

            return w;
        }

        public void AddLineNumbers()
        {
            richTextBox1.Select();
            
            Point pt = new Point(0, 0);
            
            int First_Index = richTextBox1.GetCharIndexFromPosition(pt);
            int First_Line = richTextBox1.GetLineFromCharIndex(First_Index);
           
            pt.X = ClientRectangle.Width;
            pt.Y = ClientRectangle.Height;
            
            int Last_Index = richTextBox1.GetCharIndexFromPosition(pt);
            int Last_Line = richTextBox1.GetLineFromCharIndex(Last_Index);
            
            LineNumberTextBox.SelectionAlignment = HorizontalAlignment.Center;
            
            LineNumberTextBox.Text = "";
            LineNumberTextBox.Width = getWidth();
            
            for (int i = First_Line; i <= Last_Line+1 ; i++)
            {
                LineNumberTextBox.Text += i + 1 + "\n";
            }
        }

        

        private void MyRichTextBox_Load(object sender, EventArgs e)
        {
            LineNumberTextBox.Font = richTextBox1.Font;
            AddLineNumbers();
        }

        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            this.Invalidate();
            Point pt = richTextBox1.GetPositionFromCharIndex(richTextBox1.SelectionStart);
            if (pt.X == 1)
            {
                AddLineNumbers();
            }
        }

        private void richTextBox1_VScroll(object sender, EventArgs e)
        {
            LineNumberTextBox.Text = "";
            AddLineNumbers();
            LineNumberTextBox.Invalidate();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
            {
                AddLineNumbers();
            }
            //HTML TAGS DECLARATION
            Regex tag1 = new Regex(@"<html>|<\/html>|<head>|<\/head>|<title>|</title>", RegexOptions.IgnoreCase | RegexOptions.ECMAScript);
            //Regex rx = new Regex(@"\b" + "<html>|</html>" + @"\b", RegexOptions.IgnoreCase);
            int index = richTextBox1.SelectionStart;
            foreach (Match m in tag1.Matches(richTextBox1.Text))
            {
                richTextBox1.Select(m.Index, m.Length);
                richTextBox1.SelectionColor = Color.Blue;
                richTextBox1.SelectionStart = index;
                richTextBox1.SelectionColor = Color.Black;
                richTextBox1.DeselectAll();
            }

            Regex tag2 = new Regex(@"<p>|<\/p>|<h1>|<\/h1>|<h2>|<\/h2>|<h3>|<\/h3>|<h4>|<\/h4>|<h5>|<\/h5>|<h6>|<\/h6>", RegexOptions.IgnoreCase | RegexOptions.ECMAScript);
            int index2 = richTextBox1.SelectionStart;
            foreach(Match m in tag2.Matches(richTextBox1.Text))
            {
                richTextBox1.Select(m.Index, m.Length);
                richTextBox1.SelectionColor = Color.YellowGreen;
                richTextBox1.SelectionStart = index;
                richTextBox1.SelectionColor = Color.Black;
                richTextBox1.DeselectAll();
            }
            Regex tag3 = new Regex(@"<strong>|<\/strong>|<bold>|<\/bold>|<i>|<\/i>|<emp>|<\/emp>|<sub>|<\/sub>|<sup>|<\/sup>|<marquee>|<\/marquee>", RegexOptions.IgnoreCase | RegexOptions.ECMAScript);
            int index3 = richTextBox1.SelectionStart;
            foreach (Match m in tag3.Matches(richTextBox1.Text))
            {
                richTextBox1.Select(m.Index, m.Length);
                richTextBox1.SelectionColor = Color.Magenta;
                richTextBox1.SelectionStart = index;
                richTextBox1.SelectionColor = Color.Black;
                richTextBox1.DeselectAll();
            }
            Regex br = new Regex(@"<br>", RegexOptions.IgnoreCase | RegexOptions.ECMAScript);
            int index4 = richTextBox1.SelectionStart;
            foreach (Match m in br.Matches(richTextBox1.Text))
            {
                richTextBox1.Select(m.Index, m.Length);
                richTextBox1.SelectionColor = Color.Gold;
                richTextBox1.SelectionStart = index;
                richTextBox1.SelectionColor = Color.Black;
                richTextBox1.DeselectAll();
            }

        }
        
        
        private void richTextBox1_FontChanged(object sender, EventArgs e)
        {
            LineNumberTextBox.Font = richTextBox1.Font;
            AddLineNumbers();
        }

        private void LineNumberTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            richTextBox1.Select();
            LineNumberTextBox.DeselectAll();
        }

        private void MyRichTextBox_Resize(object sender, EventArgs e)
        {
            AddLineNumbers();
        }
    }
}
