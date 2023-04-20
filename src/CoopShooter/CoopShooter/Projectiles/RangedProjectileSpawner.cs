using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopShooter
{
    internal class RangedProjectileSpawner : ProjectileSpawner
    {
        public RangedProjectileSpawner(Game game, Camera c, int numberOfObjects, Player p, int rotMod) : base(game, c, numberOfObjects, p, rotMod)
        {

        }

        public override Sprite createSpawnableObject()
        {
            return new RangedProjectile(Game, "Pellet", camera, this, player);
        }

    }
}
