using CoopShooter;
using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmShooter
{
    public class EnemyManager : GameComponent
    {
        EnemySpawner spawner;
        Random random;

        InputHandler inputHandler;

        public EnemyManager(Game game, PlayerManager pm, Camera c) : base(game)
        {
            random = new Random();
            //Enemy e = new Enemy(Game, spawner, c, pm);
            Game.Components.Add(this);
            spawner = new EnemySpawner(Game, pm, c, 20);
            inputHandler = new InputHandler();
        }

        //maybe make a manager state enum cuz this could work for playermanager to
        public void ResetEnemies()
        {
            spawner.ResetObjects();
        }

        public override void Initialize()
        {
            base.Initialize();
           
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            inputHandler.Update();
            if (inputHandler.PressedKey(Microsoft.Xna.Framework.Input.Keys.E))
            {
                spawner.SpawnObject(getRandomSpawnPos());
                Debug.WriteLine("Spawned Enemy");
            }

        }

        Vector2 spawnPoint;
        bool toggle;
        protected Vector2 getRandomSpawnPos()
        {
            if (toggle)
            {
                spawnPoint.X = random.Next(Game.GraphicsDevice.Viewport.Width + 10, Game.GraphicsDevice.Viewport.Width + 30);
                spawnPoint.Y = random.Next(Game.GraphicsDevice.Viewport.Height + 10, Game.GraphicsDevice.Viewport.Height + 30);
                toggle = false;
            }
            else
            {
                spawnPoint.X = random.Next(-30, 10);
                spawnPoint.Y = random.Next(-30, 10);
                toggle = true;
            }

            return spawnPoint;
        }

    }
}
