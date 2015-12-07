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

        float postItTextureScal=0.25f;

        Random random = new Random();

        List<Tower> towers;

        GameManager gameManager;
        
        //RectangleOverlay towerScreen;

        float pauseAlpha;
        
        Vector2 ?postIt;

        MouseState oldMouseState;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }


        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            Level level = LevelCreator.GetLevel(1, content);

            gameManager = new GameManager(level);

            towers = TowerCreator.GetTowerTypes(content);

            gameFont = content.Load<SpriteFont>("gamefont");
            
            //enemy = new Enemy(new Vector2(0, 0), content.Load<Texture2D>("enemy"), 100, 1, 100);

            background = content.Load<Texture2D>("paperBackground169");
            plattform = content.Load<Texture2D>("platform");
            postItTexture = content.Load<Texture2D>("postit");


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

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                       input.GamePadWasConnected[playerIndex];

            if (input.IsPauseGame(ControllingPlayer) || gamePadDisconnected)
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
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

                if(mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton != ButtonState.Pressed)
                {
                    //gameManager.BuyTower(TowerCreator.GetTower(0, content, mouseState.Position.ToVector2() / (1200f / 3200f)));
                    if (postIt == null)
                        postIt = mouseState.Position.ToVector2();
                    else
                    {
                        Vector2 textureVector = new Vector2(postItTexture.Width * postItTextureScal, postItTexture.Height * postItTextureScal);
                        Rectangle postItRectangle = new Rectangle(((int)postIt.Value.X)+50-(((int)textureVector.X) / 2), ((int)postIt.Value.Y)-(((int)textureVector.Y)/2), ((int)textureVector.X), ((int)textureVector.Y));

                        int tower=-1;
                        for (int i=0; i<towers.Count; i++)
                        {
                            Rectangle r = postItRectangle;
                            r.X = r.X + i * (int)textureVector.X;
                            if (r.Contains(mouseState.Position))
                            {
                                tower = i;
                                break;
                            }
                        }
                        
                        if (tower!=-1)
                        {
                            gameManager.BuyTower(TowerCreator.GetTower(tower, content, ((Vector2)postIt) / (1200f / 3200f)));
                            postIt = null;
                        }
                        else postIt = mouseState.Position.ToVector2();
                    }
                }

                if (mouseState.RightButton == ButtonState.Pressed && oldMouseState.LeftButton != ButtonState.Pressed)
                {
                    //gameManager.BuyTower(TowerCreator.GetTower(0, content, mouseState.Position.ToVector2() / (1200f / 3200f)));
                    postIt = null;
                }
            }

            oldMouseState = mouseState;
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

            spriteBatch.DrawString(gameFont, "// TODO", playerPosition, Color.Green);

            spriteBatch.DrawString(gameFont, "Insert Gameplay Here",
                                   enemyPosition, Color.DarkRed);

            spriteBatch.End();

            drawGameObjects(spriteBatch, gameTime);
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
                spriteBatch.Draw(gameObject.GetTexture(), gameObject.GetPosition() * 100 * scal + new Vector2(5,5)*scal, null, new Color(200,200,200), 0, new Vector2(0, 0), gameObject.GetScale(), SpriteEffects.None, 0.19f);
            }
            foreach (GameObject gameObject in level.GetEnemies())
                spriteBatch.Draw(gameObject.GetTexture(), gameObject.GetPosition() * scal, null, Color.White, gameObject.GetRotation(), new Vector2(gameObject.GetTexture().Width / 2, gameObject.GetTexture().Height / 2), gameObject.GetScale(), SpriteEffects.None, 0.3f);

            foreach (GameObject gameObject in gameManager.getTower())
            {
                spriteBatch.Draw(gameObject.GetTexture(), gameObject.GetPosition() * scal, null, Color.White, gameObject.GetRotation(), new Vector2(gameObject.GetTexture().Width / 2, gameObject.GetTexture().Height / 2), gameObject.GetScale(), SpriteEffects.None, 0.4f);
                spriteBatch.Draw(plattform, gameObject.GetPosition() * scal, null, Color.White, 0, new Vector2(gameObject.GetTexture().Width / 2, gameObject.GetTexture().Height / 2), gameObject.GetScale(), SpriteEffects.None, 0.39f);
            }

            foreach (GameObject gameObject in level.GetProjectiles())
                spriteBatch.Draw(gameObject.GetTexture(), gameObject.GetPosition() * scal, null, Color.White, gameObject.GetRotation(), new Vector2(gameObject.GetTexture().Width / 2, gameObject.GetTexture().Height / 2), gameObject.GetScale(), SpriteEffects.None, 0.3f);

            spriteBatch.Draw(background, new Rectangle(0, 0, ScreenManager.GraphicsDevice.Viewport.Width, ScreenManager.GraphicsDevice.Viewport.Height), Color.White);

            if (postIt!=null)
            {
                float breite = postItTexture.Width * postItTextureScal;
                for (int i= 0; i< towers.Count; i++)
                {
                    Tower t = towers[i];
                    Color color;
                    if (t.GetCosts() > gameManager.player.GetPoints())
                        color = Color.Gray;
                    else color = Color.White;
                    spriteBatch.Draw(postItTexture, ((Vector2)postIt) + new Vector2(50 + i*breite, 0), null, color, 0, new Vector2(postItTexture.Width / 2, postItTexture.Height / 2), postItTextureScal, SpriteEffects.None, 0.5f);
                    spriteBatch.Draw(t.GetTexture(), ((Vector2)postIt) + new Vector2(50 + i * breite, 0), null, color, 0, new Vector2(t.GetTexture().Width / 2, t.GetTexture().Height / 2), t.GetScale(), SpriteEffects.None, 0.6f);
                    spriteBatch.Draw(plattform, ((Vector2)postIt) + new Vector2(50 + i * breite, 0), null, color, 0, new Vector2(t.GetTexture().Width / 2, t.GetTexture().Height / 2), t.GetScale(), SpriteEffects.None, 0.59f);
                }
            }
              
            if (gameManager.IsGameOver())
                spriteBatch.DrawString(gameFont, "Game  Over!!!", new Vector2(100, 100), Color.Black, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);

            if (gameManager.IsLevelFinished())
                spriteBatch.DrawString(gameFont, "You  won!!!", new Vector2(100, 100), Color.Black, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);

            spriteBatch.DrawString(gameFont, gameManager.player.GetPoints() + " ink", new Vector2(1000, 100), Color.DarkBlue, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(gameFont, gameManager.level.GetEnemies().Count+ gameManager.level.GetAllEnemies().Count + "Gegner uebrig", new Vector2(800, 200), Color.DarkBlue, 0, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);


            spriteBatch.End();
        }

        private void DrawTowerScreen(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack);

            //towerScreen.Draw(gameTime);

                                   
            for (int i=0; i<towers.Count; i++)
            {

            }

            spriteBatch.End();
        }


        #endregion
    }
}
