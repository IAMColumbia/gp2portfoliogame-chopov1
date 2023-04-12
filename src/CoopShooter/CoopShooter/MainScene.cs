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
        CollisionManager cm;
        PlayerManager players;
        EnemyManager enemies;
        Camera camera;
        public MainScene(Game game, SceneManager manager) : base(game, manager)
        {
            camera = new Camera(game);
            cm = new CollisionManager(game);
            players = new PlayerManager(game, camera);
            enemies = new EnemyManager(game, players, camera);
            
        }
    }
}
