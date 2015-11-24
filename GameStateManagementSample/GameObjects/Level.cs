using Microsoft.Xna.Framework;
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

        private int[,] grid;
        private int[,] map;
        
        private class Path
        {
            List<Vector2> path;
        }

        public Level()
        {
            Vector2 levelSize = new Vector2(32, 18);

            grid = new int[(int)levelSize.X, (int)levelSize.Y];
            map = new int[(int)levelSize.X*100, (int)levelSize.Y*100];

            for(int i=0;i<levelSize.X;i++)
                for(int j=0;j<levelSize.Y;j++)
                    grid[i, j] = 0;

            for (int i = 0; i < levelSize.X*100; i++)
                for (int j = 0; j < levelSize.Y*100; j++)
                    map[i, j] = 0;

            enemies = new List<Enemy>();
            path = new List<PathBlock>();
            projectiles = new List<Projectile>();
        }

        public void AddEnemy(Enemy enemy)
        {
            enemies.Add(enemy);
        }

        public void AddProjectile(Projectile projectile)
        {
            projectiles.Add(projectile);
        }

        public void AddPathBlock(PathBlock pathBlock)
        {
            int value=0;

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

            if (IsConnectedToOtherPathBlock(pathBlock))
            {
                grid[(int)pathBlock.GetPosition().X, (int) pathBlock.GetPosition().Y] = value;
                path.Add(pathBlock);
            }
        }

        private bool IsConnectedToOtherPathBlock(PathBlock pathBlock)
        {
            Vector2 pos = pathBlock.GetPosition();

            if (pathBlock.IsNormal())
            {
                Vector2 north = new Vector2(0,-1) + pos;
                Vector2 south = new Vector2(0, 1) + pos;
                Vector2 east = new Vector2(1, 0) + pos;
                Vector2 west = new Vector2(-1, 0) + pos;
                
                int n=0;
                int s=0;
                int e=0;
                int w=0;

                if(!IsOutOfGrid(north))
                    n = grid[((int)north.X), ((int)north.Y)];
                if (!IsOutOfGrid(south))
                    s = grid[((int)south.X), ((int)south.Y)];
                if (!IsOutOfGrid(east))
                    e = grid[((int)east.X), ((int)east.Y)];
                if (!IsOutOfGrid(west))
                    w = grid[((int)west.X), ((int)west.Y)];

                if ((n > 0 && n < 4)||
                    (s > 0 && s < 4)||
                    (e > 0 && e < 4)||
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

        public List<Enemy> GetEnemies()
        {
            return new List<Enemy>(enemies);
        }

        public List<PathBlock> GetPathBlocks()
        {
            return new List<PathBlock>(path);
        }

        public List<Projectile> GetProjectiles()
        {
            return new List<Projectile>(projectiles);
        }

        private bool HasPath()
        {
            bool hasSpawn = false;
            bool hasGoal = false;

            foreach(PathBlock p in path)
            {
                if (p.IsSpawn())
                    hasSpawn = true;
                if (p.IsGoal())
                    hasGoal = true;
            }

            if (!hasGoal && !hasSpawn)
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
    }
}
