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
        public Level level;
        public Player player;
        private Boolean gameOver;

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

        public void addTower(List<Tower> tower)
        {
            this.tower.AddRange(tower);
        }

        public List<Tower> getTower()
        {
            return tower;
        }

        public void Update()
        {
            if (!gameOver)
            try {
                level.Update();
            } catch (Exception e)
            {
                gameOver = true;
            }
        }
    }
}
