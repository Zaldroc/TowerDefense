using GameStateManagement;
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
        public Level level {get; }
        public Player player { get; }

        private List<Tower> tower;

        public GameManager(Level level)
        {
            this.level = level;
            player = new Player();
        }

        public void addTower(Tower tower)
        {
            this.tower.Add(tower);
        }

        public void update()
        {

        }
    }
}
