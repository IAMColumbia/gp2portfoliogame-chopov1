using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace CoopShooter
{
    public class MovingSpriteManager : GameComponent, ISceneComponenet
    {
        int minIndicatorTime;
        int maxIndicatorTime;

        protected PlayerManager playerManager;
        public MovingSpriteManager(Game game, Camera c, PlayerManager pm, int spawnRate) : base(game)
        {
            playerManager = pm;
            minIndicatorTime = 200;
            maxIndicatorTime = 300;
            random = new Random();
            Game.Components.Add(this);
            spawnTimer = new Timer(spawnRate);
            spawnTimer.AutoReset = true;
            spawnTimer.Elapsed += SpawnSprite;
        }

        public Spawner spawner { get; protected set; }
        protected Random random;

        Timer spawnTimer;

        public virtual void SpawnSprite(Object source, ElapsedEventArgs e)
        {
           
        }

        //maybe make a manager state enum cuz this could work for playermanager to
        public virtual void ResetSprites()
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
        }

        Vector2 spawnPoint;
        protected virtual Vector2 getRandomSpawnPos()
        {
            int side = random.Next(0, 4);
            switch (side)
            {
                case 0:
                    //right of screen
                    spawnPoint.X = random.Next(Game.GraphicsDevice.Viewport.Width + minIndicatorTime, Game.GraphicsDevice.Viewport.Width + maxIndicatorTime);
                    spawnPoint.Y = random.Next(0, Game.GraphicsDevice.Viewport.Height);
                    break;
                case 1:
                    //left
                    spawnPoint.X = random.Next(-maxIndicatorTime, -minIndicatorTime);
                    spawnPoint.Y = random.Next(0, Game.GraphicsDevice.Viewport.Height);
                    break;
                case 2:
                    //bottom
                    spawnPoint.X = random.Next(0, Game.GraphicsDevice.Viewport.Width);
                    spawnPoint.Y = random.Next(Game.GraphicsDevice.Viewport.Height + minIndicatorTime, Game.GraphicsDevice.Viewport.Height + maxIndicatorTime);
                    break;
                case 3:
                    //top
                    spawnPoint.X = random.Next(0, Game.GraphicsDevice.Viewport.Width);
                    spawnPoint.Y = random.Next(-maxIndicatorTime, -minIndicatorTime);
                    break;
            }

            return spawnPoint;
        }

        public void Load()
        {
            //load spawner stuff
            spawner.Load();
            Enabled = true;
            spawnTimer.Start();
        }

        public void UnLoad()
        {
            //unload spawner stuff
            Enabled = false;
            spawnTimer.Stop();
            spawner.UnLoad();
        }
    }
}
