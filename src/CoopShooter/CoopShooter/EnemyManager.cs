using CoopShooter;
using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Timers;

namespace CoopShooter
{
    public class EnemyManager : GameComponent
    {
        public EnemySpawner spawner { get; private set; }
        Random random;

        InputHandler inputHandler;

        Timer spawnTimer;

        public EnemyManager(Game game, PlayerManager pm, Camera c) : base(game)
        {
            random = new Random();
            //Enemy e = new Enemy(Game, spawner, c, pm);
            Game.Components.Add(this);
            spawner = new EnemySpawner(Game, pm, c, 20);
            inputHandler = new InputHandler();
            spawnTimer = new Timer(500);
            spawnTimer.AutoReset = true;
            spawnTimer.Elapsed += SpawnEnemy;
        }


        public void SpawnEnemy(Object source, ElapsedEventArgs e)
        {
            int spawn = random.Next(0, 12);
            switch (spawn)
            {
                default:
                    break;
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    spawner.SpawnObject(getRandomSpawnPos());
                    Debug.WriteLine("Spawned Enemy");
                    break;
            }
        }

        //maybe make a manager state enum cuz this could work for playermanager to
        public void ResetEnemies()
        {
            
            spawner.ResetObjects();
        }

        public override void Initialize()
        {
            base.Initialize();
           spawnTimer.Start();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //old code to test enemies
            /*inputHandler.Update();
            if (inputHandler.PressedKey(Microsoft.Xna.Framework.Input.Keys.E))
            {
                spawner.SpawnObject(getRandomSpawnPos());
                Debug.WriteLine("Spawned Enemy");
            }*/

        }

        Vector2 spawnPoint;
        protected Vector2 getRandomSpawnPos()
        {
            int side = random.Next(0, 4);
            switch (side)
            {
                case 0:
                    //right of screen
                    spawnPoint.X = random.Next(Game.GraphicsDevice.Viewport.Width + 10, Game.GraphicsDevice.Viewport.Width + 30);
                    spawnPoint.Y = random.Next(0, Game.GraphicsDevice.Viewport.Height);
                    break;
                case 1:
                    //left
                    spawnPoint.X = random.Next(-30, 0);
                    spawnPoint.Y = random.Next(0, Game.GraphicsDevice.Viewport.Height);
                    break;
                case 2:
                    //bottom
                    spawnPoint.X = random.Next(0, Game.GraphicsDevice.Viewport.Width);
                    spawnPoint.Y = random.Next(Game.GraphicsDevice.Viewport.Height + 10, Game.GraphicsDevice.Viewport.Height + 30);
                    break;
                case 3:
                    //top
                    spawnPoint.X = random.Next(0, Game.GraphicsDevice.Viewport.Width);
                    spawnPoint.Y = random.Next(-30, 0);
                    break;
            }
           
            return spawnPoint;
        }

    }
}
