using CSCore.XAudio2;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml;

namespace RhythmGameLibrary
{
    public enum SpriteState { alive, dead, inactive, active }

    public class Sprite : DrawableGameComponent, ISceneComponenet
    {
        public SpriteState State { get; set; }

        public Vector2 Position, Direction;
        public Rectangle Rect;

        protected SpriteBatch spriteBatch;
        protected SpriteEffects effect;

        protected Texture2D spriteTexture;
        public string TextureName;
        protected float scale;
        public float Rotation { get; protected set; }
        protected float transparency;
        protected Vector2 origin;

        protected Camera camera;

        protected Texture2D spriteMarker;
        protected bool showMarkers;
        public Sprite(Game game, string texturename, Camera camera) : base(game)
        {
            Game.Components.Add(this);
            showMarkers = false;
            this.camera = camera;
            TextureName = texturename;
            scale = 1.8f;
            Rotation = 0;
            transparency = 1;
        }

        protected virtual void IncreaseScale(float increase)
        {
            scale += increase;
            Rect.Width = (int)(spriteTexture.Width * this.scale);
            Rect.Height = (int)(spriteTexture.Height * this.scale);
        }

        protected virtual void SetScale(float scale)
        {
            this.scale = scale;
            Rect.Width = (int)(spriteTexture.Width * this.scale);
            Rect.Height = (int)(spriteTexture.Height * this.scale);
        }

        public void SetRotation(float rotation)
        {
            Rotation = rotation;
        }

        protected Vector2 rotateVec(float rotMod, Vector2 vec)
        {
            if (rotMod <= 0)
            {
                return vec;
            }
            return new Vector2(
            (float)(vec.X * Math.Cos(rotMod) - vec.Y * Math.Sin(rotMod)),
            (float)(vec.X * Math.Sin(rotMod) + vec.Y * Math.Cos(rotMod)));
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            spriteMarker = Game.Content.Load<Texture2D>("SpriteMarker4x4");
            spriteTexture = Game.Content.Load<Texture2D>(TextureName);
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            effect = SpriteEffects.None;
            origin = new Vector2(this.spriteTexture.Width / 2, this.spriteTexture.Height / 2);
            Rect.Width = (int)(spriteTexture.Width * this.scale);
            Rect.Height = (int)(spriteTexture.Height * this.scale);
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
        }

        public void SetPosition(float x, float y)
        {
            Position = new Vector2(x, y);
            Rect.X = (int)Position.X;
            Rect.Y = (int)Position.Y;
        }

        public override void Draw(GameTime gameTime)
        {
            if(camera == null) { Console.WriteLine("Camera is null"); return; }
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, camera.Transform);
            if (showMarkers)
            {
                drawDebugMarkers(spriteBatch);
            }
            spriteBatch.Draw(spriteTexture,
                new Vector2(Rect.X + spriteTexture.Width, Rect.Y + spriteTexture.Height),
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

        protected void DrawLayer(Texture2D texture, Vector2 pos, Rectangle? srcrect, Color color, float rot, Vector2 orig, float scale)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture,
                pos,
                srcrect,
                color,
                rot,
                orig,
                scale,
                effect,
                0);
            spriteBatch.End();
        }

        protected void drawDebugMarkers(SpriteBatch sb)
        {
            sb.Draw(spriteMarker, new Vector2(Rect.Left, Rect.Top), Color.White);
            sb.Draw(spriteMarker, new Vector2(Rect.Right,Rect.Top), Color.White);
            sb.Draw(spriteMarker, new Vector2(Rect.Left, Rect.Bottom), Color.White);
            sb.Draw(spriteMarker, new Vector2(Rect.Right, Rect.Bottom), Color.White);

        }

        public virtual void Load()
        {
            Enabled = true;
            Visible = true;
        }

        public virtual void UnLoad()
        {
            Enabled = false;
            Visible = false;
        }
    }
}
