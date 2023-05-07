using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Timers;

namespace CoopShooter
{
    public class PlayerManager : GameComponent
    {
        public Player p1 { get; protected set; }
        public Player p2 { get; protected set; }
        public int TotalKills { get { return p1.Kills + p2.Kills; } }

        public int TotalLevel { get { return p1.Level + p2.Level; } }

        public bool ResetGame { get { return p1.State == SpriteState.dead || p2.State == SpriteState.dead; } set { } }

        Stopwatch aliveTimer;
        public PlayerManager(Game game, Camera camera) : base(game) {
            Game.Components.Add(this);
            p1 = new Player(game, 0, new AnimationData("SpaceShipBlue", 3, 0.2f), 1, 1, camera);
            p2 = new Player(game, 1, new AnimationData("SpaceShipPurple", 3, 0.2f), 1, 2, camera);
            p2.Position = p1.Position + new Vector2(200, 200);
            aliveTimer = new Stopwatch();
            
        }

        public float[] GetAliveTimerFloat()
        {
            float[] f = new float[3];
            f[0] = aliveTimer.Elapsed.Minutes;
            f[1] = aliveTimer.Elapsed.Seconds;
            f[2] = aliveTimer.Elapsed.Milliseconds;
            return f;
        }

        public string GetAliveTimeString()
        {
            float minutes = aliveTimer.Elapsed.Minutes;
            float seconds = aliveTimer.Elapsed.Seconds;
            float milliseconds = aliveTimer.Elapsed.Milliseconds;
            string s = minutes + ":" + seconds + ":" + milliseconds;
            if (seconds < 10)
            {
                s = minutes + ":0" + seconds + ":" + milliseconds;
            }
            return s;
        }
        public void RestartAliveTimer()
        {
            aliveTimer.Restart();
        }

        public void StopAliveTimer()
        {
            aliveTimer.Stop();
        }

        public void ResetPlayers()
        {
            StopAliveTimer();
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
