﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStateManagementSample.GameObjects
{
    class Level
    {
        private List<Enemy> enemies;
        private List<PathBlock> path;
        private List<Projectile> projectiles;
        public List<Vector4> corpses;

        private Queue<Wave> allWaves;
        private Wave currentWave;

        private int[,] grid;
        private int[,] map;

        private int maxEnemies;

        private Route route;

        private class Route
        {
            public List<Vector2> path;

            public Route()
            {
                path = new List<Vector2>();
            }
        }

        public List<Vector4> GetCorpses()
        {
            return corpses;
        }

        public Level()
        {
            Vector2 levelSize = new Vector2(32, 18);

            grid = new int[(int)levelSize.X, (int)levelSize.Y];
            map = new int[(int)levelSize.X * 100, (int)levelSize.Y * 100];

            for (int i = 0; i < levelSize.X; i++)
                for (int j = 0; j < levelSize.Y; j++)
                    grid[i, j] = 0;

            for (int i = 0; i < levelSize.X * 100; i++)
                for (int j = 0; j < levelSize.Y * 100; j++)
                    map[i, j] = 0;

            enemies = new List<Enemy>();
            path = new List<PathBlock>();
            corpses = new List<Vector4>();

            allWaves = new Queue<Wave>();

            route = new Route();
            projectiles = new List<Projectile>();

            maxEnemies = 0;
        }

        public float GetRestTimeRatio()
        {
            float ratio = currentWave.GetRestTimeRatio();

            if (ratio <= 0 && allWaves.Count > 0)
                return 1;
            return ratio;
        }

        public float GetRestEnemiesRatio()
        {
            return (float)GetAllEnemiesCount() / (float)maxEnemies;
        }

        public void SpawnEnemy(GameTime gameTime)
        {
            if (currentWave.Count()==0&&allWaves.Count!=0)
                currentWave = allWaves.Dequeue();

            if (currentWave.Count() != 0)
                enemies.AddRange(currentWave.getEnemies(gameTime));
        }

        public void AddWave(Wave wave)
        {
            if (HasPath())
            {
                wave.SetPath(route.path);
                if (currentWave!=null)
                    allWaves.Enqueue(wave);
                else currentWave = wave;
            }

            maxEnemies += wave.Count();
        }

        public void AddProjectile(Projectile projectile)
        {
            projectiles.Add(projectile);
        }

        public void AddPathBlock(PathBlock pathBlock)
        {
            int value = 0;

            switch (pathBlock.GetType())
            {
                case PathBlockEnum.NORMAL:
                    value = 1;
                    break;
                case PathBlockEnum.SPAWN:
                    value = 2;
                    break;
                case PathBlockEnum.GOAL:
                    value = 3;
                    break;
                default:
                    break;
            }

            if (IsConnectedToOtherPathBlock(pathBlock)&&((pathBlock.IsSpawn()&&GetSpawn()==null)||(pathBlock.IsGoal()&&GetGoal()==null&&GetSpawn()!=null)||(pathBlock.IsNormal()&&GetGoal()==null)))
            {
                grid[(int)pathBlock.GetPosition().X, (int)pathBlock.GetPosition().Y] = value;
                path.Add(pathBlock);
                route.path.Add(pathBlock.GetPosition());
            }
        }

        private bool IsConnectedToOtherPathBlock(PathBlock pathBlock)
        {
            Vector2 pos = pathBlock.GetPosition();

            if (pathBlock.IsNormal())
            {
                Vector2 north = new Vector2(0, -1) + pos;
                Vector2 south = new Vector2(0, 1) + pos;
                Vector2 east = new Vector2(1, 0) + pos;
                Vector2 west = new Vector2(-1, 0) + pos;

                int n = 0;
                int s = 0;
                int e = 0;
                int w = 0;

                if (!IsOutOfGrid(north))
                    n = grid[((int)north.X), ((int)north.Y)];
                if (!IsOutOfGrid(south))
                    s = grid[((int)south.X), ((int)south.Y)];
                if (!IsOutOfGrid(east))
                    e = grid[((int)east.X), ((int)east.Y)];
                if (!IsOutOfGrid(west))
                    w = grid[((int)west.X), ((int)west.Y)];

                if ((n > 0 && n < 4) ||
                    (s > 0 && s < 4) ||
                    (e > 0 && e < 4) ||
                    (w > 0 && w < 4))
                {
                    return true;
                }

            }
            else
                return true;

            return false;
        }

        private bool IsOutOfGrid(Vector2 pos)
        {
            if (pos.X >= 0 && pos.X < 32 && pos.Y >= 0 && pos.Y < 18)
                return false;
            return true;
        }

        private bool IsOutOfRange(Vector2 pos)
        {
            if (pos.X >= 0 && pos.X < 3200 && pos.Y >= 0 && pos.Y < 1800)
                return false;
            return true;
        }

        public List<Enemy> GetEnemies()
        {
            return new List<Enemy>(enemies);
        }

        public int GetAllEnemiesCount()
        {
            int count = 0;
            foreach (Wave wave in allWaves)
                count += wave.Count();
            count += currentWave.Count();
            count += enemies.Count;
            if (count!=200)
            {
                int a=5 + 5;
            }
            return count;
        }
        

        public List<PathBlock> GetPathBlocks()
        {
            return new List<PathBlock>(path);
        }

        public List<Projectile> GetProjectiles()
        {
            return new List<Projectile>(projectiles);
        }

        public bool HasPath()
        {
            bool hasSpawn = false;
            bool hasGoal = false;

            foreach (PathBlock p in path)
            {
                if (p.IsSpawn())
                {
                    hasSpawn = true;
                }
                if (p.IsGoal())
                {
                    hasGoal = true;
                }
            }

            if (!(hasGoal && hasSpawn))
                return false;

            return true;
        }

        public PathBlock GetSpawn()
        {
            foreach (PathBlock p in path)
            {
                if (p.IsSpawn())
                    return p;
            }
            return null;
        }

        public PathBlock GetGoal()
        {
            foreach (PathBlock p in path)
            {
                if (p.IsGoal())
                    return p;
            }
            return null;
        }

        public void Update()
        {
            foreach (Enemy e in enemies)
            {
                e.Move();
            }

            foreach (Projectile p in projectiles)
            {
                p.Move();
            }
        }

        public int checkColissions(GameTime gameTime)
        {
            int reward=0;
            for(int a=0; a < enemies.Count; a++)
            {
                Enemy e = enemies[a];
                Rectangle enemyRectangle = new Rectangle(e.GetPosition().ToPoint(), new Point(e.GetTexture().Width, e.GetTexture().Height));
                for (int b = 0; b < projectiles.Count; b++)
                {
                    Projectile p = projectiles[b];
                    Rectangle projectileRectangle = new Rectangle(p.GetPosition().ToPoint(), new Point(p.GetTexture().Width, p.GetTexture().Height));
                    if (!Rectangle.Intersect(enemyRectangle, projectileRectangle).IsEmpty)
                    {
                        if (p.GetType() == typeof(Bomb))
                        {
                            //Console.WriteLine("BOMB");

                            for (int i=0;i<enemies.Count; i++)
                            {
                                Enemy e2 = enemies[i];
                                double distance = Math.Sqrt(Math.Pow(p.GetPosition().X - e2.GetPosition().X, 2) + Math.Pow(p.GetPosition().Y - e2.GetPosition().Y, 2));
                                if (((Bomb)p).GetRange() >= distance)
                                {
                                    e2.Damage(p.GetDamage());
                                    if (e2.GetHealth() <= 0)
                                    {
                                        reward += e2.GetReward();
                                        enemies.Remove(e2);
                                        float rotation = Math.Abs(gameTime.TotalGameTime.Milliseconds - gameTime.TotalGameTime.Seconds * 1000);
                                        corpses.Add(new Vector4(e2.GetPosition(), gameTime.TotalGameTime.Seconds, rotation));
                                        i--;
                                    }
                                }
                            }
                        }
                        else
                        {
                            e.Damage(p.GetDamage());
                            if (e.GetHealth() <= 0)
                            {
                                reward += e.GetReward();
                                enemies.Remove(e);
                                float rotation = Math.Abs(gameTime.TotalGameTime.Milliseconds - gameTime.TotalGameTime.Seconds * 1000);
                                corpses.Add(new Vector4(e.GetPosition(), gameTime.TotalGameTime.Seconds, rotation));
                            }
                        }

                        projectiles.Remove(p);
                    }
                }
            }
            return reward;
        }

        private void removeOutOfRangeProjectiles()
        {
            for (int b = 0; b < projectiles.Count; b++)
            {
                Projectile p = projectiles[b];
                if (IsOutOfRange(p.GetPosition()))
                    projectiles.Remove(p);
            }
        }
    }
}
