using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopShooter
{
    public class ProjectileDecorator
    {
        ProjectileSpawner gun;
        
        public ProjectileDecorator(Game game, Camera c, int numberOfObjects, Player p, int rotMod)
        {
            gun = new ProjectileSpawner(game, c, numberOfObjects, p, rotMod);
        }

        public void Shoot(Vector2 pos)
        {
            gun.SpawnObject(pos);
        }

        public void Reset()
        {
            gun.ResetObjects();
        }

    }
}
