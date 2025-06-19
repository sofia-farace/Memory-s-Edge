using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Media;

namespace Memory_s_Edge
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pagesBackground();
            pageOne();
            background.Play();
        }

        Rectangle player = new Rectangle(10, 270, 40, 40);
        Rectangle pond = new Rectangle(550, 230, 40, 40);

        SoundPlayer background = new SoundPlayer(Properties.Resources.background);

        List<Rectangle> debris = new List<Rectangle>();
        List<Rectangle> bubbles = new List<Rectangle>();
        List<Rectangle> fireflies = new List<Rectangle>();
        List<Rectangle> treesrow1 = new List<Rectangle>();
        List<Rectangle> treesrow2 = new List<Rectangle>();
        List<Rectangle> treesrow3 = new List<Rectangle>();

        Random RandNum = new Random();

        string heroName;

        int page = 1;
        int score = 0;

        int playerSpeed = 6;
        int stickSpeed = 8;
        int bubbleSpeed = 4;


        int miniGameTime1 = 15;
        int miniGameTime2 = 10;
        int miniGameTime3 = 30;

        bool upArrowDown = false;
        bool downArrowDown = false;
        bool rightArrowDown = false;
        bool leftArrowDown = false;


        SolidBrush whiteBrush = new SolidBrush(Color.White);
        Pen whitePen = new Pen(Color.White, 3);

        Image perry = Properties.Resources.perry;
        Image stick = Properties.Resources.stick;
        Image bubble = Properties.Resources.bubble;
        Image firefly = Properties.Resources.firefly;
        Image tree = Properties.Resources.tree;
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {

                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {

                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (page == 3 || page == 4 || page == 6 || page == 7)
            {
                e.Graphics.DrawImage(perry, player);
            }

            if (page == 4)
            {
                for (int i = 0; i < debris.Count; i++)
                {
                    e.Graphics.DrawImage(stick, debris[i]);
                }
                for (int i = 0; i < bubbles.Count; i++)
                {
                    e.Graphics.DrawImage(bubble, bubbles[i]);
                }
            }

            if (page == 6)
            {
                int y = RandNum.Next(50, this.Height - 100);
                int x = RandNum.Next(50, this.Width - 100);


                Rectangle newFly = new Rectangle(x, y, 40, 50);
                fireflies.Add(newFly);

                y = RandNum.Next(50, this.Height - 100);
                x = RandNum.Next(50, this.Width - 100);


                Rectangle newFly2 = new Rectangle(x, y, 40, 50);
                fireflies.Add(newFly2);

                y = RandNum.Next(50, this.Height - 100);
                x = RandNum.Next(50, this.Width - 100);


                Rectangle newFly3 = new Rectangle(x, y, 40, 50);
                fireflies.Add(newFly3);

                for (int i = 0; i < 3; i++)
                {
                    e.Graphics.DrawImage(firefly, fireflies[i]);
                }

                int newx = 0;
                int newy = 0;
                for (int i = 0; i < 3; i++)
                {
                    if (fireflies[i].IntersectsWith(player))
                    {
                        score++;
                        newy = RandNum.Next(50, this.Height - 100);
                        newx = RandNum.Next(50, this.Width - 100);
                        fireflies.Remove(fireflies[i]);
                    }
                }
                e.Graphics.DrawImage(firefly, newx, newy, 40, 50);
            }

            if (page == 7)
            {
                for (int i = 0; i < treesrow1.Count; i++)
                {
                    e.Graphics.DrawImage(tree, treesrow1[i]);
                }

                for (int i = 0; i < treesrow2.Count; i++)
                {
                    e.Graphics.DrawImage(tree, treesrow2[i]);
                }

                for (int i = 0; i < treesrow3.Count; i++)
                {
                    e.Graphics.DrawImage(tree, treesrow3[i]);
                }
            }
        }

        private void gameEngine_Tick(object sender, EventArgs e)
        {
            movePLayer();
            pagesBackground();

            if (page == 3)
            {
                pageThree();
            }

            if (page == 4)
            {
                pageFour();

            }

            if (page == 6)
            {
                pageSix();
            }

            if (page == 7)
            {
                pageSeven();
            }

            if (page == 8)
            {
                pageEight();
            }

            if (page == 9)
            {
                pageNine();
            }

            Refresh();
        }

        private void pagesBackground()
        {
            switch (page)
            {
                case 1:
                    this.BackgroundImage = Properties.Resources.titleScreen;
                    break;

                case 3:
                    this.BackgroundImage = Properties.Resources.pond;
                    break;

                case 4:
                    this.BackgroundImage = Properties.Resources.dodge;
                    break;

                case 5:
                    this.BackgroundImage = Properties.Resources.tree;
                    break;

                case 6:
                    this.BackgroundImage = Properties.Resources.cave;
                    break;

                case 7:
                    this.BackgroundImage = Properties.Resources.field;
                    break;

                case 8:
                    this.BackgroundImage = Properties.Resources.loss;
                    break;

                case 9:
                    this.BackgroundImage = Properties.Resources.win;
                    break;
            }

        }

        private void resetCoordinates()
        {
            player.X = 10;
            player.Y = 230;
        }

        private void movePLayer()
        {
            if (upArrowDown == true && player.Y > 0)
            {
                player.Y -= playerSpeed;
            }

            if (downArrowDown == true && player.Y < 400 - player.Height)
            {
                player.Y += playerSpeed;
            }

            if (leftArrowDown == true && player.X > 0)
            {
                player.X -= playerSpeed;
            }

            if (rightArrowDown == true && player.X < 700 - player.Width)
            {
                player.X += playerSpeed;
            }
        }

        private void pageFour()
        {

            timerLabel.Visible = true;
            miniGameTimer.Enabled = true;
            scoreLabel.Visible = true;
            scoreLabel.Text = "";
            textLabel.Text = "Oops... don't drown!!";

            int random = RandNum.Next(1, 101);
            if (random < 20)
            {
                int y = RandNum.Next(50, this.Height - 100);
                Rectangle newStick = new Rectangle(this.Width, y, 40, 50);
                debris.Add(newStick);
            }

            for (int i = 0; i < debris.Count; i++)
            {
                int x = debris[i].X - stickSpeed;
                debris[i] = new Rectangle(x, debris[i].Y, debris[i].Width, debris[i].Height);
            }

            for (int i = 0; i < debris.Count; i++)
            {
                if (debris[i].X <= 0)
                {
                    debris.Remove(debris[i]);
                }
            }

            int random2 = RandNum.Next(1, 101);


            if (random2 < 5)
            {
                int y = RandNum.Next(50, this.Height - 100);
                Rectangle newBubble = new Rectangle(this.Width, y, 50, 50);
                bubbles.Add(newBubble);
            }

            for (int i = 0; i < bubbles.Count; i++)
            {
                int x = bubbles[i].X - bubbleSpeed;
                bubbles[i] = new Rectangle(x, bubbles[i].Y, bubbles[i].Width, bubbles[i].Height);
            }

            for (int i = 0; i < bubbles.Count; i++)
            {
                if (bubbles[i].X <= 0)
                {
                    bubbles.Remove(bubbles[i]);
                }
            }

            timerLabel.Text = $"00:{miniGameTime1}";

            if (player.X >= this.Width - player.Width)
            {
                miniGameTimer.Enabled = false;
                timerLabel.Visible = false;
                resetCoordinates();
                miniGameTime2 = 10;
                score = 0;

                page = 6;
            }

            if (miniGameTime1 <= 0)
            {
                miniGameTimer.Enabled = false;
                timerLabel.Visible = false;

                page = 8;
            }

            for (int i = 0; i < bubbles.Count; i++)
            {
                if (bubbles[i].IntersectsWith(player))
                {
                    miniGameTime1 += 2;
                    bubbles.Remove(bubbles[i]);
                }
            }

            for (int i = 0; i < debris.Count; i++)
            {
                if (debris[i].IntersectsWith(player))
                {
                    miniGameTime1 -= 2;
                    debris.Remove(debris[i]);
                }
            }
        }

        private void pageSix()
        {
            scoreLabel.Visible = true;
            scoreLabel.Text = $"{score}/6";
            miniGameTimer.Start();
            timerLabel.Visible = true;
            timerLabel.Text = $"00:{miniGameTime2}";
            textLabel.Text = "Catch the fireflies to get out of the cave!";

            if (score >= 6)
            {
                resetCoordinates();
                miniGameTime3 = 30;
                page = 7;
                miniGameTimer.Enabled = false;
                timerLabel.Visible = false;
                scoreLabel.Visible = false;
            }

            if (miniGameTime2 == 0)
            {
                miniGameTimer.Enabled = false;
                timerLabel.Visible = false;
                scoreLabel.Visible = false;

                page = 8;
            }
        }

        private void pageSeven()
        {
            timerLabel.Text = $"00:{miniGameTime3}";
            timerLabel.Visible = true;
            miniGameTimer.Enabled = true;
            textLabel.Text = "Looks like the forest doesn't want you to go home...";
            scoreLabel.Visible = true;
            scoreLabel.Text = "";

            int random = RandNum.Next(1, 101);
            if (random < 5)
            {
                Rectangle newTree = new Rectangle(100, this.Height, 100, 150);
                treesrow1.Add(newTree);
            }

            for (int i = 0; i < treesrow1.Count; i++)
            {
                int y = treesrow1[i].Y - 20;
                treesrow1[i] = new Rectangle(100, y, treesrow1[i].Width, treesrow1[i].Height);
            }

            for (int i = 0; i < treesrow1.Count; i++)
            {
                if (treesrow1[i].Y == 0)
                {
                    treesrow1.Remove(treesrow1[i]);
                }
            }

            int random2 = RandNum.Next(1, 101);
            if (random2 < 5)
            {
                Rectangle newTree = new Rectangle(300, 0, 100, 150);
                treesrow2.Add(newTree);
            }

            for (int i = 0; i < treesrow2.Count; i++)
            {
                int y = treesrow2[i].Y + 20;
                treesrow2[i] = new Rectangle(300, y, treesrow2[i].Width, treesrow2[i].Height);
            }

            for (int i = 0; i < treesrow2.Count; i++)
            {
                if (treesrow2[i].Y == this.Height - treesrow2[i].Height)
                {
                    treesrow2.Remove(treesrow2[i]);
                }
            }

            int random3 = RandNum.Next(1, 101);
            if (random3 < 5)
            {
                Rectangle newTree = new Rectangle(500, this.Height, 100, 150);
                treesrow3.Add(newTree);
            }

            for (int i = 0; i < treesrow3.Count; i++)
            {
                int y = treesrow3[i].Y - 20;
                treesrow3[i] = new Rectangle(500, y, treesrow3[i].Width, treesrow3[i].Height);
            }

            for (int i = 0; i < treesrow3.Count; i++)
            {
                if (treesrow3[i].Y == 0)
                {
                    treesrow3.Remove(treesrow3[i]);
                }
            }

            for (int i = 0; i < treesrow1.Count; i++)
            {
                if (treesrow1[i].IntersectsWith(player))
                {
                    resetCoordinates();
                }
            }

            for (int i = 0; i < treesrow2.Count; i++)
            {
                if (treesrow2[i].IntersectsWith(player))
                {
                    resetCoordinates();
                }
            }

            for (int i = 0; i < treesrow3.Count; i++)
            {
                if (treesrow3[i].IntersectsWith(player))
                {
                    resetCoordinates();
                }
            }

            if (player.X >= this.Width - player.Width)
            {
                timerLabel.Visible = false;
                page = 9;
            }

            if (miniGameTime3 <= 0)
            {
                timerLabel.Visible = false;
                miniGameTimer.Stop();
                page = 8;
            }
        }

        private void miniGameTimer_Tick(object sender, EventArgs e)
        {
            if (page == 4)
            {
                miniGameTime1--;
            }

            if (page == 6)
            {
                miniGameTime2--;
            }

            if (page == 7)
            {
                miniGameTime3--;
            }
        }

        private void pageEight()
        {
            playerSpeed = 0;
            exitButton.Visible = true;
            exitButton.Enabled = true;
            repeatButton.Visible = true;
            repeatButton.Enabled = true;
            textLabel.Visible = false;
            scoreLabel.Visible = false;
            timerLabel.Visible = false;
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Thread.Sleep(3000);
            this.Close();
        }

        private void repeatButton_Click(object sender, EventArgs e)
        {
            resetCoordinates();
            gameEngine.Start();
            exitButton.Visible = false;
            exitButton.Enabled = false;
            repeatButton.Visible = false;
            repeatButton.Enabled = false;
            textLabel.Visible = false;
            page = 3;
            playerSpeed = 6;
            this.Focus();
        }

        private void pageThree()
        {

            if (player.IntersectsWith(pond))
            {
                resetCoordinates();
                miniGameTime1 = 15;
                page = 4;
            }

            heroName = nameInput.Text;
            textLabel.Text = $"{heroName}...you've been walking for as long as you can remember... make your way to the pond to get a drink.";
            textLabel.Visible = true;
            timerLabel.Visible = true;
            timerLabel.Text = "";
            scoreLabel.Visible = true;
            scoreLabel.Text = "";
            nameLabel.Visible = false;
            nameInput.Visible = false;
            nameInput.Enabled = false;
        }

        private void pageNine()
        {
            playerSpeed = 0;
            exitButton.Visible = true;
            exitButton.Enabled = true;
            repeatButton.Visible = true;
            repeatButton.Enabled = true;
            textLabel.Visible = false;
            timerLabel.Visible = false;
            scoreLabel.Visible = false;
        }

        private void pageOne()
        {
            startButton.Visible = true;
            startButton.Enabled = true;
            gameEngine.Enabled = false;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            startButton.Enabled = false;
            startButton.Visible = false;
            this.Focus();
            gameEngine.Start();
            page = 3;
        }
    }
}
