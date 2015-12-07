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
                Vector2 v = new Vector2(0,0);
                foreach (Enemy e in enemies)
                {
                    distance = Math.Sqrt(Math.Pow(GetPosition().X-e.GetPosition().X, 2) + Math.Pow(GetPosition().Y - e.GetPosition().Y, 2));
                    if (range >= distance)
                    {
                        target = e;
                        //direction = Math.Acos(Vector2.Dot(e.GetPosition(), GetPosition())/(e.GetPosition().Length()*GetPosition().Length())) / Math.PI * 180;
                        v = GetPosition() - e.GetPosition();
                        v.Normalize();
                        direction = Math.Atan(v.Y/v.X);
                        direction = direction * (180 / Math.PI);

                        direction = direction + 270;

                        if(v.X<0)
                            direction = direction + 180;

                        //if (v.X > 0)
                        //  direction = direction + 360;

                        //if (v.Y > 0)
                        //  direction = direction + 180;

                        this.SetRotationInDegrees((float)direction);
                        break;
                    }
                }

                if (target != null)
                {
                    elapsedTime = 0;
                    projectileType.SetPosition(projectileType.GetPosition());
                    //return new Projectile(projectileType, ref target, direction);
                    return new Projectile(projectileType, ref target, v*(-1));
                }
            }
            return null;            
        }

        public int GetCosts()
        {
            return costs;
        }
    }
}
