#region File Description
//-----------------------------------------------------------------------------
// GameplayScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameStateManagementSample.GameObjects;
using GameStateManagementSample.Creator;
using System.Collections.Generic;
using GameStateManagementSample.Screens;
#endregion

namespace GameStateManagement
{
    /// <summary>
    /// This screen implements the actual game logic. It is just a
    /// placeholder to get the idea across: you'll probably want to
    /// put some more interesting gameplay in here!
    /// </summary>
    class GameplayScreen : GameScreen
    {
        #region Fields

        ContentManager content;
        SpriteFont gameFont;

        Vector2 playerPosition = new Vector2(100, 100);
        Vector2 enemyPosition = new Vector2(100, 100);

        Texture2D background;
        Texture2D plattform;
        Texture2D postItTexture;
        Texture2D eraser;
        Texture2D upgrade;

        float postItTextureScal=0.25f;

        Random random = new Random();

        List<Tower> towers;

        GameManager gameManager;
        
        //RectangleOverlay towerScreen;

        float pauseAlpha;
        
        Vector2 ?postIt;

        MarkedField marked;

        MouseState oldMouseState;

        Texture2D inkDrop;
        Texture2D corpseTexture;
        Texture2D spawnTexture;
        Texture2D goalTexture;

        Texture2D time;
        Texture2D wave1;
        Texture2D wave2;

        Texture2D bar;
        Texture2D barFilling;

        int leveli;

        bool postItRichtungRechts;

        Vector2 ?circlePosition;
        Texture2D circle;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public GameplayScreen(int level)
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
            this.leveli = level;
        }


        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            Level level = LevelCreator.GetLevel(leveli, content);

            gameManager = new GameManager(level);

            towers = TowerCreator.GetTowerTypes(content);

            gameFont = content.Load<SpriteFont>("gamefont");

            float scal = 1200f / 3200f;

            //enemy = new Enemy(new Vector2(0, 0), content.Load<Texture2D>("enemy"), 100, 1, 100);

            background = content.Load<Texture2D>("paperBackground169");
            plattform = content.Load<Texture2D>("platform");
            postItTexture = content.Load<Texture2D>("postit");
            inkDrop = content.Load<Texture2D>("drop");
            corpseTexture = content.Load<Texture2D>("splash");
            spawnTexture = content.Load<Texture2D>("spawn");
            goalTexture = content.Load<Texture2D>("goal");
            bar = content.Load<Texture2D>("bar");
            barFilling = content.Load<Texture2D>("barFilling");
            eraser = content.Load<Texture2D>("eraser"); 
            upgrade = content.Load<Texture2D>("upgrade");

            time = content.Load<Texture2D>("time");
            wave1 = content.Load<Texture2D>("bug");
            wave2 = content.Load<Texture2D>("spider");

            marked = new MarkedField(new Vector2(0, 0), content.Load<Texture2D>("markedField"), new Vector2(1,1)*0.5f);

            //towerScreen = new RectangleOverlay(r, Color.Red, game, ScreenManager.SpriteBatch);

            // A real game would probably have more content than this sample, so
            // it would take longer to load. We simulate that by delaying for a
            // while, giving you a chance to admire the beautiful loading screen.
            //Thread.Sleep(1000);

            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            ScreenManager.Game.ResetElapsedTime();
        }


        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void UnloadContent()
        {
            content.Unload();
        }


        #endregion

        #region Update and Draw


        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
            else
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);
            
            if (IsActive)
            {
                // Apply some random jitter to make the enemy move around.
                const float randomization = 10;

                enemyPosition.X += (float)(random.NextDouble() - 0.5) * randomization;
                enemyPosition.Y += (float)(random.NextDouble() - 0.5) * randomization;

                // Apply a stabilizing force to stop the enemy moving off the screen.
                Vector2 targetPosition = new Vector2(
                    ScreenManager.GraphicsDevice.Viewport.Width / 2 - gameFont.MeasureString("Insert Gameplay Here").X / 2, 
                    200);

                enemyPosition = Vector2.Lerp(enemyPosition, targetPosition, 0.05f);

                // TODO: this game isn't very fun! You could probably improve
                // it by inserting something more interesting in this space :-)

                gameManager.Update(gameTime);
                if (gameManager.IsGameOver())
                    ScreenManager.AddScreen(new GameOverMenuScreen(leveli), ControllingPlayer);

                if (gameManager.IsLevelFinished())
                    ScreenManager.AddScreen(new GameWonMenuScreen(leveli), ControllingPlayer);
            }
        }


        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];
            GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];

            MouseState mouseState = Mouse.GetState();

            float scal = 1200f / 3200f;
            
            Vector2 mPos = new Vector2(mouseState.X / 100.0f / scal, mouseState.Y / 100.0f/scal);
            mPos.X = (int)mPos.X;
            mPos.Y = (int)mPos.Y;

            if (postIt == null)
            {
                circlePosition = null;
                marked.SetPosition(mPos);
                if (gameManager.IsThereAPath(new Vector2(mouseState.X / scal, mouseState.Y / scal)))
                    marked.NotOk();
                else
                    marked.Ok();
            }
            else
            {
                Vector2 textureVector = new Vector2(postItTexture.Width * postItTextureScal, postItTexture.Height * postItTextureScal);
                Rectangle postItRectangle = new Rectangle(((int)postIt.Value.X) - (((int)textureVector.X) / 2), ((int)postIt.Value.Y) - (((int)textureVector.Y) / 2), ((int)textureVector.X), ((int)textureVector.Y));

                bool isClickedATower = gameManager.IsThereATower((Vector2)postIt / scal);
                bool upgradeAvailable = false;

                int tower = -1;
                int count;
                if (!isClickedATower)
                    count = towers.Count;
                else if (gameManager.getTower((Vector2)postIt / scal).UpgradeAvailable())
                {
                    count = 2;
                    upgradeAvailable = true;
                }
                else count = 1;

                for (int i = 0; i < count; i++)
                {
                    Rectangle r = postItRectangle;
                    int x;
                    if (postItRichtungRechts)
                        x = 50 + i * (int)textureVector.X;
                    else x = -50 - i * (int)textureVector.X;

                    r.X = r.X + x;
                    if (r.Contains(mouseState.Position))
                    {
                        tower = i;
                        break;
                    }
                }

                if (tower != -1)
                {
                    if (!isClickedATower)
                    {
                        Tower t = towers[tower];
                        if (circlePosition == null || !circlePosition.Equals(((Vector2)postIt) / (1200f / 3200f) / 100))
                        {
                            circlePosition = ((Vector2)postIt) / (1200f / 3200f);
                            Vector2 setPos = ((Vector2)circlePosition) / 100.0f;
                            setPos.X = (int)setPos.X *100 + 50;
                            setPos.Y = (int)setPos.Y*100 + 50;
                            circlePosition = setPos;
                            circle = CreateCircle((int)(t.GetRange() * scal));
                        }
                    }
                    /*else if (upgradeAvailable && tower == 0)
                        gameManager.upgradeTower(gameManager.getTower((Vector2)postIt / scal));
                    else gameManager.RemoveTower(gameManager.getTower((Vector2)postIt / scal));*/
                }
                else circlePosition = null;
            }

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                       input.GamePadWasConnected[playerIndex];

            if (input.IsPauseGame(ControllingPlayer) || gamePadDisconnected)
            {
                ScreenManager.AddScreen(new PauseMenuScreen(leveli), ControllingPlayer);
            }
            else
            {
                // Otherwise move the player position.
                Vector2 movement = Vector2.Zero;

                if (keyboardState.IsKeyDown(Keys.Left))
                    movement.X--;

                if (keyboardState.IsKeyDown(Keys.Right))
                    movement.X++;

                if (keyboardState.IsKeyDown(Keys.Up))
                    movement.Y--;

                if (keyboardState.IsKeyDown(Keys.Down))
                    movement.Y++;

                Vector2 thumbstick = gamePadState.ThumbSticks.Left;

                movement.X += thumbstick.X;
                movement.Y -= thumbstick.Y;

                if (movement.Length() > 1)
                    movement.Normalize();

                playerPosition += movement * 5;
                
                if(mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton != ButtonState.Pressed &&!gameManager.IsGameOver()&&!gameManager.IsLevelFinished())
                {
                    marked.Mark();
                    circle = null;
                    //gameManager.BuyTower(TowerCreator.GetTower(0, content, mouseState.Position.ToVector2() / (1200f / 3200f)));
                    if (postIt == null)
                    {
                        if (marked.IsOk())
                        {
                            postIt = mouseState.Position.ToVector2();
                            berechnePostItsRichtung();
                        }
                    }
                    else
                    {
                        Vector2 textureVector = new Vector2(postItTexture.Width * postItTextureScal, postItTexture.Height * postItTextureScal);
                        Rectangle postItRectangle = new Rectangle(((int)postIt.Value.X) - (((int)textureVector.X) / 2), ((int)postIt.Value.Y) - (((int)textureVector.Y) / 2), ((int)textureVector.X), ((int)textureVector.Y));

                        bool isClickedATower = gameManager.IsThereATower((Vector2)postIt / scal);
                        bool upgradeAvailable = false;

                        int tower = -1;
                        int count;
                        if (!isClickedATower)
                            count = towers.Count;
                        else if (gameManager.getTower((Vector2)postIt / scal).UpgradeAvailable())
                        {
                            count = 2;
                            upgradeAvailable = true;
                        }
                        else count = 1;

                        for (int i = 0; i < count; i++)
                        {
                            Rectangle r = postItRectangle;
                            int x;
                            if (postItRichtungRechts)
                                x = 50 + i * (int)textureVector.X;
                            else x = -50 - i * (int)textureVector.X;

                            r.X = r.X + x;
                            if (r.Contains(mouseState.Position))
                            {
                                tower = i;
                                break;
                            }
                            else
                            {
                                marked.SetPosition(mPos);
                                marked.Mark();
                            }
                        }

                        if (tower != -1)
                        {
                            if (!isClickedATower)
                                gameManager.BuyTower(TowerCreator.GetTower(tower, content, ((Vector2)postIt) / (1200f / 3200f)));
                            else if (upgradeAvailable && tower == 0)
                                gameManager.upgradeTower(gameManager.getTower((Vector2)postIt / scal));
                            else gameManager.RemoveTower(gameManager.getTower((Vector2)postIt / scal));

                            postIt = null;
                            marked.DeMark();
                        }
                        else
                        {
                            marked.SetPosition(mPos);
                            if (gameManager.IsThereAPath(new Vector2(mouseState.X / scal, mouseState.Y / scal)))
                            {
                                marked.NotOk();
                                postIt = null;
                                marked.DeMark();
                            }
                            else
                            {
                                marked.Ok();
                                postIt = mouseState.Position.ToVector2();
                                berechnePostItsRichtung();
                            }
                        }
                    }
                }

                if (mouseState.RightButton == ButtonState.Pressed && oldMouseState.LeftButton != ButtonState.Pressed)
                {
                    //gameManager.BuyTower(TowerCreator.GetTower(0, content, mouseState.Position.ToVector2() / (1200f / 3200f)));
                    postIt = null;
                    marked.DeMark();
                }
            }

            oldMouseState = mouseState;
        }

        private void berechnePostItsRichtung()
        {
            int fensterBreite = 1200;
            float textureBreite = postItTexture.Width * postItTextureScal;
            int count;
            float scal = 1200f / 3200f;

            if (!gameManager.IsThereATower((Vector2)postIt / scal))
                count = towers.Count;
            else if (gameManager.getTower((Vector2)postIt / scal).UpgradeAvailable())
                count = 2;
            else count = 1;


            if (postIt.Value.X + count * textureBreite + 50 < fensterBreite)
                postItRichtungRechts = true;
            else postItRichtungRechts = false;
        }


        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            // This game has a blue background. Why? Because!
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                               Color.CornflowerBlue, 0, 0);

            // Our player and enemy are both actually just text strings.
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin(SpriteSortMode.FrontToBack);

            spriteBatch.End();

            drawGameObjects(spriteBatch, gameTime);
            DrawHUD(spriteBatch, new Vector2(800, 20));
            //DrawTowerScreen(spriteBatch, gameTime);

            // If the game is transitioning on or off, fade it out to black.ddfgdf
            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }


        private void drawGameObjects(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack);

            //spriteBatch.Draw(enemy.GetTexture(), enemy.GetPosition(), Color.White);

            Level level = gameManager.level;

            float scal = 1200f / 3200f;

            foreach (GameObject gameObject in level.GetPathBlocks())
            {
                spriteBatch.Draw(gameObject.GetTexture(), gameObject.GetPosition() * 100 * scal, null, Color.White, 0, new Vector2(0, 0), gameObject.GetScale(), SpriteEffects.None, 0.2f);
                //spriteBatch.Draw(gameObject.GetTexture(), gameObject.GetPosition() * 100 * scal + new Vector2(5,5)*scal, null, new Color(200,200,200), 0, new Vector2(0, 0), gameObject.GetScale(), SpriteEffects.None, 0.19f);
                if(((PathBlock)gameObject).IsSpawn())
                    spriteBatch.Draw(spawnTexture, new Vector2(gameObject.GetPosition().X * 100 +10, gameObject.GetPosition().Y * 100 +10) * scal, null, Color.White, 0, new Vector2(0, 0), gameObject.GetScale()*1.8f, SpriteEffects.None, 0.21f);
                if (((PathBlock)gameObject).IsGoal())
                    spriteBatch.Draw(goalTexture, new Vector2(gameObject.GetPosition().X * 100 + 10, gameObject.GetPosition().Y * 100 + 10) * scal, null, Color.White, 0, new Vector2(0, 0), gameObject.GetScale() * 1.8f, SpriteEffects.None, 0.21f);
            }
            foreach (GameObject gameObject in level.GetEnemies())
                spriteBatch.Draw(gameObject.GetTexture(), gameObject.GetPosition() * scal, null, Color.White, gameObject.GetRotation(), new Vector2(gameObject.GetTexture().Width / 2, gameObject.GetTexture().Height / 2), gameObject.GetScale(), SpriteEffects.None, 0.3f);

            foreach(Vector4 corpse in level.GetCorpses())
                spriteBatch.Draw(corpseTexture,new Vector2(corpse.X,corpse.Y) * scal, null, Color.White, corpse.W, new Vector2(corpseTexture.Width/2,corpseTexture.Height/2), 0.5f, SpriteEffects.None, 0.29f);

            foreach (GameObject gameObject in gameManager.getTower())
            {
                spriteBatch.Draw(gameObject.GetTexture(), gameObject.GetPosition() * scal, null, Color.White, gameObject.GetRotation(), new Vector2(gameObject.GetTexture().Width / 2, gameObject.GetTexture().Height / 2), gameObject.GetScale(), SpriteEffects.None, 0.4f);
                spriteBatch.Draw(plattform, gameObject.GetPosition() * scal, null, Color.White, 0, new Vector2(gameObject.GetTexture().Width / 2, gameObject.GetTexture().Height / 2), gameObject.GetScale().Y, SpriteEffects.None, 0.39f);
            }

            foreach (GameObject gameObject in level.GetProjectiles())
                spriteBatch.Draw(gameObject.GetTexture(), gameObject.GetPosition() * scal, null, Color.White, gameObject.GetRotation(), new Vector2(gameObject.GetTexture().Width / 2, gameObject.GetTexture().Height / 2), gameObject.GetScale(), SpriteEffects.None, 0.395f);

            spriteBatch.Draw(background, new Rectangle(0, 0, ScreenManager.GraphicsDevice.Viewport.Width, ScreenManager.GraphicsDevice.Viewport.Height), Color.White);

            if (postIt!=null)
            {
                float breite = postItTexture.Width * postItTextureScal;

                if (!gameManager.IsThereATower((Vector2)postIt / scal))
                {
                    for (int i = 0; i < towers.Count; i++)
                    {
                        Tower t = towers[i];
                        Color color;
                        if (t.GetCosts() > gameManager.player.GetPoints())
                            color = Color.Gray;
                        else color = Color.White;

                        float x;
                        if (postItRichtungRechts)
                            x = 50 + i * breite;
                        else x = -50 - i * breite;

                        spriteBatch.Draw(postItTexture, ((Vector2)postIt) + new Vector2(x, 0), null, color, 0, new Vector2(postItTexture.Width / 2, postItTexture.Height / 2), postItTextureScal, SpriteEffects.None, 0.5f);
                        spriteBatch.Draw(t.GetTexture(), ((Vector2)postIt) + new Vector2(x, 0), null, color, 0, new Vector2(t.GetTexture().Width / 2, t.GetTexture().Height / 2), t.GetScale(), SpriteEffects.None, 0.6f);
                        spriteBatch.Draw(plattform, ((Vector2)postIt) + new Vector2(x, 0), null, color, 0, new Vector2(t.GetTexture().Width / 2, t.GetTexture().Height / 2), t.GetScale().Y, SpriteEffects.None, 0.59f);
                        spriteBatch.DrawString(gameFont, t.GetCosts().ToString(), ((Vector2)postIt) + new Vector2(x, 30), Color.DarkBlue, 0, new Vector2(t.GetTexture().Width / 2, t.GetTexture().Height / 2), t.GetScale().Y * 0.6f, SpriteEffects.None, 0.61f);
                    }

                    if (circlePosition!=null)
                        spriteBatch.Draw(circle, (Vector2)circlePosition * scal, null, Color.DarkBlue, 0f, new Vector2(circle.Width / 2, circle.Height / 2), 1f, SpriteEffects.None, 0.7f);

                }
                else
                {
                    Color color;
                    Tower t = gameManager.getTower((Vector2)postIt / scal);
                    int upgradeAvailable;

                    if (t.UpgradeAvailable())
                        upgradeAvailable = 1;
                    else upgradeAvailable = 0;

                    if (t.GetUpgradeCosts() > gameManager.player.GetPoints())
                        color = Color.Gray;
                    else color = Color.White;

                    float x;
                    if (postItRichtungRechts)
                        x = 50 + upgradeAvailable * breite;
                    else x = -50 - upgradeAvailable * breite;

                    float x2;
                    if (postItRichtungRechts)
                        x2 = 50;
                    else x2 = -50;



                    if (upgradeAvailable == 1)
                    {
                        spriteBatch.Draw(postItTexture, ((Vector2)postIt) + new Vector2(x2, 0), null, color, 0, new Vector2(postItTexture.Width / 2, postItTexture.Height / 2), postItTextureScal, SpriteEffects.None, 0.5f);
                        spriteBatch.Draw(upgrade, ((Vector2)postIt) + new Vector2(x2, 0), null, color, 0, new Vector2(upgrade.Width / 2, upgrade.Height / 2), 0.5f, SpriteEffects.None, 0.6f);
                        spriteBatch.DrawString(gameFont, t.GetUpgradeCosts().ToString(), ((Vector2)postIt) + new Vector2(x2, 30), Color.DarkBlue, 0, new Vector2(t.GetTexture().Width / 2, t.GetTexture().Height / 2), t.GetScale().Y * 0.6f, SpriteEffects.None, 0.61f);
                    }

                    spriteBatch.Draw(postItTexture, ((Vector2)postIt) + new Vector2(x, 0), null, Color.White, 0, new Vector2(postItTexture.Width / 2, postItTexture.Height / 2), postItTextureScal, SpriteEffects.None, 0.5f);
                    spriteBatch.Draw(eraser, ((Vector2)postIt) + new Vector2(x, 0), null, Color.White, 0, new Vector2(eraser.Width / 2, eraser.Height / 2), 0.5f, SpriteEffects.None, 0.6f);
                    spriteBatch.DrawString(gameFont, "-25", ((Vector2)postIt) + new Vector2(x, 30), Color.DarkBlue, 0, new Vector2(t.GetTexture().Width / 2, t.GetTexture().Height / 2), t.GetScale().Y * 0.6f, SpriteEffects.None, 0.61f);

                    if (circle == null)
                        circle = CreateCircle((int)(t.GetRange() * scal));

                    if (circlePosition == null)
                        spriteBatch.Draw(circle, t.GetPosition() * scal, null, Color.DarkBlue, 0f, new Vector2(circle.Width / 2, circle.Height / 2), 1f, SpriteEffects.None, 0.7f);

                }
            }
              
                        
            //.DrawString(gameFont, gameManager.level.GetAllEnemiesCount() + "Gegner uebrig", new Vector2(800, 200), Color.DarkBlue, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);

            spriteBatch.Draw(marked.GetTexture(), (marked.GetPosition()*100+ new Vector2(10, 10)) * scal, null, marked.GetColor(), 0, new Vector2(0, 0), marked.GetScale().Y, SpriteEffects.None, 0.41f);

            spriteBatch.End();
        }

        private void DrawHUD(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(time, new Vector2(position.X - 20, position.Y+5), null, Color.White, 0, new Vector2(0), 0.35f, SpriteEffects.None, 0.98f);

            spriteBatch.Draw(bar, position, null, Color.White, 0, new Vector2(0, 0), new Vector2(1, 1), SpriteEffects.None, 1f);
            for (int i = 0; i < gameManager.GetFillingCountTime(); i++)
            {
                spriteBatch.Draw(barFilling, new Vector2(position.X + 5 + i * 0.45f, position.Y + 5), null, Color.White, 0, new Vector2(0, 0), 0.9f, SpriteEffects.None, 0.98f);
            }


            spriteBatch.Draw(wave1, new Vector2(position.X + 75, position.Y + 5), null, Color.White, 0, new Vector2(0), 0.35f, SpriteEffects.None, 0.98f);
            spriteBatch.Draw(wave2, new Vector2(position.X + 77, position.Y + 4), null, Color.White, 0, new Vector2(0), 0.35f, SpriteEffects.None, 0.97f);
            spriteBatch.Draw(bar, new Vector2(position.X + 100, position.Y), null, Color.White, 0, new Vector2(0, 0), new Vector2(1, 1), SpriteEffects.None, 1f);
            for (int i = 0; i < gameManager.GetFillingCountEnemies(); i++)
            {
                spriteBatch.Draw(barFilling, new Vector2(position.X +105 + i * 0.45f, position.Y + 5), null, Color.White, 0, new Vector2(0, 0), 0.9f, SpriteEffects.None, 0.98f);
            }

            spriteBatch.DrawString(gameFont, gameManager.player.GetPoints().ToString(), new Vector2(position.X + 220, position.Y+2), Color.DarkBlue, 0, new Vector2(0, 0), 0.6f, SpriteEffects.None, 1f);
            spriteBatch.Draw(inkDrop, new Vector2(position.X+200, position.Y), null, Color.White, 0, new Vector2(0, 0), 0.3f, SpriteEffects.None, 1f);

            spriteBatch.End();
        }

        public Texture2D CreateCircle(int radius)
        {
            int outerRadius = radius * 2 + 2; // So circle doesn't go out of bounds
            Texture2D texture = new Texture2D(ScreenManager.GraphicsDevice, outerRadius, outerRadius);

            Color[] data = new Color[outerRadius * outerRadius];

            // Colour the entire texture transparent first.
            for (int i = 0; i < data.Length; i++)
                data[i] = Color.Transparent;

            // Work out the minimum step necessary using trigonometry + sine approximation.
            double angleStep = 1f / radius;

            for (double angle = 0; angle < Math.PI * 2; angle += angleStep)
            {
                int x = (int)Math.Round(radius + radius * Math.Cos(angle));
                int y = (int)Math.Round(radius + radius * Math.Sin(angle));

                data[y * outerRadius + x + 1] = Color.White;
            }

            texture.SetData(data);
            return texture;
        }
        #endregion
    }
}
