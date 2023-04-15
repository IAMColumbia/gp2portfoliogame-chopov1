using CoopShooter;
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

        GameplayUI gameplayUI;
        public MainScene(Game game, SceneManager manager) : base(game, manager)
        {
            Game.Components.Add(this);
            camera = new Camera(game);
            cm = new CollisionManager(game);
            players = new PlayerManager(game, camera);
            enemies = new EnemyManager(game, players, camera);
            gameplayUI = new GameplayUI(game, players);
        }

        public override void Initialize()
        {
            //base.Initialize();
            State = SceneState.active;
        }

        protected override void SceneUpdate()
        {
            base.SceneUpdate();
            if (players.ResetGame)
            {
                enemies.ResetEnemies();
                players.ResetPlayers();
                players.ResetGame = false;
            }
        }
    }
}
