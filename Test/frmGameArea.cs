using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Test
{
    public partial class frmGameArea : Form
    {
        private bool gameStarted = false;
        private bool gamePaused = false;
        private bool gameOver = false;
        private Snake snake;
        public frmGameArea()
        {
            InitializeComponent();
            StartGame();
        }

        private void tmrEngine_Tick(object sender, EventArgs e)
        {
            if (gamePaused && gameStarted)
            {
                SaveBest();
                if (Input.KeyPress(Keys.Back))
                    StartGame();
                if (Input.KeyPress(Keys.Escape))
                    Exit();
            }
            if (gameOver)
            {
                gameStarted = false;
                SaveBest();
                if (Input.KeyPress(Keys.Enter) || Input.KeyPress(Keys.Back))
                    StartGame();
                if (Input.KeyPress(Keys.Escape))
                    Exit();
            }
            else
            {
                if (Input.KeyPress(Keys.NumPad8) && snake.way != Directions.Down)
                    snake.way = Directions.Up;
                else if (Input.KeyPress(Keys.NumPad5) && snake.way != Directions.Up)
                    snake.way = Directions.Down;
                else if (Input.KeyPress(Keys.NumPad4) && snake.way != Directions.Right)
                    snake.way = Directions.Left;
                else if (Input.KeyPress(Keys.NumPad6) && snake.way != Directions.Left)
                    snake.way = Directions.Right;
                else if (Input.KeyPress(Keys.Escape))
                    Exit();
                if (Input.KeyPress(Keys.Back))
                    StartGame();
                if (!snake.MoveSnake(this.pnlArea.Size))
                    gameOver = true;
            }
            pnlArea.Invalidate();
        }
        private void StartGame()
        {
            pnlArea.Focus();
            pnlArea.LostFocus += PnlArea_LostFocus;
            lblLvel.Text = Settings.Name;
            snake = new Snake(new Circle(10,0), this.pnlArea.Size);
            tmrEngine.Interval = 1000 / Settings.Speed;
            Console.WriteLine(Settings.Speed);
            lblGameOver.Visible = false;
            gameOver = false;
            if (gameStarted)
            {
                tmrEngine.Start();
            }
        }

        private void PnlArea_LostFocus(object sender, EventArgs e)
        {
            pnlArea.Focus();
        }

        private void Pause()
        {
            if(!gamePaused)
            {
                SaveBest();
                this.tmrEngine.Stop();
                gamePaused = true;
                btnPause.Image = Image.FromFile(Path.Combine(Application.StartupPath, "../../Resources/play_48px.png"));
                lblGameOver.Text = "Pause\n" + "    " + lblScore.Text;
                if(gameStarted)
                    lblGameOver.Visible = true;
            }
            else if(gamePaused)
            {
                lblGameOver.Visible = false;
                gamePaused = false;
                btnPause.Image = Image.FromFile(Path.Combine(Application.StartupPath, "../../Resources/pause_48px.png"));
                this.tmrEngine.Start();
            }
        }
        private void Exit()
        {
            SaveBest();
            var form = new frmMenu();
            form.TopLevel = false;
            frmMain.pnlMain.Controls.Clear();
            frmMain.pnlMain.Controls.Add(form);
            form.Dock = DockStyle.Fill;
            frmMain.pnlMain.Controls[0].Focus();
            form.Show();
            Input.ChangeState(Keys.Escape, false);
            this.Close();
        }
        private void frmGameArea_KeyPress(object sender, KeyPressEventArgs e)
        {

            if ((Keys)e.KeyChar == Keys.Space && gameStarted)
                Pause();
            else
            {
                if (!gameStarted && (Keys)e.KeyChar != Keys.Escape)
                {
                    gameStarted = true;
                    StartGame();
                }
                else if((Keys)e.KeyChar == Keys.Escape)
                    Exit();
            }
        }

        private void frmGameArea_KeyDown(object sender, KeyEventArgs e)
        {   
            if(e.KeyCode != Keys.Space)
                Input.ChangeState(e.KeyCode, true);
            Console.WriteLine("Bonjour");
        }

        private void frmGameArea_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Space)
                Input.ChangeState(e.KeyCode, false);
            Console.WriteLine("Au revoir");
        }

        private void pbArea_Paint(object sender, PaintEventArgs e)
        {
            //Graphics canvas = e.Graphics;
            //if (gameOver == false)
            //{
            //    Brush snakeColor;
            //    for (int i = 0; i < snake.Body.Count; i++)
            //    {
            //        if (i == 0)
            //            snakeColor = Brushes.Red;
            //        else
            //            snakeColor = Brushes.White;
            //        canvas.FillEllipse(snakeColor, new Rectangle(snake.Body[i].X*Settings.Width, snake.Body[i].Y*Settings.Height, Settings.Width, Settings.Height));
            //    }
            //    canvas.FillEllipse(Brushes.Black, new Rectangle(snake.Food.X * Settings.Width, snake.Food.Y * Settings.Height, Settings.Width, Settings.Height));
            //    lblScore.Text = (snake.Lenght - 1).ToString();
            //}
            //else
            //{
            //    lblScore.Text = (snake.Lenght - 1).ToString();
            //    lblGameOver.Text = $"Game Over\n Score: {lblScore.Text}";
            //    lblGameOver.Visible = true;
            //    gameOver = true;
            //}
        }
        private void btnPause_Click(object sender, EventArgs e)
        {
            Pause();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void pnlArea_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;
            canvas.SmoothingMode = SmoothingMode.HighQuality;
            if (gameOver == false)
            {
                Brush snakeColor;
                for (int i = 0; i < snake.Body.Count; i++)
                {
                    if (i == 0)
                        snakeColor = Brushes.Red;
                    else
                        snakeColor = Brushes.White;
                    canvas.FillEllipse(snakeColor, new Rectangle(snake.Body[i].X * Settings.Width, snake.Body[i].Y * Settings.Height, Settings.Width, Settings.Height));
                }
                canvas.FillEllipse(Brushes.Black, new Rectangle(snake.Food.X * Settings.Width, snake.Food.Y * Settings.Height, Settings.Width, Settings.Height));
                lblScore.Text = (snake.Lenght - 1).ToString();
            }
            else
            {
                lblScore.Text = (snake.Lenght - 1).ToString();
                lblGameOver.Text = $"Game Over\n Score: {lblScore.Text}";
                lblGameOver.Visible = true;
                gameOver = true;
            }
        }
        private void SaveBest()
        {

            var path = Path.Combine(Application.StartupPath, "score.txt");
            if (File.Exists(path))
            {
                int oldSocre = 0;
                using (var rd = new StreamReader(path))
                {
                    oldSocre = int.Parse(rd.ReadLine());
                    rd.Close();
                    rd.Dispose();
                }

                if ( !string.IsNullOrEmpty(lblScore.Text) && oldSocre < int.Parse(lblScore.Text))
                {
                    using (var wt = new StreamWriter(path, false))
                    {
                        wt.WriteLine(lblScore.Text);
                        wt.Close();
                        wt.Dispose();
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(lblScore.Text))
                    File.WriteAllLines(path, new string[] { lblScore.Text });
                else
                    File.WriteAllLines(path, new string[] { "0" });
               
            }
        }

    }
}
