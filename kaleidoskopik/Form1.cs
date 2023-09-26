using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace kaleidoskopik
{
    public partial class Form1 : Form
    {
        Box[,] box;
        int w, h;

        public Form1()
        {
            InitializeComponent();
            init();
            timer.Enabled = true;
        }
        public void init()
        {
            Box.setSize(panel1.Width, panel1.Height);
            w = panel.Width / panel1.Width;
            h = panel.Height / panel1.Height;
            int sx, sy;
            box = new Box[w, h];
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                {
                    Panel cell = new Panel();
                    cell.Parent = panel;
                    cell.Location = new System.Drawing.Point(i*panel1.Width,j*panel1.Height);
                    cell.Size = new System.Drawing.Size(panel1.Width,panel1.Height);
                    sx = (i % 2 == 1) ? -1 : 1;
                    sy = (j % 2 == 1) ? -1 : 1;
                    box[i,j] = new Box(cell, sx, sy);
                }
        }

        public void DrawRoundRect(Graphics g, Pen p, float X, float Y, float width, float height, float radius)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddLine(X + radius, Y, X + width - (radius * 2), Y);
            gp.AddArc(X + width - (radius * 2), Y, radius * 2, radius * 2, 270, 90);
            gp.AddLine(X + width, Y + radius, X + width, Y + height - (radius * 2));
            gp.AddArc(X + width - (radius * 2), Y + height - (radius * 2), radius * 2, radius * 2, 0, 90);
            gp.AddLine(X + width - (radius * 2), Y + height, X + radius, Y + height);
            gp.AddArc(X, Y + height - (radius * 2), radius * 2, radius * 2, 90, 90);
            gp.AddLine(X, Y + height - (radius * 2), X, Y + radius);
            gp.AddArc(X, Y, radius * 2, radius * 2, 180, 90);
            gp.CloseFigure();
            g.DrawPath(p, gp);
            panel.Region = new System.Drawing.Region(gp);
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            Graphics v = e.Graphics;
            DrawRoundRect(v, Pens.White, e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1, 270);
            base.OnPaint(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer.Enabled = true;
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "Help.chm");
        }

        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Box.choiseFigure();
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                    box[i, j].drawFigure();
        }
    }
}
