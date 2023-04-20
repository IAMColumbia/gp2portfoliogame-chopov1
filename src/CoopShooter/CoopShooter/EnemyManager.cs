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
    public class EnemyManager : MovingSpriteManager
    {
        public EnemyManager(Game game, PlayerManager pm, Camera c) : base(game, c, pm, 500)
        {
            spawner = new EnemySpawner(Game, pm, c, 20);
        }


        public override void SpawnSprite(Object source, ElapsedEventArgs e)
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
