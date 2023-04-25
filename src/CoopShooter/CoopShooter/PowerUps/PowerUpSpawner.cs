using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopShooter.PowerUps
{
    public class PowerUpSpawner : Spawner
    {
        protected PlayerManager pm;
        public PowerUpSpawner(Game game, Camera c, int numberOfObjects, PlayerManager pm) : base(game, c, numberOfObjects)
        {
            this.pm = pm;
        }

        public override Sprite createSpawnableObject()
        {
            return new PowerUp(Game, this, camera, pm);
        }

        public override Sprite SpawnObject(Vector2 pos)
        {
            if (objects.Count > 0)
            {
                if (objects.Peek().Enabled == false)
                {
                    Sprite objToSpawn = objects.Dequeue();
                    objects.Enqueue(objToSpawn);
                    objToSpawn.SetPosition(pos.X, pos.Y);
                    objToSpawn.Enabled = true;
                    objToSpawn.Visible = true;
                    ((PowerUp)objToSpawn).SetTarget(pm.GetMidpoint());
                    objToSpawn.State = SpriteState.alive;
                    return objToSpawn;
                }
            }
            return null;
        }


    }
}
