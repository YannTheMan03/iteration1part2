using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iteration1
{
    /// <summary>
    /// Entity class to allow instances to be created with hitboxes and positions
    /// </summary>
    public class Entity
    {
        // Variables
        public Bitmap SpriteImage { get; }
        public Rectangle HitBox => new Rectangle(PositionX, PositionY, SpriteImage.Width, SpriteImage.Height);
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        // Constructor
        public Entity(Bitmap spriteImage, int positionX, int positionY)
        {
            SpriteImage = spriteImage;
            PositionX = positionX;
            PositionY = positionY;
        }

    }
}
