using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopShooter
{
    public class AnimatableSprite : CollidableSprite
    {
        Animation animation;
        int frames;
        float frameTime;
        public AnimatableSprite(Game game, string texturename, int frames, float frameTime, Camera camera) : base(game, texturename, camera)
        {
            this.frames = frames;
            this.frameTime = frameTime;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            animation = new Animation(spriteTexture, frames, frameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            animation.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, camera.Transform);
            animation.Draw(Position, spriteBatch, transparency, scale, Rotation);
            spriteBatch.End();
        }

    }
}
