using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Timers;

namespace CoopShooter.Enemies
{
    public class EnemyManager : MovingSpriteManager
    {
        int spawnOdds;
        int maxSpawnOdds;
        public EnemyManager(Game game, PlayerManager pm, Camera c) : base(game, c, pm, 200)
        {
            spawner = new EnemySpawner(Game, pm, c, 24);
            maxSpawnOdds = 15;
            spawnOdds = maxSpawnOdds;
        }

        public override void SpawnSprite(object source, ElapsedEventArgs e)
        {
            if(playerManager.TotalLevel < maxSpawnOdds)
            {
                spawnOdds = ( maxSpawnOdds - playerManager.TotalLevel);
            }
            int spawn = random.Next(0, spawnOdds);
            switch (spawn)
            {
                default:
                    break;
                case 0:
                    spawner.SpawnObject(getRandomSpawnPos());
                    break;
            }
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

    }
}
