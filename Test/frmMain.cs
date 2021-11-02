using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Test
{
    public partial class frmMain : Form
    {
        public static Panel pnlMain = new Panel();
        public frmMain()
        {
            InitializeComponent();
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            pnlMain.Size = new Size(561, 447);
            pnlMain.Dock = DockStyle.Bottom;
            this.Controls.Add(pnlMain);
            pnlMain.Focus();
            pnlMain.LostFocus += pnlMain_LostFocus;
        }

        private void pnlMain_LostFocus(object sender, EventArgs e)
        {
            pnlMain.Controls[0].Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wMsg, int wParam, int lParam);

        private void pnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();
            var form = new frmMenu();
            form.TopLevel = false;
            pnlMain.Controls.Add(form);
            form.Show();
            form.Dock = DockStyle.Fill;
            pnlMain.Controls[0].Focus();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
