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
        private Enemy target;

        public Projectile(Vector2 position, Texture2D texture, float scale, int damage, double speed, double direction):base(position, texture, scale)
        {
            this.damage = damage;
            this.speed = speed;
            this.direction = direction;
        }

        public Projectile(Projectile p, ref Enemy target):base(p.GetPosition(), p.GetTexture(), p.GetScale())
        {
            damage = p.GetDamage();
            speed = p.GetSpeed();
            direction = p.GetDirection();
            this.target = target;
        }

        public int GetDamage()
        {
            return damage;
        }

        public double GetSpeed()
        {
            return speed;
        }

        public double GetDirection()
        {
            return direction;
        }

        public Enemy GetTarget()
        {
            return target;
        }

        public void Move()
        {

        }
    }
}
