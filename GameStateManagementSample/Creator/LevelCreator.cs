using GameStateManagementSample.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStateManagementSample.Creator
{
    class LevelCreator
    {
        public static Level GetLevel(int i, ContentManager content)
        {
            switch (i)
            {
                case 1: return getLevel1(content);

                default: return null;
            }
        }

        private static Level getLevel1(ContentManager content)
        {
            Level level = new Level();

            Texture2D dirt = content.Load<Texture2D>("paperPath3");
            level.AddPathBlock(new PathBlock(new Vector2(0, 0), dirt, 0.35f, PathBlockEnum.SPAWN));
            level.AddPathBlock(new PathBlock(new Vector2(0, 1), dirt, 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(0, 2), dirt, 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(0, 3), dirt, 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(0, 4), dirt, 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(0, 5), dirt, 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(0, 6), dirt, 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(0, 7), dirt, 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(0, 8), dirt, 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(0, 9), dirt, 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(1, 9), dirt, 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(2, 9), dirt, 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(3, 9), dirt, 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(4, 9), dirt, 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(5, 9), dirt, 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(6, 9), dirt, 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(7, 9), dirt, 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(8, 9), dirt, 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(9, 9), dirt, 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(10, 9), dirt, 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(11, 9), dirt, 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(12, 9), dirt, 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(13, 9), dirt, 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(14, 9), dirt, 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(15, 9), dirt, 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(16, 9), dirt, 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(17, 9), dirt, 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(18, 9), dirt, 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(19, 9), dirt, 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(20, 9), dirt, 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(21, 9), dirt, 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(21, 10), dirt, 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(21, 11), dirt, 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(21, 12), dirt, 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(21, 13), dirt, 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(21, 14), dirt, 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(21, 15), dirt, 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(21, 16), dirt, 0.35f, PathBlockEnum.NORMAL));

            level.AddPathBlock(new PathBlock(new Vector2(21, 17), dirt, 0.35f, PathBlockEnum.GOAL));

            GameObject spawn = level.GetSpawn();
            Vector2 spawnPos = new Vector2(spawn.GetPosition().X * 100 +50, spawn.GetPosition().Y * 100 + 50);


            
            for (int i=0; i<20; i++)
            {
                level.AddEnemy(new Enemy(spawnPos, content.Load<Texture2D>("bug"), 0.5f, 100, 1.0f, 100));
                level.AddEnemy(new Enemy(spawnPos, content.Load<Texture2D>("bug"), 0.5f, 100, 2.8f, 100));
                level.AddEnemy(new Enemy(spawnPos, content.Load<Texture2D>("bug"), 0.5f, 100, 2.4f, 100));
                level.AddEnemy(new Enemy(spawnPos, content.Load<Texture2D>("bug"), 0.5f, 100, 3.1f, 100));
                level.AddEnemy(new Enemy(spawnPos, content.Load<Texture2D>("bug"), 0.5f, 100, 2.2f, 100));
                level.AddEnemy(new Enemy(spawnPos, content.Load<Texture2D>("bug"), 0.5f, 100, 2.3f, 100));
                level.AddEnemy(new Enemy(spawnPos, content.Load<Texture2D>("bug"), 0.5f, 100, 3.0f, 100));
                level.AddEnemy(new Enemy(spawnPos, content.Load<Texture2D>("bug"), 0.5f, 100, 2.0f, 100));
                level.AddEnemy(new Enemy(spawnPos, content.Load<Texture2D>("bug"), 0.5f, 100, 2.1f, 100));
            }//*/


            //level.AddEnemy(new Enemy(spawnPos, content.Load<Texture2D>("bug"), 0.5f, 100, 3.0f, 100));

            return level;
        }
    }
}
