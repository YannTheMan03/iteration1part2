using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace iteration1
{
    /// <summary>
    /// Wave class to create each individual wave on the screen.
    /// </summary>
    public abstract class Wave
    {

        // Protected variables
        protected const int BoundaryLeftX = 0;
        protected const int BoundaryRightX = 350;
        protected const int Spacing = 50;
        protected int _enemyCount = 0;
        protected int _rowCount = 0;
        public List<Enemy> enemies = [];

        public void SpawnEnemies()
        {

        }

    }
    public class waveOne : Wave
    {
        waveOne()
        {

        }
    }





}


