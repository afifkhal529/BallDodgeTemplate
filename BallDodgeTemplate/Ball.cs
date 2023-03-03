using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallDodgeTemplate
{
    internal class Ball
    {
        public int x, y, xSpeed, ySpeed;
        public int size = 10;

        public Ball(int _x, int _y, int _xSpeed, int _ySpeed)
        {
            x = _x;
            y = _y;
            xSpeed = _xSpeed;
            ySpeed = _ySpeed;
        }

        public void Move(int height, int width)
        {
            x += xSpeed;
            y += ySpeed;

            if (x > width - size || x < 0)
            {
                xSpeed *= -1;
            }

            if (y > height - size || y < 0)
            {
                ySpeed *= -1;
            }
        }
    }
}
