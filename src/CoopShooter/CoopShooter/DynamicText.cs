using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopShooter
{
    public class DynamicText : Sprite
    {
        string Text;
        SpriteFont font;
        float decaySpeed;
        Spawner spawner;
        public DynamicText(Game game, Camera camera, Spawner s) : base(game, "Transparent", camera)
        {
            spawner = s;
            decaySpeed = 0.02f;
            Text = "";
        }

        protected override void LoadContent()
        {
            font = Game.Content.Load<SpriteFont>("ScoreFont");
            base.LoadContent();
        }

        public void Activate(string text)
        {
            Text = text;
            transparency = 1;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (transparency >= 0)
            {
                transparency -= decaySpeed;
            }
            if(transparency < 0 && Enabled) {
                spawner.DeSpawn(this);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, camera.Transform);
            spriteBatch.DrawString(font, Text, Position, Color.White * transparency);
            spriteBatch.End();
        }
    }
}
