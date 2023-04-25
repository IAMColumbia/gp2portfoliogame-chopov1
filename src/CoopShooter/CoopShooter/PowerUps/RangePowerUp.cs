using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopShooter.PowerUps
{
    public class RangePowerUp : PowerUp
    {
        int upgradeAmnt;
        public RangePowerUp(Game game, Spawner s, Camera camera, PlayerManager pm) : base(game, s, camera, pm)
        {
            upgradeAmnt = 50;
        }

        protected override void activatePowerup(Player p)
        {
            DynamicTextSpawner.instance.ActivateText("Range +" + upgradeAmnt.ToString(), p.Position);
            p.UpgradeRange(upgradeAmnt);
            p.UpgradeReloadSpeed(upgradeAmnt);
        }

    }
}
