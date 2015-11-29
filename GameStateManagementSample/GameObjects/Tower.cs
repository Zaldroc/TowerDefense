using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStateManagementSample.GameObjects
{
    class Tower:GameObject
    {
        private float range;
        private int costs;
        private int shootingInterval;
        private int elapsedTime=1;

        private Projectile projectileType;

        public Tower(Vector2 position, Texture2D texture, float scale, float range, int costs, int shootingInterval, Projectile projectileType):base(position,texture,scale)
        {
            this.range = range;
            this.costs = costs;
            this.shootingInterval = shootingInterval;
            this.projectileType = projectileType;
        }

        public float GetRange()
        {
            return range;
        }

        public Projectile Shoot(int millisElapsed, List<Enemy> enemies)
        {
            elapsedTime += millisElapsed;
            if (elapsedTime>=shootingInterval)
            {
                Enemy target = null;
                foreach (Enemy e in enemies)
                {
                    double distance = Math.Sqrt(Math.Pow(GetPosition().X-e.GetPosition().X, 2) + Math.Pow(GetPosition().Y - e.GetPosition().Y, 2));
                    if (range >= distance)
                    {
                        target = e;
                        break;
                    }
                }
                
                if (target!=null)
                {
                    elapsedTime = 0;
                    projectileType.SetPosition(projectileType.GetPosition() - new Vector2(0, 100));
                    return new Projectile(projectileType, ref target);
                }
            }
            return null;            
        }
    }
}
