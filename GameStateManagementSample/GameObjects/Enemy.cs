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

        private Queue<Vector2> path;
        private Vector2 nextPosition;

        public Enemy(Vector2 position, Texture2D texture, Vector2 scale, int health, float speed, int reward):base(position,texture,scale)
        {
            this.health = health;
            this.speed = speed;
            this.reward = (int)(speed * 0.06f + 1.0f*Math.Log(10*health)) + 15;

            //this.reward += reward;

            nextPosition = new Vector2(-1,-1);
        }

        public Enemy(Vector2 position, Texture2D texture, Vector2 scale, int health, float speed)
            :this(position,texture,scale,health,speed,0)
        {

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
            if(nextPosition==new Vector2(-1,-1))
            {
                nextPosition = path.Dequeue();
            }

            

            Vector2 pos = GetPosition();
            
            //Vector2 currentPos = new Vector2((int)Math.Round((pos.X / 100),MidpointRounding.AwayFromZero)-1, (int)Math.Round((pos.Y / 100),MidpointRounding.AwayFromZero)-1);


            //pos.X = pos.X + (GetTexture().Width*GetScale().X) / 2;
            
            Vector2 currentPos = pos / 100.0f;


            currentPos.X = (int)currentPos.X;
            currentPos.Y = (int)currentPos.Y;
           

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

            float turningSpeed = 3 * speed;

            if (north.Equals(nextPosition))
            {
                SetPosition((GetPosition() + new Vector2(0, -speed)));
                if (GetRotationInDegrees() > 180 && GetRotationInDegrees() < 360)
                    SetRotationInDegrees(GetRotationInDegrees() + 3*speed);
                else if (GetRotationInDegrees() <= 180 && GetRotationInDegrees() > 0)
                    SetRotationInDegrees(GetRotationInDegrees() - turningSpeed);
            }
            else if (south.Equals(nextPosition))
            {
                SetPosition((GetPosition() + new Vector2(0, speed)));
                //SetRotationInDegrees(180);
                if (GetRotationInDegrees() > 180)
                    SetRotationInDegrees(GetRotationInDegrees() - turningSpeed);
                else if (GetRotationInDegrees() < 180)
                    SetRotationInDegrees(GetRotationInDegrees() + turningSpeed);
            }
            else if (east.Equals(nextPosition))
            {
                SetPosition((GetPosition() + new Vector2(speed, 0)));
                //SetRotationInDegrees(90);
                if (GetRotationInDegrees() > 90)
                    SetRotationInDegrees(GetRotationInDegrees() - turningSpeed);
                else if (GetRotationInDegrees() < 90)
                    SetRotationInDegrees(GetRotationInDegrees() + turningSpeed);
            }
            else if (west.Equals(nextPosition))
            {
                SetPosition((GetPosition() + new Vector2(-speed, 0)));
                //SetRotationInDegrees(270);
                if (GetRotationInDegrees() > 270)
                    SetRotationInDegrees(GetRotationInDegrees() - turningSpeed);
                else if (GetRotationInDegrees() < 270)
                    SetRotationInDegrees(GetRotationInDegrees() + turningSpeed);
                //System.Console.WriteLine(pos.ToString() + " " + currentPos.ToString() + " " + nextPosition.ToString());
            }
        }
    }
}
