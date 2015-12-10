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
        private float speed;
        private Vector2 direction;


        public Projectile(Vector2 position, Texture2D texture, Vector2 scale, int damage, float speed, Vector2 direction):base(position, texture, scale)
        {
            this.damage = damage;
            this.speed = speed;
            this.direction = direction;
        }

        public Projectile(Projectile p, Vector2 position, Vector2 direction):base(position, p.GetTexture(), p.GetScale())
        {
            damage = p.GetDamage();
            speed = p.GetSpeed();
            this.direction = direction;
        }

        public int GetDamage()
        {
            return damage;
        }

        public float GetSpeed()
        {
            return speed;
        }

        public void SetSpeed(float speed)
        {
            this.speed = speed;
        }

        public void SetDamage(int damage)
        {
            this.damage = damage;
        }

        public Vector2 GetDirection()
        {
            return direction;
        }

        public void SetDirection(Vector2 direction)
        {
            this.direction = direction;
        }
        
        public void Move()
        {
            SetPosition(GetPosition() + this.direction * speed);

            double direction = Math.Atan(this.direction.Y / this.direction.X);
            direction = direction * (180 / Math.PI);

            direction = direction + 270;

            if (this.direction.X < 0)
                direction = direction + 180;

            direction += 180;

            this.SetRotationInDegrees((float)direction);
        }
    }
}
