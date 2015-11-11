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
    class Game
    {
        public Level level {get; }

        public Game(Level level)
        {
            //this.level = level;

            // Zum 
            ContentManager content = new ContentManager(ScreenManager.Game.Services, "Content");
            level = new Level();
            level.enemies.Add(new Enemy(new Vector2(0, 0), content.Load<Texture2D>("enemy"), 1, 100, 1, 100));
            level.enemies.Add(new Enemy(new Vector2(200, 200), content.Load<Texture2D>("enemy"), 1, 100, 1, 100));

            level.path.Add(new PathBlock(new Vector2(0, 0), content.Load<Texture2D>("Dirt"), 1, PathBlockEnum.NORMAL));
        }
    }
}
