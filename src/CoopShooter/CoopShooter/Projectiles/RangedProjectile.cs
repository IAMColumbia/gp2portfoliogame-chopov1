using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopShooter
{
    public class RangedProjectile : Projectile
    {
        int maxDistFromPlayer;
        Player player;

        public RangedProjectile(Game game, string texturename, Camera camera, Spawner s, Player p) : base(game, texturename, camera, s)
        {
            maxDistFromPlayer = 500;
            player = p;
        }

        public override void Update(GameTime gameTime)
        {
            if (isToFarFromPlayer())
            {
                State = SpriteState.dead;
            }
            base.Update(gameTime);
        }

        protected bool isToFarFromPlayer()
        {
            if (Vector2.Distance(player.Position, this.Position) > maxDistFromPlayer)
            {
                return true;
            }
                return false;
        }
    }
}
