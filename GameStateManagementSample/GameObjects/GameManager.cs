﻿using GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStateManagementSample.GameObjects
{
    class GameManager
    {
        public Level level;
        public Player player;

        private List<Tower> tower;

        public GameManager(Level level)
        {
            this.level = level;
            player = new Player();
            tower = new List<Tower>();
        }

        public void addTower(Tower tower)
        {
            this.tower.Add(tower);
        }

        public void Update()
        {
            level.Update();
        }
    }
}
