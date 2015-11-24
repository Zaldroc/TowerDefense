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
        public static Tower GetTower(String path, ContentManager content)
        {
            Tower tower = new Tower(new Vector2(0, 0), content.Load<Texture2D>("tower"), 1, 100, 100, 1);

            return tower;
        }

        public static List<Texture2D> GetTowerTypes(ContentManager content)
        {
            List<Texture2D> tower = new List<Texture2D>();

            tower.Add(content.Load<Texture2D>("tower"));

            return tower;
        }
        
            
    }
}
