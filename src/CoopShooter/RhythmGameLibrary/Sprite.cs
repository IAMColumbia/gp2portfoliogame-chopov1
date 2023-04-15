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
    public enum SpriteState { alive, dead, inactive }

    public class Sprite : DrawableGameComponent, ISceneComponenet
    {
        public SpriteState State { get; set; }

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

        protected Texture2D spriteMarker;
        bool showMarkers;
        public Sprite(Game game, string texturename, Camera camera) : base(game)
        {
            showMarkers = false;
            this.camera = camera;
            Game.Components.Add(this);
            TextureName = texturename;
            scale = 1.8f;
            Rotation = 0;
            rotationVelocity = 0.1f;
            transparency = 1;
        }

        public void SetRotation(float rotation)
        {
            Rotation = rotation;
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

        protected void setPosition(float x, float y)
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

        void drawDebugMarkers(SpriteBatch sb)
        {
            sb.Draw(spriteMarker, new Vector2(Rect.Left, Rect.Top), Color.White);
            sb.Draw(spriteMarker, new Vector2(Rect.Right,Rect.Top), Color.White);
            sb.Draw(spriteMarker, new Vector2(Rect.Left, Rect.Bottom), Color.White);
            sb.Draw(spriteMarker, new Vector2(Rect.Right, Rect.Bottom), Color.White);

        }

        public void Load()
        {
            Enabled = true;
            Visible = true;
        }

        public void UnLoad()
        {
            Enabled = false;
            Visible = false;
        }
    }
}
