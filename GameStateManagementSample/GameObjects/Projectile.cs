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

        public Projectile(Vector2 position, Texture2D texture, float scale, int damage, double speed, double direction):base(position, texture, scale)
        {
            this.damage = damage;
            this.speed = speed;
            this.direction = direction;
        }

        public int GetDamage()
        {
            return damage;
        }

        public double GetSpeed()
        {
            return speed;
        }

        public void Move()
        {

        }
    }
}
