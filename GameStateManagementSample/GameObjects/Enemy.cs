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

        public Enemy(Vector2 p, Texture2D t,float sc,int h, float s, int r):base(p,t,sc)
        {
            this.health = h;
            this.speed = s;
            this.reward = r;
        }

        protected void Damage(int d)
        {
            health -= d;
        }

        protected int GetHealth()
        {
            return health;
        }

        protected void SetSpeed(float s)
        {
            speed = s;
        }

        protected float GetSpeed()
        {
            return speed;
        }

        protected int GetReward()
        {
            return reward;
        }

        protected void Move()
        {

        }
    }
}
