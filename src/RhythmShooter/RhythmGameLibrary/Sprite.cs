using CSCore.XAudio2;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace RhythmGameLibrary
{
    public class Sprite : DrawableGameComponent, ISceneComponenet
    {
        public Vector2 Position, Direction;
        public Rectangle Rect;

        protected SpriteBatch spriteBatch;
        SpriteEffects effect;

        protected Texture2D spriteTexture;
        public string TextureName;
        protected float scale;
        public float Rotation { get; protected set; }
        protected float transparency;
        protected float rotationVelocity;
        protected Vector2 origin;

        protected Camera camera;
        public Sprite(Game game, string texturename, Camera camera) : base(game)
        {
            this.camera = camera;
            Game.Components.Add(this);
            TextureName = texturename;
            scale = 1.8f;
            Rotation = 0;
            rotationVelocity = 0.1f;
            transparency = 1;
            Position = new Vector2(100, 100);
        }

        public void SetRotation(float rotation)
        {
            Rotation= rotation;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            spriteTexture = Game.Content.Load<Texture2D>(TextureName);
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            effect = SpriteEffects.None;
            origin = new Vector2(this.spriteTexture.Width / 2, this.spriteTexture.Height / 2);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            updateRectangeForDrawing();
            updateRotation();
        }

        protected virtual void updateRotation()
        {
            Rotation = (float)Math.Atan2((double)Direction.Y, (double)Direction.X);
        }

        protected Vector2 getDirectionFromRotation()
        {
            return new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));
        }

        private void updateRectangeForDrawing()
        {
            Rect.X = (int)Position.X;
            Rect.Y = (int)Position.Y;
            Rect.Width = (int)(spriteTexture.Width * this.scale);
            Rect.Height = (int)(spriteTexture.Height * this.scale);
        }

        #region old Attempt at rotation
        /*private void updateRotation()
        {
            if (Direction.X < 0)
            {
                setRotationValue(MathHelper.ToRadians(180));
            }
            if( Direction.X > 0)
            {
                setRotationValue(MathHelper.ToRadians(360));
            }
            if(Direction.Y < 0)
            {
                setRotationValue(MathHelper.ToRadians(270));
            }
            if(Direction.Y > 0)
            {
                setRotationValue(MathHelper.ToRadians(90));
            }
            
        }*/
        float getAngle(float s, float e)
        {
            float d = e -s;
            float dp = d / 360;
            float n = MathF.Round(dp);
            float dx = dp - n;
            d = 360 * dx;
            return d;
        }
        private bool rotateClockwise(float target)
        {
            clockwiseDistance = (Rotation - target + 360) % 360;
            counterclockwiseDistance = (target - Rotation + 360) % 360;

            if (Math.Abs(clockwiseDistance) < Math.Abs(counterclockwiseDistance))
            {
                return true;
            }
            return false;
        }
        float clockwiseDistance;
        float counterclockwiseDistance;

        private void setRotationValue(float target)
        {
            if (rotateClockwise(target))
            {
                if (Rotation != Rotation - clockwiseDistance)
                {
                    Rotation -= MathHelper.ToRadians(rotationVelocity);
                }
            }
            else
            {
                if (Rotation != Rotation - counterclockwiseDistance)
                {
                    Rotation += MathHelper.ToRadians(rotationVelocity);
                }
            }
        }

        #endregion
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred,null, SamplerState.PointClamp,null, null, null, camera.Transform);
            spriteBatch.Draw(spriteTexture,
                new Vector2(Rect.X, Rect.Y),
                null,
                Color.White * transparency,
                Rotation,
                origin, 
                scale,
                effect,
                0);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void Load()
        {
            Enabled= true;
            Visible= true;
        }

        public void UnLoad()
        {
            Enabled= false;
            Visible= false;
        }
    }
}
