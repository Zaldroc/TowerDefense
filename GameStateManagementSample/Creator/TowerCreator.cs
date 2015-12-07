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
            Texture2D projectileTexture = content.Load<Texture2D>("bullet");
            if (i == 0)
            {
                Projectile projectile = new Projectile(position, projectileTexture, 0.4f, 25, 10, new Vector2(1, 1));
                return new Tower(position, content.Load<Texture2D>("canon"), 0.5f, 400, 100, 300, projectile);
            }

            if (i == 1)
            {
                Projectile projectile = new Projectile(position, projectileTexture, 0.4f, 5, 50, new Vector2(1, 1));
                return new Tower(position, content.Load<Texture2D>("canon"), 0.5f, 400, 175, 150, projectile);
            }

            return null;            
        }

        public static List<Tower> GetTowerTypes(ContentManager content)
        {
            List<Tower> tower = new List<Tower>();

            tower.Add(new Tower(new Vector2(0,0), content.Load<Texture2D>("canon"), 0.5f, 400, 100, 300, null));
            tower.Add(new Tower(new Vector2(0, 0), content.Load<Texture2D>("canon"), 0.5f, 400, 100, 300, null));

            return tower;
        }
        
            
    }
}
