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

        private int pointsRegenTime = 0;

        private int fillingsCountTime = 100;
        private int fillingsCountEnemies = 100;

        private List<Tower> tower;

        public GameManager(Level level)
        {
            this.level = level;
            player = new Player(300);
            tower = new List<Tower>();
        }

        public bool IsThereAPath(Vector2 position)
        {
            Vector2 setPos = position / 100.0f;
            setPos.X = (int)setPos.X;
            setPos.Y = (int)setPos.Y;
            foreach (PathBlock path in level.GetPathBlocks())
            {
                if (path.GetPosition().Equals(setPos))
                    return true;
            }
            return false;
        }

        public bool IsThereATower(Vector2 position)
        {
            Vector2 setPos = position / 100.0f;
            setPos.X = (int)setPos.X;
            setPos.Y = (int)setPos.Y;

            foreach (Tower t2 in getTower())
            {
                Vector2 setPos2 = t2.GetPosition() / 100.0f;
                setPos2.X = (int)setPos2.X;
                setPos2.Y = (int)setPos2.Y;

                if (setPos.Equals(setPos2))
                    return true;
            }

            return false;
        }

        public bool CanBuild(Vector2 position)
        {
            return !IsThereATower(position) && !IsThereAPath(position);
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
                pointsRegenTime += gameTime.ElapsedGameTime.Milliseconds;
                 
                level.SpawnEnemy(gameTime);
                
                for (int i=0;i<level.corpses.Count;i++)
                {
                    Vector4 c = level.corpses[i];
                    if (gameTime.TotalGameTime.Seconds - c.Z > 5)
                    {
                        level.corpses.Remove(c);
                    }
                }
                    

                if (pointsRegenTime > 1000)
                {
                    pointsRegenTime = 0;
                    player.AddPoints(1);
                }

                foreach (Tower t in tower)
                {
                    Projectile p = t.Shoot(gameTime.ElapsedGameTime.Milliseconds, level.GetEnemies());
                    if (p != null)
                        level.AddProjectile(p);

                    t.Move();
                }

                int reward = level.checkColissions(gameTime);
                player.AddPoints(reward);
                level.Update();

                levelFinished = level.GetAllEnemiesCount() == 0;

                fillingsCountTime = (int)(level.GetRestTimeRatio()*100);
                fillingsCountEnemies = (int)(level.GetRestEnemiesRatio() * 100);
                } catch (Exception e)
            {
                gameOver = true;
            }
        }

        public int GetFillingCountTime()
        {
            return fillingsCountTime;
        }

        public int GetFillingCountEnemies()
        {
            return fillingsCountEnemies;
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
