using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace CoopShooter
{
    public class PlayerManager : GameComponent
    {
        public Player p1 { get; protected set; }
        public Player p2 { get; protected set; }
        public int TotalKills { get { return p1.Kills + p2.Kills; } }

        public int TotalLevel { get { return p1.Level + p2.Level; } }

        public bool ResetGame { get { return p1.State == SpriteState.dead || p2.State == SpriteState.dead; } set { } }
        public PlayerManager(Game game, Camera camera) : base(game) {
            Game.Components.Add(this);
            p1 = new Player(game, 0, "TestShip2", 1, 1, camera);
            p2 = new Player(game, 1, "TestShip1", 1, 2, camera);
            p2.Position = p1.Position + new Vector2(200, 200);
        }

        public void ResetPlayers()
        {
            p1.ResetPlayer(new Vector2(Game.GraphicsDevice.Viewport.Width + (Game.GraphicsDevice.Viewport.Width /4), Game.GraphicsDevice.Viewport.Height/2));
            p2.ResetPlayer(new Vector2(Game.GraphicsDevice.Viewport.Width - (Game.GraphicsDevice.Viewport.Width / 4), Game.GraphicsDevice.Viewport.Height / 2));
        }

        public override void Update(GameTime gameTime)
        {
            p1.SetRotation(p2.Position);
            p2.SetRotation(p1.Position);
            base.Update(gameTime);
        }

        public Vector2 GetMidpoint()
        {
            return new Vector2((p1.Position.X + p2.Position.X)/2, (p1.Position.Y + p2.Position.Y )/2);
        }

      
    }
}
