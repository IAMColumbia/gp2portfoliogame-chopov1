using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopShooter
{
    public class GameOverScene : Scene
    {
        GameOverUI gameOverUI;
        new mySceneManager sceneManager;
        public GameOverScene(Game game, mySceneManager manager) : base(game, manager)
        {
            sceneManager = manager;
            gameOverUI = new GameOverUI(game);
            addComponentToScene(gameOverUI);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (sceneManager.sceneInput.PressedBack)
            {
                sceneManager.ChangeScene(this, sceneManager.MainMenu);
            }
            if (sceneManager.sceneInput.PressedContinue)
            {
                sceneManager.ChangeScene(this,sceneManager.Gameplay);
            }
        }
    }
}
