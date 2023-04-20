using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopShooter
{
    public class MovingSprite : AnimatableSprite
    {
        int health;
        float speed;
        int minSpeed, maxSpeed;
        Vector2 target;
        Spawner spawner;
        PlayerManager pm;
        Random rnd;
        public MovingSprite(Game game, AnimationData anim, Camera camera, Spawner s, PlayerManager pm) : base(game,anim, camera)
        {
            minSpeed = 3;
            maxSpeed = 9;
            spawner = s;
            this.pm = pm;
            setPosition(-200, -200);
            speed = 1f;
            Direction = new Vector2(1, 0);
            rnd = new Random();
        }

        public void SetTarget(Vector2 t)
        {
            target = t;
            setDirection();
            speed = rnd.Next(minSpeed, maxSpeed) * 0.1f;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            moveSprite(gameTime);
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
            switch (obj.tag)
            {
                default: break;
            }
        }


        public void moveSprite(GameTime gameTime)
        {
            Position += Direction * speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        protected void setDirection()
        {
            Direction = Vector2.Normalize(target - Position);
        }

        protected bool isOffScreen()
        {
            if (Position.Y > Game.GraphicsDevice.Viewport.Height + 100 || Position.Y < -100 || Position.X > Game.GraphicsDevice.Viewport.Width + 100 || Position.X < -100)
            {
                return true;
            }
            return false;
        }
    }
}
