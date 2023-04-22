using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopShooter.PowerUps
{
    public class ShieldPowerUpSpawner : PowerUpSpawner
    {
        public ShieldPowerUpSpawner(Game game, Camera c, int numberOfObjects, PlayerManager pm) : base(game, c, numberOfObjects, pm)
        {

        }

        public override Sprite createSpawnableObject()
        {
            return new ShieldPowerUp(Game, this, camera, pm);
        }
    }
}
