using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopShooter
{
    public class PowerUp : MovingSprite
    {
        public PowerUp(Game game, Spawner s, Camera camera, PlayerManager pm) : base(game, new AnimationData("Laser", 1, 1), camera, s, pm)
        {

        }

        protected override void onCollision(CollisionObj obj)
        {
            switch (obj.tag)
            {
                case CollisionTag.Player:
                    //add powerup to player
                        upgradePlayer(obj.id);
                        State = SpriteState.dead;
                    break;
                default: break;
            }
        }

        Player p;

        void upgradePlayer(int id)
        {
            p = ((Player)CollisionManager.instance.GetCollidable(id));
            p.AddGun();
        }
    }
}
