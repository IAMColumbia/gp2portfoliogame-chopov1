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
        public Scene gamePlay;
        public Scene shop;
        public InputHandler sceneInput;
        public mySceneManager(Game game) : base(game)
        {
            Game.Components.Add(this);
            sceneInput = new InputHandler();
            gamePlay = new MainScene(game, this);
            shop = new ShopScene(game,this, ((MainScene)gamePlay).playerManager);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            sceneInput.Update();
        }
    }
}
