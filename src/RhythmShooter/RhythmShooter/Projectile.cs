using CSCore.XAudio2;
using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmShooter
{
    public class Projectile : Sprite
    {
        float speed;
        Spawner spawner;
        public Projectile(Game game, string texturename, Camera camera, Spawner s) : base(game, texturename, camera)
        {
            this.spawner = s;
            speed = 2f;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            move(gameTime);
            if (isOutOfBounds())
            {
                spawner.DeSpawn(this);
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
