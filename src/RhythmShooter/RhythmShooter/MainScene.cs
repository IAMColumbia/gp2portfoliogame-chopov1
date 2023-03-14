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
        PlayerManager players;
        Camera camera;
        public MainScene(Game game, SceneManager manager) : base(game, manager)
        {
            camera = new Camera(game);
            players = new PlayerManager(game, camera);
        }
    }
}
