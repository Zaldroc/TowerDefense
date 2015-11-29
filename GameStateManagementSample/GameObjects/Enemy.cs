﻿using Microsoft.Xna.Framework;
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

        private Queue<Vector2> path;
        private Vector2 nextPosition;

        public Enemy(Vector2 position, Texture2D texture, float scale, int health, float speed, int reward):base(position,texture,scale)
        {
            this.health = health;
            this.speed = speed;
            this.reward = reward;
        }

        public void SetPath(List<Vector2> path)
        {
            this.path = new Queue<Vector2>(path);
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
            if(nextPosition==null)
            {
                nextPosition = path.Dequeue();
            }

            Vector2 pos = GetPosition();
            Vector2 currentPos = new Vector2((int)pos.X / 100, (int)pos.Y / 100);

            if (nextPosition.Equals(currentPos))
            {
                try
                {
                    nextPosition = path.Dequeue();
                }
                catch (Exception e)
                {
                    //GAME OVER!
                    throw e;
                }
            }

            Vector2 north = new Vector2(0, -1) + currentPos;
            Vector2 south = new Vector2(0, 1) + currentPos;
            Vector2 east = new Vector2(1, 0) + currentPos;
            Vector2 west = new Vector2(-1, 0) + currentPos;

            if (north.Equals(nextPosition))
            {
                SetPosition((GetPosition() + new Vector2(0, -speed)));
                if (GetRotationInDegrees() > 180 && GetRotationInDegrees() < 360)
                    SetRotationInDegrees(GetRotationInDegrees() + 10);
                else if (GetRotationInDegrees() <= 180 && GetRotationInDegrees() > 0)
                    SetRotationInDegrees(GetRotationInDegrees() - 10);
            }
            else if (south.Equals(nextPosition))
            {
                SetPosition((GetPosition() + new Vector2(0, speed)));
                //SetRotationInDegrees(180);
                if (GetRotationInDegrees() > 180)
                    SetRotationInDegrees(GetRotationInDegrees() - 10);
                else if (GetRotationInDegrees() < 180)
                    SetRotationInDegrees(GetRotationInDegrees() + 10);
            }
            else if (east.Equals(nextPosition))
            {
                SetPosition((GetPosition() + new Vector2(speed, 0)));
                //SetRotationInDegrees(90);
                if (GetRotationInDegrees() > 90)
                    SetRotationInDegrees(GetRotationInDegrees() - 10);
                else if (GetRotationInDegrees() < 90)
                    SetRotationInDegrees(GetRotationInDegrees() + 10);
            }
            else if (west.Equals(nextPosition))
            {
                SetPosition((GetPosition() + new Vector2(-speed, 0)));
                //SetRotationInDegrees(270);
                if (GetRotationInDegrees() > 270)
                    SetRotationInDegrees(GetRotationInDegrees() - 10);
                else if (GetRotationInDegrees() < 270)
                    SetRotationInDegrees(GetRotationInDegrees() + 10);
            }
        }
    }
}
