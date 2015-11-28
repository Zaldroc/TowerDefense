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
        private int elapsedTime;

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

        public Projectile Shoot(int millis)
        {
            elapsedTime += millis;
            if (elapsedTime>=shootingInterval)
            {
                elapsedTime = 0;
                projectileType.SetPosition(projectileType.GetPosition()-new Vector2(0, 1));
                return new Projectile(projectileType);
            }
            return null;            
        }
    }
}
