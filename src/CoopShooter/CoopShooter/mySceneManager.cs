using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using RhythmGameLibrary;

namespace CoopShooter
{
    public class mySceneManager : SceneManager
    {
        public int HighScore;
        public Scene MainMenu;
        public Scene Gameplay;
        public Scene GameOver;
        public Scene PauseMenu;
        public MenuController sceneInput;
        public mySceneManager(Game game) : base(game)
        {
            Game.Components.Add(this);
            sceneInput = new MenuController();
            MainMenu = new MainMenu(game, this);
            Gameplay = new GameplayScene(game, this);
            PauseMenu = new PauseScene(game, this);
            GameOver = new GameOverScene(game, this);
            
            MainMenu.State = SceneState.loading;
            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            sceneInput.Update();
        }
    }
}
