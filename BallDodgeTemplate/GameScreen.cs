using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BallDodgeTemplate
{
    public partial class GameScreen : UserControl
    {
        int score = 0;
        public static int lives = 5;
        public static int difficulty;

        Ball chaseBall = new Ball(10, 10, 10, 10);
        List<Ball> balls = new List<Ball>();

        Player hero;
        Boolean leftArrowDown, rightArrowDown, leftArrowUp, rightArrowUp, upArrowDown, downArrowDown;

        SolidBrush greenBrush = new SolidBrush(Color.Green);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush whiteBrush = new SolidBrush(Color.White);

        Random randGen = new Random();

        public GameScreen()
        {
            InitializeComponent();
            InitializeGame();
        }

        public void InitializeGame()
        {
            hero = new Player(50, 100);

            int x = randGen.Next(10, this.Width - 30);
            int y = randGen.Next(10, this.Height - 30);
            chaseBall = new Ball(x, y, 10, 10);
            
            for (int i = 0; i < difficulty; i++)
            {
                
                newBall();
            }
        }

        private void gameEngine_Tick(object sender, EventArgs e)
        {
            if (upArrowDown == true && hero.y > 0)
            {
                hero.Move("up");
            }

            if (downArrowDown && hero.y < this.Height - hero.height)
            {
                hero.Move("down");
            }

            if (rightArrowDown && hero.x > 0)
            {
                hero.Move("right");
            }

            if (leftArrowDown && hero.x < this.Width - hero.width)
            {
                hero.Move("left");
            }

            chaseBall.Move(this.Width, this.Height);

            foreach (Ball b in balls)
            {
                b.Move(this.Width, this.Height);
            }

            foreach (Ball b in balls)
            {
                chaseBall.Collision(b);
            }

            if (chaseBall.Collision(hero))
            {
                lives++;
            }

            foreach (Ball b in balls)
            {
                if (b.Collision(hero))
                {
                    newBall();
                    lives--;
                    break;
                }
            }

            if (lives == 0)
            {
                gameEngine.Stop();
            }

            //if (chaseBall.x > this.Width - chaseBall.size || chaseBall.x < 0)
            //{
            //    chaseBall.xSpeed *= -1;
            //}

            //if (chaseBall.y > this.Height - chaseBall.size || chaseBall.y < 0)
            //{
            //    chaseBall.ySpeed *= -1;
            //}

            Refresh();
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
            }
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(whiteBrush, hero.x, hero.y, hero.width, hero.height);
            e.Graphics.FillEllipse(greenBrush, chaseBall.x, chaseBall.y, chaseBall.size, chaseBall.size);

            foreach (Ball b in balls)
            {
                e.Graphics.FillEllipse(redBrush, b.x, b.y, b.size, b.size);
            }
        }

        public void newBall()
        {
            int x = randGen.Next(10, this.Width - 30);
            int y = randGen.Next(10, this.Height - 30);
            Ball newBall = new Ball(x, y, 10, 10);
            balls.Add(newBall);
        }
    }
}
