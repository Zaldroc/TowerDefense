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
        private bool gameOver;
        private bool levelFinished;
        private int elapsedTime = 0;

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

        public void Update(GameTime gameTime)
        {
            if (!gameOver&&!levelFinished)
            try
            {
                elapsedTime += gameTime.ElapsedGameTime.Milliseconds;

                if (elapsedTime>500)
                {
                    elapsedTime = 0;
                    level.SpawnEnemy();
                }

                foreach (Tower t in tower)
                {
                    Projectile p = t.Shoot(gameTime.ElapsedGameTime.Milliseconds, level.GetEnemies());
                    if (p != null)
                        level.AddProjectile(p);
                }

                level.checkColissions();
                level.Update();

                levelFinished = level.GetEnemies().Count == 0 && level.GetAllEnemies().Count == 0;
            } catch (Exception e)
            {
                gameOver = true;
            }
        }

        public bool IsGameOver()
        {
            return gameOver;
        }

        public bool IsLevelFinished()
        {
            return levelFinished;
        }
    }
}
