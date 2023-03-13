using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameLibrary
{
    public class MainMenu : Scene
    {
        Sprite test;
        Camera camera;
        MenuUI display;
        RhythmManager rm;
        public MainMenu(Game game, SceneManager manager) : base(game, manager)
        {
            display = new MainMenuUI(game);
            camera = new Camera(Game);
            //Sprite test = new Sprite(Game, "Rocket-Sheet", camera);
            AnimatableSprite poop = new AnimatableSprite(Game, "Rocket-Sheet", 4, .4f, camera);
            rm = new RhythmManager(game);
        }
    }
}
