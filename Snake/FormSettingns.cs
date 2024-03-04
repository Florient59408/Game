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
    public partial class FormSettingns : Form
    {
        public FormSettingns()
        {
            InitializeComponent();
            this.comboBox1.SelectedItem = "Medium";
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Settings.Speed = Settings.Levels[this.comboBox1.SelectedIndex];
            Settings.Height = 16;
            Settings.Width = 16;
            Settings.Name = comboBox1.SelectedItem.ToString();
            Console.WriteLine(Settings.Speed);
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Settings.Speed = Settings.Levels[2];
            Settings.Height = 16;
            Settings.Width = 16;
            Settings.Name = comboBox1.SelectedItem.ToString();
            this.Close();
        }
    }
}
