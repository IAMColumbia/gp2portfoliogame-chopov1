using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoopShooter
{
    public class ProjectileSpawner : Spawner
    {
        protected Player player;
        public int rotMod;
        //create a timer that needs to finish before player can fire again. check if this is complete before spawning an object(wont need this if take rhythm approach)
        Timer reloadTimer;
        public ProjectileSpawner(Game game, Player p, Camera c,int numberOfObjects, int rotMod) : base(game, c, numberOfObjects)
        {
            player = p;
            this.rotMod = rotMod;
        }

        public void DestroyedEnemy()
        {
            player.AddScore();
        }

        public override Sprite createSpawnableObject()
        {
            return new Projectile(Game, "Pellet", camera, this);
        }

        Vector2 getDirMod(int rotMod)
        {
            if(rotMod <= 0)
            {
                return player.ShootDir;
            }
            return new Vector2(
                (float)(player.ShootDir.X * Math.Cos(rotMod) - player.ShootDir.Y * Math.Sin(rotMod)), 
                (float)(player.ShootDir.X * Math.Sin(rotMod) + player.ShootDir.Y * Math.Cos(rotMod)));
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
                    objToSpawn.Direction = getDirMod(rotMod);
                    objToSpawn.Enabled = true;
                    objToSpawn.Visible = true;
                    return objToSpawn;
                }
            }
            return null;
        }
    }
}
