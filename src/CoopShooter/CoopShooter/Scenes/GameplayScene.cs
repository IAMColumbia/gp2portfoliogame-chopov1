using CoopShooter.PowerUps;
using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopShooter
{
    public class GameplayScene : Scene
    {
        CollisionManager cm;
        DynamicTextSpawner dts;
        public PlayerManager playerManager { get; private set; }
        EnemyManager enemies;
        Camera camera;
        GameplayUI gameplayUI;
        PowerUpManager powerUps;
        new mySceneManager sceneManager;

        MenuController menuController;
        public GameplayScene(Game game, mySceneManager manager) : base(game, manager)
        {
            menuController = new MenuController();
            sceneManager = manager;
            camera = new Camera(game);
            cm = new CollisionManager(game);
            dts = new DynamicTextSpawner(game, camera);
            playerManager = new PlayerManager(game, camera);
            enemies = new EnemyManager(game, playerManager, camera);
            gameplayUI = new GameplayUI(game, playerManager);
            powerUps = new PowerUpManager(game, camera, playerManager);
            addCompsToScene();
        }

        void addCompsToScene()
        {
            addComponentToScene(gameplayUI);
            addComponentToScene(camera);
            addComponentToScene(playerManager.p1);
            addComponentToScene(playerManager.p2);
            addComponentToScene(enemies);
            addComponentToScene(powerUps);
        }

        protected override void SceneUpdate()
        {
            menuController.Update();
            base.SceneUpdate();
            if (menuController.PressedBack)
            {
                sceneManager.ChangeScene(this, sceneManager.PauseMenu);
            }
            if (playerManager.ResetGame)
            {
                updateHighScore();
                enemies.ResetSprites();
                powerUps.ResetSprites();
                playerManager.ResetPlayers();
                playerManager.ResetGame = false;
                sceneManager.ChangeScene(this, sceneManager.GameOver);
            }
            
            
        }

        void updateHighScore()
        {
            if (playerManager.TotalKills > sceneManager.HighScore)
            {
                sceneManager.HighScore = playerManager.TotalKills;
            }
        }

    }
}
