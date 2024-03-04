using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class Splash : Form
    {
        private List<Point> loads = new List<Point>();
        private static int i = 0;
        public Splash()
        {
            InitializeComponent();
            var first = new Point(0, 0);
            loads.Add(first);
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (i < 55)
            {
                Point p;
                if (i % 2 == 0)
                    p = new Point(loads[i].X + 20, 0);
                else
                    p = new Point(loads[i].X + 3, 0);
                loads.Add(p);
                i++;
            }
            else
            {
                var form = new frmMain();
                form.Show();
                this.Close();
            }
            panel1.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics gph = e.Graphics;
            Brush loadPen;
            for(int i = 0; i<loads.Count; i++)
            {
                if(i%2==0)
                {
                    loadPen = new SolidBrush(Color.FromArgb(67, 183, 110));
                    gph.FillRectangle(loadPen, loads[i].X, loads[i].Y, 20, panel1.Size.Height);
                }

                else
                {
                    loadPen = Brushes.White;
                    gph.FillRectangle(loadPen, loads[i].X, loads[i].Y, 2, panel1.Size.Height);
                }
            }
        }
    }
}
