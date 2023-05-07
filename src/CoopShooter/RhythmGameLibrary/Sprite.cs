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
using CoopShooter;

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

        public Color[] SpriteTextureData;
        protected Texture2D spriteTexture;
        public string TextureName;
        protected float scale;
        public float Rotation { get; protected set; }
        protected float transparency;
        protected Vector2 origin;

        protected Camera camera;

        protected Texture2D spriteMarker;
        protected bool showMarkers;

        public Matrix Transform => transform;
        protected Matrix transform;

        public virtual int TextureWidth { get { return spriteTexture.Width; } }
        public int TextureHeight { get { return spriteTexture.Height; } }

        
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

      
        protected void setSpriteData()
        {
            this.SpriteTextureData =
                    new Color[this.spriteTexture.Width * this.spriteTexture.Height];
            this.spriteTexture.GetData(this.SpriteTextureData);
        }
        protected virtual void IncreaseScale(float increase)
        {
            scale += increase;
            Rect.Width = (int)(spriteTexture.Width * this.scale);
            Rect.Height = (int)(spriteTexture.Height * this.scale);
        }

        public virtual void SetScale(float scale)
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
            setSpriteData();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            updateRotation();
            updateTransformMatrix();
            updateRectangle();
            
        }

        protected virtual void updateRectangle()
        {
            Vector2 topLeft = Vector2.Transform(Vector2.Zero, transform);
            Vector2 topRight = Vector2.Transform(new Vector2(spriteTexture.Width, 0), transform);
            Vector2 bottomLeft = Vector2.Transform(new Vector2(0, spriteTexture.Height), transform);
            Vector2 bottomRight = Vector2.Transform(new Vector2(spriteTexture.Width, spriteTexture.Height), transform);

            Vector2 min = new Vector2(MathExtension.Min(topLeft.X, topRight.X, bottomLeft.X, bottomRight.X), MathExtension.Min(topLeft.Y, topRight.Y, bottomRight.Y, bottomLeft.Y));
            Vector2 max = new Vector2(MathExtension.Max(topLeft.X, topRight.X, bottomLeft.X, bottomRight.X), MathExtension.Max(topLeft.Y, topRight.Y, bottomRight.Y, bottomLeft.Y));

            Rect = new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y-min.Y));
        }

        void updateTransformMatrix()
        {
            //SRT Reverse Origin * Scale * rotation * translate
            transform =  Matrix.CreateTranslation(new Vector3((-origin), 0)) * 
                         Matrix.CreateScale(scale) * 
                         Matrix.CreateRotationZ(Rotation) * 
                         Matrix.CreateTranslation(new Vector3(Position, 0));
        }
        protected virtual void updateRotation()
        {
            Rotation = (float)Math.Atan2((double)Direction.Y, (double)Direction.X);
        }

        protected Vector2 getDirectionFromRotation()
        {
            return new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));
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
                Position,
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
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, camera.Transform);
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
