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

        private int[,] grid;
        private int[,] map;
        
        private class Path
        {
            List<Vector2> path;
        }

        public Level()
        {
            Vector2 levelSize = new Vector2(32, 18);

            grid = new int[Convert.ToInt16(levelSize.X), Convert.ToInt16(levelSize.Y)];
            map = new int[Convert.ToInt16(levelSize.X)*100, Convert.ToInt16(levelSize.Y)*100];

            for(int i=0;i<levelSize.X;i++)
                for(int j=0;j<levelSize.Y;j++)
                    grid[i, j] = 0;

            for (int i = 0; i < levelSize.X*100; i++)
                for (int j = 0; j < levelSize.Y*100; j++)
                    map[i, j] = 0;

            enemies = new List<Enemy>();
            path = new List<PathBlock>();
        }

        public void AddEnemy(Enemy enemy)
        {
            enemies.Add(enemy);
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

            grid[Convert.ToInt32(pathBlock.GetPosition().X) / 100, Convert.ToInt32(pathBlock.GetPosition().Y) / 100] = value;
            path.Add(pathBlock);
        }

        private bool IsConnectedToOtherPathBlock(PathBlock pathBlock)
        {
            Vector2 pos = new Vector2(Convert.ToInt32(pathBlock.GetPosition().X) / 100, Convert.ToInt32(pathBlock.GetPosition().Y) / 100);

            return false;
        }

        public List<Enemy> GetEnemies()
        {
            return new List<Enemy>(enemies);
        }

        public List<PathBlock> GetPathBlocks()
        {
            return new List<PathBlock>(path);
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
    }
}
