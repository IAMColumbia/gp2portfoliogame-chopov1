using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopShooter.Enemies
{
    public class Enemy : MovingSprite
    {
        int health;
        float speed;
        SpawnIndicator indicator;
        public Enemy(Game game, EnemySpawner s, Camera c, PlayerManager pm) : base(game, new AnimationData("EnemyAnim2-Sheet", 2, 0.2f), c, s, pm)
        {
            colInfo.tag = CollisionTag.Enemy;
            SetPosition(-200, -200);
            speed = 1f;
            Direction = new Vector2(1, 0);
            indicator = new SpawnIndicator(Game, camera, this);
        }

        public void Activate()
        {
            indicator.Activate();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void onCollision(CollisionObj obj)
        {
            base.onCollision(obj);
            switch (obj.tag)
            {
                case CollisionTag.Enemy:
                    //collideWithSimilarType(obj);
                    break;
                case CollisionTag.Projectile:
                    State = SpriteState.dead;
                    break;
                case CollisionTag.Player:
                    State = SpriteState.dead;
                    break;

            }
        }

    }
}
