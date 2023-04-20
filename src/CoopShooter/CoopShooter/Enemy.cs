using CoopShooter;
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
    public class Enemy : MovingSprite
    {
        int health;
        float speed;

        public Enemy(Game game, EnemySpawner s,Camera c, PlayerManager pm) : base (game, new AnimationData("AnimationTest", 3, 6), c, s, pm){
            colInfo.tag = CollisionTag.Enemy;
            setPosition(-200, -200);
            speed = 1f;
            Direction = new Vector2(1,0);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void onCollision(CollisionObj obj)
        {
            base.onCollision(obj);
            switch(obj.tag)
            {
                case CollisionTag.Enemy:
                    //collideWithSimilarType(obj);
                    break;
                case CollisionTag.Projectile:
                    State = SpriteState.dead;
                    break;

            }
        }

    }
}
