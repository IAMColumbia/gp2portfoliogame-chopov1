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
    public class Enemy : AnimatableSprite
    {
        int health;
        float speed;
        Vector2 target;

        EnemySpawner spawner;
        PlayerManager pm;

        Random rnd;

        public Enemy(Game game, EnemySpawner s,Camera c, PlayerManager pm) : base (game, "AnimationTest", 3, 6, c){
            colInfo.tag = CollisionTag.Enemy;
            spawner = s;
            this.pm = pm;
            setPosition(-200, -200);
            speed = 1f;
            Direction = new Vector2(1,0);
            rnd = new Random();
        }

        public void setTarget(Vector2 t)
        {
            target= t;
            setDirection();
            speed = rnd.Next(3, 9) * 0.1f;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
           
            moveEnemy(gameTime);

            //setDirection();
            //setTarget(pm.GetEnemyTargetPos());
        }

        protected override void StateBasedUpdate()
        {
            base.StateBasedUpdate();
            switch (State)
            {
                case SpriteState.alive:
                    if (isOffScreen())
                    {
                        State = SpriteState.dead;
                    }
                    break;
                case SpriteState.dead:
                    spawner.DeSpawn(this);
                    break;
            }
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


        public void moveEnemy(GameTime gameTime)
        {
            Position += Direction * speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            //Position = target;
        }

        void setDirection()
        {
            //was for when enemies would be able to collide
            /*if (!isOverlappingSameType())
            {
                velocity = target - Position;
            }*/
            Direction = Vector2.Normalize(target - Position);
        }

        bool isOffScreen()
        {
            if(Position.Y > Game.GraphicsDevice.Viewport.Height + 100 || Position.Y < -100 || Position.X > Game.GraphicsDevice.Viewport.Width + 100 || Position.X < -100)
            {
                return true;
            }
            return false;
        }

    }
}
