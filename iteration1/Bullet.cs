using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace iteration1
{
    //Bullet class
    public class Bullet : Entity
    {
        public int SpeedY { get; set; } = 5;
        public bool IsAlive { get; set; } = true;

        public Bullet(Bitmap pSpriteImage, int pPosition_X, int pPosition_Y)
            : base(pSpriteImage, pPosition_X, pPosition_Y)
        {
        }

        public void Update(int screenHeight)
        {
            PositionY += SpeedY;
            if (PositionY > screenHeight)
                IsAlive = false;
        }

        public void Draw(Graphics g) { g.DrawImage(SpriteImage, PositionX, PositionY); }


    }
}
