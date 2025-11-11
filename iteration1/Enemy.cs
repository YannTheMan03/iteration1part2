using iteration1;
using iteration1.Properties;

public class Enemy : Entity
{
    public int Health { get; set; }
    public int Speed { get; set; }
    public bool IsAlive => Health > 0;

    public bool IsDead { get; set; }
    public int bulletVelocity = -5;

    public Enemy(Bitmap spriteImage, int x, int y, int health, int speed)
        : base(spriteImage, x, y) { 
    
        Health = health;
        Speed = speed;
    }

    public void Move() { PositionY += Speed; }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health < 0) Health = 0;
    }

    public void Draw(Graphics g) { g.DrawImage(SpriteImage, PositionX, PositionY); }

    public void Shoot(Graphics g, int x, int y)
    {
        g.DrawImage(Resources.bulletImage___Copy, x, y);
    }
}
