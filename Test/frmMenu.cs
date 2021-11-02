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
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
            frmMain.pnlMain.Invalidate();
            this.LostFocus += frmMenu_LosFocus;
        }

        private void frmMenu_LosFocus(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            var form = new frmGameArea();
            form.TopLevel = false;
            frmMain.pnlMain.Controls.Clear();
            frmMain.pnlMain.Controls.Add(form);
            form.Dock = DockStyle.Fill;
            form.Show();
            frmMain.pnlMain.Controls[0].Focus();
            this.Close();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            var form = new FormSettingns();
            form.ShowDialog();
        }

        private void btnBest_Click(object sender, EventArgs e)
        {
            var form = new frmScore();
            form.ShowDialog();
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {
            this.Focus();
        }
    }
}
