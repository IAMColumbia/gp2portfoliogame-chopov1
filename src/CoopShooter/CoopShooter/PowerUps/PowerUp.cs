using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopShooter.PowerUps
{
    public class PowerUp : MovingSprite
    {
        public PowerUp(Game game, Spawner s, Camera camera, PlayerManager pm) : base(game, new AnimationData("PowerUpShit", 2, 0.5f), camera, s, pm)
        {
            minSpeed = 8; maxSpeed = 15;
        }

        protected override void onCollision(CollisionObj obj)
        {
            switch (obj.tag)
            {
                case CollisionTag.Player:
                    //add powerup to player
                    player = (Player)CollisionManager.instance.GetCollidable(obj.id);
                    activatePowerup(player);
                    State = SpriteState.dead;
                    break;
                default: break;
            }
        }

        protected Player player;

        protected virtual void activatePowerup(Player p)
        {
            DynamicTextSpawner.instance.ActivateText("Gun +1", p.Position);
            p.AddGun();
        }
    }
}
