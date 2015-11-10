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
        private double shootingInterval;

        private Projectile projectyleType;

        protected Tower(Vector2 position, Texture2D texture, float scale, float range, int costs, double shootingInterval):base(position,texture,scale)
        {
            this.range = range;
            this.costs = costs;
            this.shootingInterval = shootingInterval;
        }

        protected float GetRange()
        {
            return range;
        }
    }
}
