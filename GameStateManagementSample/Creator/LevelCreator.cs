﻿using GameStateManagementSample.GameObjects;
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
                case 2: return getLevel2(content);

                default: throw new NotImplementedException();
            }
        }

        private static Level getLevel1(ContentManager content)
        {
            Level level = new Level();

            Texture2D dirt = content.Load<Texture2D>("paperPath3");
            level.AddPathBlock(new PathBlock(new Vector2(0, 0), dirt, new Vector2(1,1)*0.35f, PathBlockEnum.SPAWN));

            level.AddPathBlock(new PathBlock(new Vector2(0, 1), dirt, new Vector2(1,1)*0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(0, 2), dirt, new Vector2(1,1)*0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(0, 3), dirt, new Vector2(1,1)*0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(0, 4), dirt, new Vector2(1,1)*0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(0, 5), dirt, new Vector2(1,1)*0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(0, 6), dirt, new Vector2(1,1)*0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(0, 7), dirt, new Vector2(1,1)*0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(0, 8), dirt, new Vector2(1,1)*0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(0, 9), dirt, new Vector2(1,1)*0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(1, 9), dirt, new Vector2(1,1)*0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(2, 9), dirt, new Vector2(1,1)*0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(3, 9), dirt, new Vector2(1,1)*0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(4, 9), dirt, new Vector2(1,1)*0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(5, 9), dirt, new Vector2(1,1)*0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(6, 9), dirt, new Vector2(1,1)*0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(7, 9), dirt, new Vector2(1,1)*0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(8, 9), dirt, new Vector2(1,1)*0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(9, 9), dirt, new Vector2(1,1)*0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(10, 9), dirt, new Vector2(1,1)*0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(11, 9), dirt, new Vector2(1,1)*0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(12, 9), dirt, new Vector2(1,1)*0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(13, 9), dirt, new Vector2(1,1)*0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(14, 9), dirt, new Vector2(1,1)*0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(15, 9), dirt, new Vector2(1,1)*0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(16, 9), dirt, new Vector2(1,1)*0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(17, 9), dirt, new Vector2(1,1)*0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(18, 9), dirt, new Vector2(1,1)*0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(19, 9), dirt, new Vector2(1,1)*0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(20, 9), dirt, new Vector2(1,1)*0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(21, 9), dirt, new Vector2(1,1)*0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(21, 10), dirt, new Vector2(1,1)*0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(21, 11), dirt, new Vector2(1,1)*0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(21, 12), dirt, new Vector2(1,1)*0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(21, 13), dirt, new Vector2(1,1)*0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(21, 14), dirt, new Vector2(1,1)*0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(21, 15), dirt, new Vector2(1,1)*0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(21, 16), dirt, new Vector2(1,1)*0.35f, PathBlockEnum.NORMAL));

            level.AddPathBlock(new PathBlock(new Vector2(21, 17), dirt, new Vector2(1,1)*0.35f, PathBlockEnum.GOAL));

            GameObject spawn = level.GetSpawn();
            Vector2 spawnPos = new Vector2(spawn.GetPosition().X * 100 +50, spawn.GetPosition().Y * 100 + 50);


            
            for (int i=0; i<40; i++)
            {
                Queue<Enemy> enemies = new Queue<Enemy>();

                Texture2D bug = content.Load<Texture2D>("bug");
                Texture2D spider = content.Load<Texture2D>("spider");

                float speed = 1 + 0.001f * i;
                int startHealth = 90;
                float healthFactor = 1 + 0.7f * i;

                int health = (int)(startHealth * healthFactor);

                enemies.Enqueue(new Enemy(spawnPos, bug, new Vector2(1,1)*0.5f, health, 2.0f * speed, 25));
                enemies.Enqueue(new Enemy(spawnPos, spider, new Vector2(1,1)*0.5f, health, 2.8f * speed, 25));
                if(i>4)
                enemies.Enqueue(new Enemy(spawnPos, bug, new Vector2(1,1)*0.5f, health, 2.4f * speed, 25));
                if(i>9)
                enemies.Enqueue(new Enemy(spawnPos, spider, new Vector2(1,1)*0.5f, health, 3.1f * speed, 25));
                if(i>19)
                enemies.Enqueue(new Enemy(spawnPos, bug, new Vector2(1,1)*0.5f, health, 2.2f * speed, 25));
                if(i>29)
                enemies.Enqueue(new Enemy(spawnPos, bug, new Vector2(1,1)*0.5f, health, 2.3f * speed, 25));
                if(i>39)
                enemies.Enqueue(new Enemy(spawnPos, spider, new Vector2(1,1)*0.5f, health, 3.0f * speed, 25));
                if(i>35)
                enemies.Enqueue(new Enemy(spawnPos, bug, new Vector2(1,1)*0.5f, health, 2.0f * speed, 25));
                if(i>44)
                enemies.Enqueue(new Enemy(spawnPos, bug, new Vector2(1,1)*0.5f, health, 2.1f * speed, 25));

                if((i+1)%5==0)
                enemies.Enqueue(new Enemy(spawnPos, bug, new Vector2(1,1)*0.8f, health * 2, 1.0f * speed, 50));

                Wave wave = new Wave(enemies, (int)(10000*(1+0.01f*i)), 500);
                level.AddWave(wave);
            }//*/


            //level.AddEnemy(new Enemy(spawnPos, content.Load<Texture2D>("bug"), 0.5f, 100, 3.0f, 100));

            return level;
        }

        private static Level getLevel2(ContentManager content)
        {
            Level level = new Level();

            Texture2D dirt = content.Load<Texture2D>("paperPath3");
            level.AddPathBlock(new PathBlock(new Vector2(6, 17), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.SPAWN));

            level.AddPathBlock(new PathBlock(new Vector2(6, 16), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(6, 15), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(6, 14), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(6, 13), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(6, 12), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(6, 11), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(5, 11), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(4, 11), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(3, 11), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(2, 11), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(2, 10), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(2, 9), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(2, 8), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(2, 7), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(3, 7), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(4, 7), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(5, 7), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(6, 7), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(7, 7), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(8, 7), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(9, 7), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(10, 7), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(11, 7), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(12, 7), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(13, 7), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(14, 7), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(15, 7), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(16, 7), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(17, 7), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(18, 7), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(19, 7), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(20, 7), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(21, 7), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(22, 7), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(23, 7), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(24, 7), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(25, 7), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(26, 7), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(27, 7), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(28, 7), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(29, 7), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(30, 7), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(31, 7), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(31, 6), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(31, 5), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(31, 4), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(31, 3), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(31, 2), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));
            level.AddPathBlock(new PathBlock(new Vector2(31, 1), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.NORMAL));

            level.AddPathBlock(new PathBlock(new Vector2(31, 0), dirt, new Vector2(1, 1) * 0.35f, PathBlockEnum.GOAL));

            GameObject spawn = level.GetSpawn();
            Vector2 spawnPos = new Vector2(spawn.GetPosition().X * 100 + 50, spawn.GetPosition().Y * 100 + 50);



            for (int i = 0; i < 40; i++)
            {
                Queue<Enemy> enemies = new Queue<Enemy>();

                Texture2D bug = content.Load<Texture2D>("bug");
                Texture2D spider = content.Load<Texture2D>("spider");

                enemies.Enqueue(new Enemy(spawnPos, bug, new Vector2(1, 1) * 0.5f, 100 * (i + 1), 1.0f, 25));
                enemies.Enqueue(new Enemy(spawnPos, spider, new Vector2(1, 1) * 0.5f, 100 * (i + 1), 2.8f, 25));
                enemies.Enqueue(new Enemy(spawnPos, bug, new Vector2(1, 1) * 0.5f, 100 * (i + 1), 2.4f, 25));
                enemies.Enqueue(new Enemy(spawnPos, spider, new Vector2(1, 1) * 0.5f, 100 * (i + 1), 3.1f, 25));
                enemies.Enqueue(new Enemy(spawnPos, bug, new Vector2(1, 1) * 0.5f, 100 * (i + 1), 2.2f, 25));
                enemies.Enqueue(new Enemy(spawnPos, bug, new Vector2(1, 1) * 0.5f, 100 * (i + 1), 2.3f, 25));
                enemies.Enqueue(new Enemy(spawnPos, spider, new Vector2(1, 1) * 0.5f, 100 * (i + 1), 3.0f, 25));
                enemies.Enqueue(new Enemy(spawnPos, bug, new Vector2(1, 1) * 0.5f, 100 * (i + 1), 2.0f, 25));
                enemies.Enqueue(new Enemy(spawnPos, bug, new Vector2(1, 1) * 0.5f, 100 * (i + 1), 2.1f, 25));

                enemies.Enqueue(new Enemy(spawnPos, bug, new Vector2(1, 1) * 0.8f, 100 * 2 * i, 2.1f, 50));

                Wave wave = new Wave(enemies, 10000, 500);
                level.AddWave(wave);
            }//*/


            //level.AddEnemy(new Enemy(spawnPos, content.Load<Texture2D>("bug"), 0.5f, 100, 3.0f, 100));

            return level;
        }
    }
}
