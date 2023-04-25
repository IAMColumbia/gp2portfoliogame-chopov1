using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RhythmGameLibrary;

namespace CoopShooter
{
    internal class BgPlanet : AnimatableSprite
    {
        enum CountState { up, down };
        CountState state;
        float fadeSpeed;
        public Texture2D Texture { get { return spriteTexture; } }
        Random rnd;
        public BgPlanet(Game game, Camera camera) : base(game, new AnimationData("BluePlanetSheet", 6, 0.2f), camera)
        {
            rnd = new Random();
            transparency = rnd.Next(100) * 0.01f;
            state = CountState.up;
            fadeSpeed = 0.001f * rnd.Next(1, 5);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (transparency <= 0)
            {
                state = CountState.up;
                getRndPos(this);
            }
            if (transparency >= 1)
            {
                state = CountState.down;
            }
            switch (state)
            {
                case CountState.up:
                    transparency += fadeSpeed;
                    break;
                case CountState.down:
                    transparency -= fadeSpeed;
                    break;
            }
        }

        private void getRndPos(Sprite s)
        {
            s.Position = new Vector2(rnd.Next(Game.GraphicsDevice.Viewport.Width), rnd.Next(Game.GraphicsDevice.Viewport.Height));
        }

    }
}
