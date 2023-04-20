using CoopShooter;
using CSCore.XAudio2;
using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;

namespace CoopShooter
{
    public class Projectile : CollidableSprite
    {
        float speed;
        Spawner spawner;
        public Projectile(Game game, string texturename, Camera camera, Spawner s) : base(game, texturename, camera)
        {
            setPosition(-800, -800);
            colInfo.tag = CollisionTag.Projectile;
            this.spawner = s;
            speed = 2f;
        }

       

        public override void Update(GameTime gameTime)
        {
            move(gameTime);
            if (isOutOfBounds())
            {
                State = SpriteState.dead;
            }
            base.Update(gameTime);
        }

        protected override void StateBasedUpdate()
        {
            switch (State)
            {
                case SpriteState.dead:
                    spawner.DeSpawn(this);
                    break;
                case SpriteState.alive:
                    break;
            }
        }

        protected override void onCollision(CollisionObj obj)
        {
            base.onCollision(obj);
            switch (obj.tag)
            {
                case CollisionTag.Enemy:
                    ((ProjectileSpawner)spawner).DestroyedEnemy();
                    spawner.DeSpawn(this);
                    break;
            }
        }

        protected void move(GameTime gameTime)
        {
            Position += Direction * speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        //change this to be rhtyhm based later
        protected bool isOutOfBounds()
        {
            if(this.Position.X < -100 || Position.X > GraphicsDevice.Viewport.Width + 100|| Position.Y < -100 || Position.Y > GraphicsDevice.Viewport.Height + 100)
            {
                return true;
            }
            return false;
        }

        
    }
}
