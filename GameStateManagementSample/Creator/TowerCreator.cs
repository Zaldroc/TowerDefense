﻿using GameStateManagementSample.GameObjects;
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
            Tower tower = new Tower(position, content.Load<Texture2D>("canon"), 1, 100, 100, 1);

            return tower;
        }

        public static List<Texture2D> GetTowerTypes(ContentManager content)
        {
            List<Texture2D> tower = new List<Texture2D>();

            tower.Add(content.Load<Texture2D>("canon"));

            return tower;
        }
        
            
    }
}
