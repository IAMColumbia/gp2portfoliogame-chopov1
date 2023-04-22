using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopShooter.PowerUps
{
    public class ShieldPowerUp : PowerUp
    {
        public ShieldPowerUp(Game game, Spawner s, Camera camera, PlayerManager pm) : base(game, s, camera, pm)
        {

        }

        protected override void activatePowerup(Player p)
        {
            DynamicTextSpawner.instance.ActivateText("Shield Activated", p.Position);
            p.AddShield();
        }
    }
}
