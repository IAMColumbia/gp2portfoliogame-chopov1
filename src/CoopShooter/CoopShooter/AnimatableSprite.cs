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
    public struct AnimationData
    {
        public float frameTime;
        public int frames;
        public string texturename;

        public AnimationData(string texturename, int frames, float frameTime )
        {
            this.frames = frames;
            this.texturename = texturename;
            this.frameTime = frameTime;
        }
    }
    public class AnimatableSprite : CollidableSprite
    {
        protected Animation animation;
        protected int frames;
        float frameTime;

        public override int TextureWidth { get { return spriteTexture.Width / frames; } }

        public AnimatableSprite(Game game, AnimationData anim, Camera camera) : base(game, anim.texturename, camera)
        {
            frames = anim.frames;
            frameTime = anim.frameTime;
            
        }

        protected override void IncreaseScale(float increase)
        {
            scale += increase;
            Rect.Width = (int)(((spriteTexture.Width/frames)) * this.scale);
            Rect.Height = (int)((spriteTexture.Height )* this.scale);
        }

        protected override void SetScale(float scale)
        {
            this.scale = scale;
            Rect.Width = (int)((spriteTexture.Width/frames) * this.scale);
            Rect.Height = (int)(spriteTexture.Height * this.scale);
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

        protected override void updateRectangle()
        {
            Vector2 topLeft = Vector2.Transform(Vector2.Zero, transform);
            Vector2 topRight = Vector2.Transform(new Vector2((spriteTexture.Width/frames), 0), transform);
            Vector2 bottomLeft = Vector2.Transform(new Vector2(0, spriteTexture.Height), transform);
            Vector2 bottomRight = Vector2.Transform(new Vector2((spriteTexture.Width / frames), spriteTexture.Height), transform);

            Vector2 min = new Vector2(MathExtension.Min(topLeft.X, topRight.X, bottomLeft.X, bottomRight.X), MathExtension.Min(topLeft.Y, topRight.Y, bottomRight.Y, bottomLeft.Y));
            Vector2 max = new Vector2(MathExtension.Max(topLeft.X, topRight.X, bottomLeft.X, bottomRight.X), MathExtension.Max(topLeft.Y, topRight.Y, bottomRight.Y, bottomLeft.Y));

            Rect = new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, camera.Transform);
            if (showMarkers)
            {
                drawDebugMarkers(spriteBatch);
            }

            
            animation.Draw(Position, spriteBatch, transparency, scale, Rotation, origin);
            spriteBatch.End();
        }

    }
}
