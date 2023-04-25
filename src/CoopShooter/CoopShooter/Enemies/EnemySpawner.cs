using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopShooter.Enemies
{
    public class EnemySpawner : Spawner
    {
        Camera c;
        PlayerManager pm;
        public EnemySpawner(Game game, PlayerManager pm, Camera c, int numberOfObjects) : base(game, c, numberOfObjects)
        {
            this.pm = pm;
            this.c = c;
        }

        public override Sprite createSpawnableObject()
        {
            return new Enemy(Game, this, c, pm);
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
                    objToSpawn.Visible = true;
                    ((Enemy)objToSpawn).SetTarget(pm.GetMidpoint());
                    ((Enemy)objToSpawn).Activate();
                    objToSpawn.State = SpriteState.alive;
                    objToSpawn.Enabled = true;
                    return objToSpawn;
                }
            }
            return null;
        }


    }
}
