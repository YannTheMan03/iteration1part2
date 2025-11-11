using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using iteration1.Properties;

namespace iteration1
{
    /// <summary>
    /// Wave class to create each individual wave on the screen.
    /// </summary>
    /// 
    public enum Direction
    {
        Left = -1,
        Right = 1
    }
    public class Wave
    {
        // Protected variables

        protected int _enemyCount = 0;
        protected int _rowCount = 0;
        public List<Enemy> enemies { get; private set; }

        // Private variables
        private Direction _moveDirection = Direction.Right;
        private int _speed = 1;      
        private int _stepDown = 20; 
        private int _screenWidth = 400;

        private Random rnd = new Random();

        public List<Bullet> EnemyBullets {  get; private set; }
        public List<Bullet> DisposedEnemyBullets { get; set; }
        public Bullet enemyBulletInstance { get; set; }


        // Choosing the wave
        public Wave(int waveNumber)
        {
            switch (waveNumber)
            {
                case 1: _enemyCount = 4; _rowCount = 1; _speed = 1; break;
                case 2: _enemyCount = 8; _rowCount = 2; _speed = 2; break;
                case 3: _enemyCount = 12; _rowCount = 3; _speed = 2; break;
                case 4: _enemyCount = 16; _rowCount = 4; _speed = 3; break;
                case 5: _enemyCount = 20; _rowCount = 5; _speed = 3; break;
                default: _enemyCount = 4; _rowCount = 1; _speed = 1; break;
            }
            enemies = new List<Enemy>();
            EnemyBullets = new List<Bullet>();
            DisposedEnemyBullets = new List<Bullet>();
            SpawnEnemies(_enemyCount, _rowCount);


        }
        // Spawing the enemies
        public void SpawnEnemies(int enemyCount, int rowCount)
        {
            int spacingX = 10;
            int spacingY = 20;
            int screenWidth = 400;
            int enemyWidth = Properties.Resources.enemyPng.Width;
            int enemyHeight = Properties.Resources.enemyPng.Height;


            int enemiesPerRow = 6;
            int totalRowWidth = enemiesPerRow * enemyWidth + (enemiesPerRow - 1) * spacingX;
            int startX = (screenWidth - totalRowWidth) / 2;
            int startY = 50;

            int spawned = 0;

            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < enemiesPerRow && spawned < enemyCount; col++)
                {
                    int x = startX + col * (enemyWidth + spacingX);
                    int y = startY + row * (enemyHeight + spacingY);

                    Enemy enemy = new Enemy(Properties.Resources.enemyPng, x, y, 50, _speed);
                    enemies.Add(enemy);
                    spawned++;
                }
            }
        }

        // Updating the positions.
        public void Update()
        {
            bool hittingEdge = false;

            foreach (var enemy in enemies)
            {
                enemy.PositionX += (int)_moveDirection * _speed;
                if( enemy.PositionX <= 0 || enemy.PositionX + enemy.SpriteImage.Width >= _screenWidth)
                {
                    hittingEdge = true;
                }
            }
            if (hittingEdge)
            {
                _moveDirection = _moveDirection == Direction.Right ? Direction.Left : Direction.Right;

                foreach (var enemy in enemies)
                {
                    enemy.PositionY += _stepDown;
                }
            }
            enemies.RemoveAll(e => !e.IsAlive);  
            foreach(var bullet in EnemyBullets)
            {
                bullet.PositionY += 3;
            }
            EnemyBullets.RemoveAll(b => b.PositionY > 600);
            foreach (var enemy in enemies)
            {
                int shootChance = rnd.Next(0, 1000); // ~0.5% chance per update
                if (shootChance < 3)
                {
                    Bullet newBullet = new Bullet(Properties.Resources.bulletImage___Copy, enemy.PositionX , enemy.PositionY);
                    EnemyBullets.Add(newBullet);
                }
            }
        }

        // Drawing to the screen
        public void Draw(Graphics g)
        {
            foreach (var enemy in enemies)
            {
                enemy.Draw(g);      
            }  
            foreach (var bullet in EnemyBullets)
            {
                bullet.Draw(g);
            }
        }
    }      
}



