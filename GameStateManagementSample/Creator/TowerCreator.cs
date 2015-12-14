using GameStateManagementSample.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStateManagementSample.Creator
{
    class TowerCreator
    {
        public static Tower GetTower(int i, ContentManager content, Vector2 position)
        {
            Texture2D bullet = content.Load<Texture2D>("bullet");
            Texture2D rocket = content.Load<Texture2D>("rocket");

            if (i == 1)
            {
                Projectile projectile = new Projectile(position, bullet, new Vector2(1,1)* 0.4f, 25, 10, new Vector2(1, 1));
                return new Tower(position, content.Load<Texture2D>("canon"), new Vector2(1.0f,1)*0.5f, 400, 200, 300, projectile);
            }

            if (i == 0)
            {
                Projectile projectile = new Projectile(position, bullet, new Vector2(0.5f, 1) * 0.4f, 5, 25, new Vector2(1, 1));
                return new Tower(position, content.Load<Texture2D>("smallCanon"), new Vector2(1f, 1) * 0.5f, 600, 100, 200, projectile);
            }

            if (i == 2)
            {
                Projectile projectile = new Projectile(position, bullet, new Vector2(0.5f, 1) * 0.4f, 50, 35, new Vector2(1, 1));
                return new Tower(position, content.Load<Texture2D>("smallCanon"), new Vector2(1f, 1) * 0.5f, 1000, 450, 1000, projectile);
            }
            if (i == 3)
            {
                Bomb bomb = new Bomb(position, rocket, new Vector2(1, 1) * 0.4f, 1000, 5, new Vector2(1, 1), 50);
                return new Tower(position, content.Load<Texture2D>("canon"), new Vector2(1f, 1) * 0.5f, 1000, 2000, 5000, bomb);
            }

            return null;            
        }

        public static List<Tower> GetTowerTypes(ContentManager content)
        {
            List<Tower> tower = new List<Tower>();
            for(int i=0;i<10;i++)
            {
                Tower t = GetTower(i, content, new Vector2(0, 0));
                if (t != null)
                    tower.Add(t);
                else
                    break;
            }

            return tower;
        }
        
            
    }
}
