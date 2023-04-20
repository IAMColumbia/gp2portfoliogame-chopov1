using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace CoopShooter
{
    public class PowerUpManager : MovingSpriteManager
    {
        //should be responsible for spawning power ups, as well as checking how many powerups a player has so as not to spawn powerups if players are at their max
        //need to encapsulate decorators within a collidable sprite
        //should also probobly have a spawner to spawn the encapsulated objs, we can call them powerupboxes or something
        //needs to check collisions, of what player collided with it, then it can pass in a player arg
        public PowerUpManager(Game game, Camera c, PlayerManager pm) : base(game, c, pm, 1000 )
        {
            spawner = new PowerUpSpawner(game, c, 1, pm);
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
    }
}
