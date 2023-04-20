using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopShooter
{
    public class MainMenu : Scene
    {
        MenuController inputHandler;
        MenuUI ui;
        public MainMenu(Game game, SceneManager manager) : base(game, manager)
        {
            inputHandler = new MenuController();
            ui = new MainMenuUI(game);
            addComponentToScene(ui);
        }

        public override void Update(GameTime gameTime)
        {
            inputHandler.Update();
            base.Update(gameTime);
            if (inputHandler.PressedContinue)
            {
                sceneManager.ChangeScene(this, ((mySceneManager)sceneManager).Gameplay);
            }
        }
    }
}
