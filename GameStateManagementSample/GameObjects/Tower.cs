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

        protected Tower(Vector2 p, Texture2D t, float r, int c, double sI):base(p,t)
        {
            range = r;
            costs = c;
            shootingInterval = sI;
        }

        protected float GetRange()
        {
            return range;
        }
    }
}
