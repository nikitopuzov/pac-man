using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacManGame
{
    public partial class Form1 : Form
    {
        bool goUp = false;
        bool goDown = false;
        bool goLeft = false;
        bool goRight = false;
        bool isGameOver = false;

        int score = 0;
        int playerSpeed = 0;
        int redGhostSpeed = 0;
        int cyanGhostSpeed = 0;
        int pinkGhostSpeed = 0;
        int orangeGhostSpeed = 0;

        public Form1()
        {
            InitializeComponent();

            ResetGame();
        }

        private void ClickedKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                goUp = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = true;
            }
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
        }

        private void ClickedKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
        }

        private void MainGameTimer(object sender, EventArgs e)
        {
            textScore.Text = "Score: " + score;

            // Save Pac-Man's current position
            int newLeft = pacman.Left;
            int newTop = pacman.Top;

            // Calculate new position based on the direction of movement
            if (goLeft)
            {
                newLeft -= playerSpeed;
                pacman.Image = Properties.Resources.PacManLeft;
            }
            if (goRight)
            {
                newLeft += playerSpeed;
                pacman.Image = Properties.Resources.PacManRight;
            }
            if (goDown)
            {
                newTop += playerSpeed;
                pacman.Image = Properties.Resources.PacManDown;
            }
            if (goUp)
            {
                newTop -= playerSpeed;
                pacman.Image = Properties.Resources.PacManUp;
            }

            // Check for collision with walls
            bool isCollision = false;
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "wall")
                {
                    // Check if the new position would cause a collision with a wall
                    if (newLeft < x.Right && newLeft + pacman.Width > x.Left &&
                        newTop < x.Bottom && newTop + pacman.Height > x.Top)
                    {
                        isCollision = true;
                        break;
                    }
                }
            }

            // If there's no collision, update Pac-Man's position
            if (!isCollision)
            {
                pacman.Left = newLeft;
                pacman.Top = newTop;
            }

            // Teleportation feature
            if (pacman.Left < (-10))
            {
                pacman.Left = 680;
            }
            if (pacman.Left > 680)
            {
                pacman.Left = (-10);
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "coin" && x.Visible == true)
                    {
                        if (pacman.Bounds.IntersectsWith(x.Bounds))
                        {
                            score += 1;
                            x.Visible = false;
                        }
                    }

                    if ((string)x.Tag == "ghost")
                    {
                        if (pacman.Bounds.IntersectsWith(x.Bounds))
                        {
                            GameOver("You lose!");
                        }
                    }
                }
            }

            // Moving ghosts
            redGhost.Left += redGhostSpeed;

            if (redGhost.Bounds.IntersectsWith(pictureBox1.Bounds) || redGhost.Bounds.IntersectsWith(pictureBox2.Bounds))
            {
                redGhostSpeed = (-redGhostSpeed);
            }

            cyanGhost.Left += cyanGhostSpeed;

            if (cyanGhost.Bounds.IntersectsWith(pictureBox3.Bounds) || cyanGhost.Bounds.IntersectsWith(pictureBox4.Bounds))
            {
                cyanGhostSpeed = (-cyanGhostSpeed);
            }

            orangeGhost.Top -= orangeGhostSpeed;

            if (orangeGhost.Bounds.IntersectsWith(pictureBox74.Bounds) || orangeGhost.Bounds.IntersectsWith(pictureBox66.Bounds))
            {
                orangeGhostSpeed = (-orangeGhostSpeed);
            }

            pinkGhost.Left += pinkGhostSpeed;

            if (pinkGhost.Bounds.IntersectsWith(pictureBox42.Bounds) || pinkGhost.Bounds.IntersectsWith(pictureBox39.Bounds))
            {
                pinkGhostSpeed = (-pinkGhostSpeed);
            }

            if (score == 190)
            {
                GameOver("You win!");
            }
        }

        private void ResetGame()
        {
            textScore.Text = "Score: 0";
            score = 0;

            playerSpeed = 5;
            redGhostSpeed = 5;
            cyanGhostSpeed = 5;
            pinkGhostSpeed = 5;
            orangeGhostSpeed = 5;

            isGameOver = false;

            // Location of the pacman
            pacman.Left = 15;
            pacman.Top = 265;

            // Locations of the ghosts
            redGhost.Left = 185;
            redGhost.Top = 50;

            cyanGhost.Left = 435;
            cyanGhost.Top = 480;

            pinkGhost.Left = 330;
            pinkGhost.Top = 310;

            orangeGhost.Left = 570;
            orangeGhost.Top = 240;

            foreach (Control x in this.Controls)
            {
                x.Visible = true;
            }

            gameTimer.Start();
        }

        private void GameOver(string message)
        {
            isGameOver = true;

            gameTimer.Stop();

            textScore.Text = $"{textScore.Text}\n{message}";
        }
    }
}