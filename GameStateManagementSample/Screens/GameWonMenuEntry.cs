#region File Description
//-----------------------------------------------------------------------------
// PauseMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using System.Collections.Generic;
#endregion

namespace GameStateManagement
{
    /// <summary>
    /// The pause menu comes up over the top of the game,
    /// giving the player options to resume or quit.
    /// </summary>
    class GameWonMenuScreen : MenuScreen
    {
        #region Initialization

        int leveli;

        /// <summary>
        /// Constructor.
        /// </summary>
        public GameWonMenuScreen(int leveli)
            : base("You won!")
        {
            this.leveli = leveli;
            // Create our menu entries.
            MenuEntry nextLevelMenuEntry = new MenuEntry("Next Level");
            MenuEntry resumeGameMenuEntry = new MenuEntry("Replay Game");
            MenuEntry quitGameMenuEntry = new MenuEntry("Exit to Main Menu");

            // Hook up menu event handlers.
            nextLevelMenuEntry.Selected += NextLevelMenuEntrySelected;
            resumeGameMenuEntry.Selected += PlayGameMenuEntrySelected;
            quitGameMenuEntry.Selected += QuitGameMenuEntrySelected;

            // Add entries to the menu.
            MenuEntries.Add(nextLevelMenuEntry);
            MenuEntries.Add(resumeGameMenuEntry);
            MenuEntries.Add(quitGameMenuEntry);
        }


        #endregion

        #region Handle Input


        /// <summary>
        /// Event handler for when the Quit Game menu entry is selected.
        /// </summary>


        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to quit" message box. This uses the loading screen to
        /// transition from the game back to the main menu screen.
        /// </summary>
        void QuitGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(),
                                                           new MainMenuScreen());
        }

        void PlayGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            List<GameScreen> screensToLoad = new List<GameScreen>();
            screensToLoad.Add(new GameplayScreen(leveli));
            //screensToLoad.Add(new TowerScreen(5));
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex, screensToLoad.ToArray());
        }

        void NextLevelMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            List<GameScreen> screensToLoad = new List<GameScreen>();
            screensToLoad.Add(new GameplayScreen(leveli + 1));
            //screensToLoad.Add(new TowerScreen(5));
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex, screensToLoad.ToArray());
        }




        #endregion
    }
}
