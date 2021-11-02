using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Test
{
    public partial class frmScore : Form
    {
        public frmScore()
        {
            InitializeComponent();
        }

        private void FormSettingns_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine($"{e.KeyCode} is down");
        }

        private void FormSettingns_KeyUp(object sender, KeyEventArgs e)
        {
            Console.WriteLine($"{e.KeyCode} is up");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void frmScore_Load(object sender, EventArgs e)
        {
            var path = Path.Combine(Application.StartupPath, "score.txt");
            int oldSocre = 0;
            if (File.Exists(path))
            {
                using (var rd = new StreamReader(path))
                {
                    oldSocre = int.Parse(rd.ReadLine());
                    rd.Close();
                    rd.Dispose();
                }
            }
            label1.Text = $"BEST SCORE: {oldSocre}";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            File.Delete(Path.Combine(Application.StartupPath, "score.txt"));
            label1.Text = $"BEST SCORE: {0}";
        }
    }
}
