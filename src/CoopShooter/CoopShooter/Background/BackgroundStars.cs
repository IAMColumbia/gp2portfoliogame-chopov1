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
    public class BackgroundStars : DrawableGameComponent
    {
        Texture2D star;
        List<BgStar> stars;
        Camera camera;
        SpriteBatch sb;

        List<BgPlanet> planets;


        public BackgroundStars(Game game, Camera camera) : base(game)
        {
            Game.Components.Add(this);
            this.camera = camera;
            stars = new List<BgStar>();
            planets = new List<BgPlanet>();
            setupStars();
            setupPlanets();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            star = Game.Content.Load<Texture2D>("BackgroundStar");
            sb = new SpriteBatch(Game.GraphicsDevice);
        }
        void setupStars()
        {
            stars = new List<BgStar>();
            for (int i = 0; i < 10; i++)
            {
                stars.Add(new BgStar(Game, "BackgroundStar", camera));
            }
        }

        void setupPlanets()
        {
            planets.Add(new BgPlanet(Game, camera));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            sb.Begin();
            foreach (BgStar s in stars)
            {
                sb.Draw(s.Texture, s.Position, Color.White * s.Transparency);
            }
            //rocket.DrawAnim(sb);
            sb.End();
        }


    }
}
