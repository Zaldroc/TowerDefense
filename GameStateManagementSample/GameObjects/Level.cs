using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStateManagementSample.GameObjects
{
    class Level
    {
        public List<Enemy> enemies;
        public List<PathBlock> path;

        private int[,] grid;
        private int[,] map;

        public Level()
        {
            grid = new int[160,90];
            map = new int[16000, 9000];

            enemies = new List<Enemy>();
            path = new List<PathBlock>();
        }

        //public 
    }
}
