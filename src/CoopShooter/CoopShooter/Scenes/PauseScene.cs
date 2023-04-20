using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopShooter
{
    public class PauseScene : Scene
    {
        new mySceneManager sceneManager;
        PauseUI shopUI;
        

        public PauseScene(Game game, mySceneManager manager) : base(game, manager)
        {
            shopUI = new PauseUI(game);
            addComponentToScene(shopUI);
            sceneManager = manager;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (sceneManager.sceneInput.PressedContinue)
            {
                resume();
            }
        }

        private void resume()
        {
            sceneManager.ChangeScene(this, sceneManager.Gameplay);
        }

    }
}
