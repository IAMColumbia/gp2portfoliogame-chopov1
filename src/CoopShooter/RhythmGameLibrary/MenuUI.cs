using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameLibrary
{
    public class MenuUI : DrawableGameComponent, ISceneComponenet
    {
    
        protected SpriteFont BasicFont;
        protected SpriteFont ScoreFont;
        protected SpriteBatch spriteBatch;
        

        protected int Xcenter;
        protected int Ycenter;

        public MenuUI(Game game) : base(game)
        {
           game.Components.Add(this);   
        }

        protected override void LoadContent()
        {
            Xcenter = GraphicsDevice.Viewport.Width / 2;
            Ycenter = GraphicsDevice.Viewport.Height / 2;
            base.LoadContent();
            BasicFont = Game.Content.Load<SpriteFont>("BasicFont");
            ScoreFont = Game.Content.Load<SpriteFont>("ScoreFont");
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
        }

        protected void DrawCustomString(SpriteFont font, string s, int Y, Color color)
        {
            spriteBatch.DrawString(font, s, new Vector2(centerText(s, font).X, Y),color);
        }

        protected void DrawCustomStringBacked(SpriteFont font, SpriteFont backFont,string s, int Y, Color backColor, Color color )
        {
            spriteBatch.DrawString(backFont, s, new Vector2(centerText(s, font).X, Y), backColor);
            spriteBatch.DrawString(font, s, new Vector2(centerText(s, font).X, Y), color);
        }

        


        protected Vector2 centerText(string s, SpriteFont font)
        {
            float x = font.MeasureString(s).X / 2;
            float y = font.MeasureString(s).Y / 2;
            return new Vector2(Xcenter - x, Ycenter - y);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.Begin();
            drawUI();
            spriteBatch.End();
        }

        protected virtual void drawUI()
        {

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
