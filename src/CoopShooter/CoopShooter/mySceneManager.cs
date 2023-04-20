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
        public Scene MainMenu;
        public Scene Gameplay;
        public Scene GameOver;
        public Scene PauseMenu;
        public InputHandler sceneInput;
        public mySceneManager(Game game) : base(game)
        {
            Game.Components.Add(this);
            sceneInput = new InputHandler();
            MainMenu = new 
            Gameplay = new GameplayScene(game, this);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            sceneInput.Update();
        }
    }
}
