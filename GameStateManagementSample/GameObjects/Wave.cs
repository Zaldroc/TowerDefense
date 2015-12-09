using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStateManagementSample.GameObjects
{
    class Wave
    {
        private Queue<Enemy> enemies;
        private int timeTillStart;
        private int intervall;
        private int elapsed;

        private int timeTillStartBeginning;

        public Wave(Queue<Enemy> enemies, int timeTillStart, int intervall)
        {
            this.enemies = enemies;
            this.timeTillStart = timeTillStart;
            this.intervall = intervall;
            elapsed = intervall;

            timeTillStartBeginning = timeTillStart;
        }
        

        public List<Enemy> getEnemies(GameTime gameTime)
        {
            List<Enemy> enemies = new List<Enemy>();
            timeTillStart -= gameTime.ElapsedGameTime.Milliseconds;
            if (timeTillStart<=0)
            {
                elapsed += gameTime.ElapsedGameTime.Milliseconds;
                if (elapsed>=intervall)
                {
                    elapsed = 0;
                    enemies.Add(this.enemies.Dequeue());
                }
            }
            return enemies;
        }

        public float GetRestTimeRatio()
        {
            return (float)timeTillStart / (float)timeTillStartBeginning;
        }

        public int Count()
        {
            return enemies.Count;
        }

        public void SetPath(List<Vector2> path)
        {
            foreach (Enemy e in enemies)
                e.SetPath(path);
        }
    }
}
