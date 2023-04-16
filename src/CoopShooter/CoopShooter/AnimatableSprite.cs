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
            origin = new Vector2((this.spriteTexture.Width / frames)/2, this.spriteTexture.Height / 2);
            Rect.Width = (int)((spriteTexture.Width/frames) * this.scale);
            Rect.Height = (int)(spriteTexture.Height * this.scale);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            animation.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, camera.Transform);
            if (showMarkers)
            {
                drawDebugMarkers(spriteBatch);
            }
            animation.Draw(new Vector2(Rect.X + (spriteTexture.Width/3), Rect.Y + (spriteTexture.Height)), spriteBatch, transparency, scale, Rotation, origin);
            spriteBatch.End();
        }

    }
}
