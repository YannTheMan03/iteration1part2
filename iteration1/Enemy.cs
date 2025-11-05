using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iteration1
{
    /// <summary>
    /// Enemy class, derived from entity class to created instances of enemies in my project.
    /// </summary>
    public class Enemy : Entity
    {
        private int EnemyHealth;
        private int _speedY = 1;

        public Enemy(Bitmap spriteImage, int positionX, int positionY, int enemyHealth) 
            : base(spriteImage, positionX, positionY)
        {
            EnemyHealth = enemyHealth;
        }
        // Work on this.
        public bool IsDead
        {
            get { return EnemyHealth <= 0; }
        }
        public void Update()
        {
            PositionY += _speedY;
            
        }
        public void TakeDamage(int bullet_Damage)
        {
            EnemyHealth -= bullet_Damage;
            if (EnemyHealth <= 0)
            {
                EnemyHealth = 0;
            }
        }

    }
}
