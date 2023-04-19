using CoopShooter;
using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopShooter
{
    public class MainScene : Scene
    {
        CollisionManager cm;
        public PlayerManager playerManager { get; private set; }
        EnemyManager enemies;
        Camera camera;
        GameplayUI gameplayUI;

        new mySceneManager sceneManager;
        public MainScene(Game game, mySceneManager manager) : base(game, manager)
        {
            sceneManager = manager;
            Game.Components.Add(this);
            camera = new Camera(game);
            cm = new CollisionManager(game);
            playerManager = new PlayerManager(game, camera);
            enemies = new EnemyManager(game, playerManager, camera);
            gameplayUI = new GameplayUI(game, playerManager);
            addCompsToScene();
        }

        void addCompsToScene()
        {
            addComponentToScene(camera);
            addComponentToScene(playerManager.p1);
            addComponentToScene(playerManager.p2);

        }

        public override void Initialize()
        {
            //base.Initialize();
            State = SceneState.active;
        }

        protected override void SceneUpdate()
        {
            base.SceneUpdate();
            if (playerManager.ResetGame)
            {
                enemies.ResetEnemies();
                playerManager.ResetPlayers();
                playerManager.ResetGame = false;
            }
            if (sceneManager.sceneInput.ReleasedKey(Microsoft.Xna.Framework.Input.Keys.B))
            {
                OpenShop();
            }
        }

        private void OpenShop()
        {
            sceneManager.ChangeScene(((mySceneManager)sceneManager).gamePlay, ((mySceneManager)sceneManager).shop);
        }
    }
}
