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

        public Level()
        {
            enemies = new List<Enemy>();
            path = new List<PathBlock>();
        }
    }
}
