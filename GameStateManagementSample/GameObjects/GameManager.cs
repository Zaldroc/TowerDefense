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
            player = new Player(250);
            tower = new List<Tower>();
        }

        public bool BuyTower(Tower t)
        {
            if(t.GetCosts()<=player.GetPoints())
            {
                Vector2 setPos = t.GetPosition() / 100.0f;
                setPos.X = (int)setPos.X;
                setPos.Y = (int)setPos.Y;

                foreach(PathBlock path in level.GetPathBlocks())
                {
                    if (path.GetPosition().Equals(setPos))
                        return false;
                }

                foreach (Tower t2 in getTower())
                {
                    Vector2 setPos2 = t2.GetPosition() / 100.0f;
                    setPos2.X = (int)setPos2.X;
                    setPos2.Y = (int)setPos2.Y;

                    if (setPos.Equals(setPos2))
                        return false;
                }

                setPos.X = (setPos.X+1) * 100 - t.GetTexture().Height / 2;
                setPos.Y = (setPos.Y+1) * 100 - t.GetTexture().Height / 2;
                t.SetPosition(setPos);

                addTower(t);
                player.RemovePoints(t.GetCosts());
                return true;
            }
            return false;
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

                int reward = level.checkColissions();
                player.AddPoints(reward);
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
