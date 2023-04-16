using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace RhythmGameLibrary
{
    public class Animation
    {

        private Texture2D texture;
        List<Rectangle> _sourceRectangles;
        int frames;
        int frame;
        float frameTime;
        float frameTimeLeft;
        bool active = true;
        public Animation(Texture2D spriteSheet, int framesX, float frameTime) 
        {
            _sourceRectangles = new List<Rectangle>();
            texture= spriteSheet;
            frames = framesX;
            this.frameTime = frameTime; 
            frameTimeLeft = frameTime;
            var frameWidth = texture.Width/framesX;
            var frameHeight = texture.Height;
            for(int i =0 ; i < frames; i++)
            {
                _sourceRectangles.Add(new Rectangle(i * frameWidth, 0, frameWidth, frameHeight));
            }
        }

        public void Start()
        {
            active = true;
        }

        public void Stop()
        {
            active = false;
        }

        public void Reset()
        {
            frame = 0;
            frameTimeLeft = frameTime;
        }

        public void Update(GameTime gameTime)
        {
            if (!active)
            {
                return;
            }

            frameTimeLeft -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(frameTimeLeft <= 0)
            {
                frameTimeLeft += frameTime;
                frame = (frame +1 ) % frames;
            }
        }

        public void Draw(Vector2 pos, SpriteBatch s, Rectangle Rect)
        {
            s.Draw(texture, pos, _sourceRectangles[frame], Color.White , 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 1);
            
        }

        public void Draw(Vector2 pos, SpriteBatch s, float transparency, float scale, float rotation, Vector2 origin)
        {
            s.Draw(texture, pos, _sourceRectangles[frame], Color.White * transparency, rotation, origin, scale, SpriteEffects.None, 1);
        }


    }
}
