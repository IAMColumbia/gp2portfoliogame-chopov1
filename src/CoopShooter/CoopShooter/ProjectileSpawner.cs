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
    public class ProjectileSpawner : Spawner
    {
        protected Player player;
        public int rotMod;
        Timer reloadTimer;
        float reloadTime;
        public ProjectileSpawner(Game game, Camera c,int numberOfObjects, Player p, int rotMod) : base(game, c, numberOfObjects)
        {
            this.rotMod = rotMod;
            player = p;
            reloadTime = 1000;
            reloadTimer = new Timer(reloadTime);
            reloadTimer.Elapsed += ReloadComplete;
            canShoot = true;
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

        bool canShoot;

        void ReloadComplete(Object source, ElapsedEventArgs e)
        {
            canShoot = true;
        }
        public override Sprite SpawnObject(Vector2 pos)
        {
            if (!canShoot)
            {
                return null;
            }
            canShoot = false;
            reloadTimer.Start();
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
