using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStateManagementSample.GameObjects
{
    class Projectile:GameObject
    {
        private int damage;
        private double speed;
        private double direction;

        public Projectile(Vector2 p, Texture2D t,float sc, int d, double s, double dir):base(p,t,sc)
        {
            damage = d;
            speed = s;
            direction = dir;
        }

        public int GetDamage()
        {
            return damage;
        }

        public double GetSpeed()
        {
            return speed;
        }
    }
}
