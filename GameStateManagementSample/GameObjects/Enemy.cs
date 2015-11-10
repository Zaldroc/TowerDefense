using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStateManagementSample.GameObjects
{
    class Enemy:GameObject
    {
        private int health;
        private float speed;
        private int reward;

        public Enemy(Vector2 position, Texture2D texture, float scale, int health, float speed, int range):base(position,texture,scale)
        {
            this.health = health;
            this.speed = speed;
            this.reward = range;
        }

        protected void Damage(int damage)
        {
            health -= damage;
        }

        protected int GetHealth()
        {
            return this.health;
        }

        protected void SetSpeed(float speed)
        {
            this.speed = speed;
        }

        protected float GetSpeed()
        {
            return this.speed;
        }

        protected int GetReward()
        {
            return this.reward;
        }

        protected void Move()
        {

        }
    }
}
