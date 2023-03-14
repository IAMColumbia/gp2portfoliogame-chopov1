using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmShooter
{
    public class MainScene : Scene
    {
        Player player;
        Camera camera;
        public MainScene(Game game, SceneManager manager) : base(game, manager)
        {
            camera = new Camera(game); 
            player = new Player(game, 0,"TestShip2", 1, 1, camera);
            //player = new Player(game, 1,"TestShip1", 1, 2, camera);
        }
    }
}
