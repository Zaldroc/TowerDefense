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

        private List<Vector2> path;

        public Enemy(Vector2 position, Texture2D texture, float scale, int health, float speed, int reward):base(position,texture,scale)
        {
            this.health = health;
            this.speed = speed;
            this.reward = reward;
        }

        public void SetPath(List<Vector2> path)
        {
            this.path = new List<Vector2>(path);
        }

        public void Damage(int damage)
        {
            health -= damage;
        }

        public int GetHealth()
        {
            return this.health;
        }

        public void SetSpeed(float speed)
        {
            this.speed = speed;
        }

        public float GetSpeed()
        {
            return this.speed;
        }

        public int GetReward()
        {
            return this.reward;
        }

        public void Move()
        {
            
        }
    }
}
