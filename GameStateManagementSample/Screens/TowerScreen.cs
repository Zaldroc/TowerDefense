using GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStateManagementSample.Screens
{
    class TowerScreen : GameScreen
    {
        #region Fields

        ContentManager content;
        int towerCount;
        Texture2D t;
        Rectangle r;
        #endregion

        #region Initialization
        /// <summary>
        /// Constructor.
        /// </summary>
        /// 

        public TowerScreen(int towerCount)
        {
            this.towerCount = towerCount;
            base.IsPopup = true;
        }

        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            t = content.Load<Texture2D>("background");
            r = new Rectangle(viewport.Width-300, 0, 300, viewport.Height);
        }


        /// <summary>
        /// Unloads graphics content for this screen.
        /// </summary>
        public override void UnloadContent()
        {
            content.Unload();
        }


        #endregion

        #region Update and Draw


        /// <summary>
        /// Updates the background screen. Unlike most screens, this should not
        /// transition off even if it has been covered by another screen: it is
        /// supposed to be covered, after all! This overload forces the
        /// coveredByOtherScreen parameter to false in order to stop the base
        /// Update method wanting to transition off.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
                        

            spriteBatch.Begin();

            spriteBatch.Draw(t, r, Color.White);

            spriteBatch.End();
        }

        #endregion
    }
}
