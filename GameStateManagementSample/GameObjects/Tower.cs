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
        public double distance=0;

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
                double direction=0;
                foreach (Enemy e in enemies)
                {
                    distance = Math.Sqrt(Math.Pow(GetPosition().X-e.GetPosition().X, 2) + Math.Pow(GetPosition().Y - e.GetPosition().Y, 2));
                    if (range >= distance)
                    {
                        target = e;
                        direction = Math.Acos(Vector2.Dot(e.GetPosition(), GetPosition())/(e.GetPosition().Length()*GetPosition().Length())) / Math.PI * 180;
                        break;
                    }
                }
                
                if (target!=null)
                {
                    elapsedTime = 0;
                    projectileType.SetPosition(projectileType.GetPosition());
                    return new Projectile(projectileType, ref target, direction);
                }
            }
            return null;            
        }
    }
}
