using CoopShooter;
using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmShooter
{
    public class Enemy : CollidableSprite
    {
        int health;
        float speed;
        Vector2 target;

        EnemySpawner spawner;
        PlayerManager pm;
        public Enemy(Game game, EnemySpawner s,Camera c, PlayerManager pm) : base (game, "TestEnemy", c){
            colInfo.tag = CollisionTag.Enemy;
            spawner = s;
            this.pm = pm;
            setPosition(-200, -200);
            speed = 0.001f;
            Direction = new Vector2(1,0);
        }

        public void setTarget(Vector2 t)
        {
            target= t;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            setDirection();


            moveEnemy(gameTime);
            setTarget(pm.GetEnemyTargetPos());
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
                    spawner.DeSpawn(this);
                    break;

            }
        }


        public void moveEnemy(GameTime gameTime)
        {
            Position += velocity* speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            //Position = target;
        }

        void setDirection()
        {
            if (!isOverlappingSameType())
            {
                velocity = target - Position;
            }
        }

    }
}
