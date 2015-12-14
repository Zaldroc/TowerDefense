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

        private int lastDirection;

        public Enemy(Vector2 position, Texture2D texture, Vector2 scale, int health, float speed, int reward):base(position,texture,scale)
        {
            this.health = health;
            this.speed = speed;
            //this.reward = (int)(speed * 0.06f + 1.0f*Math.Log(10*health)) + 15;

            this.reward = (int)((Math.Log(speed) + 2.0f * Math.Log(10000 * health)));

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


            switch(lastDirection)
            {
                case 0:
                    pos.Y += 50;
                    break;
                case 2:
                    pos.Y -= 50;
                    break;
                case 1:
                    pos.X -= 50;
                    break;
                case 3:
                    pos.X += 50;
                    break;
            }
            
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

            float degrees = GetRotationInDegrees();

            degrees = degrees % 360;

            if (degrees < 0)
                degrees = 360 + degrees;

            if (north.Equals(nextPosition))
            {
                SetPosition((GetPosition() + new Vector2(0, -speed)));
                if (degrees > 180 && degrees < 360)
                    SetRotationInDegrees(degrees + turningSpeed);
                else if (degrees <= 180 && degrees > 0)
                    SetRotationInDegrees(degrees - turningSpeed);

                lastDirection = 0;
            }
            else if (south.Equals(nextPosition))
            {
                SetPosition((GetPosition() + new Vector2(0, speed)));
                //SetRotationInDegrees(180);
                if (degrees > 180)
                    SetRotationInDegrees(degrees - turningSpeed);
                else if (degrees < 180)
                    SetRotationInDegrees(degrees + turningSpeed);

                lastDirection = 2;
            }
            else if (east.Equals(nextPosition))
            {
                SetPosition((GetPosition() + new Vector2(speed, 0)));
                //SetRotationInDegrees(90);
                if (degrees > 270 || degrees < 90)
                    SetRotationInDegrees(degrees + turningSpeed);
                else if (degrees < 270 || degrees >90)
                    SetRotationInDegrees(degrees - turningSpeed);

                lastDirection = 1;
            }
            else if (west.Equals(nextPosition))
            {
                SetPosition((GetPosition() + new Vector2(-speed, 0)));
                

                //SetRotationInDegrees(270);
                if (degrees>270||degrees<90)
                    SetRotationInDegrees(degrees - turningSpeed);
                else if (degrees < 270)
                    SetRotationInDegrees(degrees + turningSpeed);
                //System.Console.WriteLine(pos.ToString() + " " + currentPos.ToString() + " " + nextPosition.ToString());

                lastDirection = 3;
            }
        }
    }
}
