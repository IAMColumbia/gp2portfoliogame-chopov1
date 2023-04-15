using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmShooter
{
    internal class ProjectileSpawner : Spawner
    {
        protected Player player;
        public ProjectileSpawner(Game game, Player p, Camera c,int numberOfObjects) : base(game, c, numberOfObjects)
        {
            player = p;
        }

        public void DestroyedEnemy()
        {
            player.AddScore();
        }

        public override Sprite createSpawnableObject()
        {
            return new Projectile(Game, "Pellet", camera, this);
        }

        public override Sprite SpawnObject(Vector2 pos)
        {
            if (objects.Count > 0)
            {
                if (objects.Peek().Enabled == false)
                {
                    Sprite objToSpawn = objects.Dequeue();
                    objects.Enqueue(objToSpawn);
                    objToSpawn.Position = pos;
                    objToSpawn.SetRotation(player.Rotation + MathHelper.ToRadians(90));
                    objToSpawn.Direction = player.ShootDir;
                    objToSpawn.Enabled = true;
                    objToSpawn.Visible = true;
                    return objToSpawn;
                }
            }
            return null;
        }
    }
}
