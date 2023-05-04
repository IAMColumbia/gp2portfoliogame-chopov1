using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using CoopShooter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopShooter
{
    public class GameplayUI : MenuUI
    {
        PlayerManager playerManager;
        public GameplayUI(Game game, PlayerManager pm) : base(game)
        {
            playerManager = pm;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            DrawCustomString(ScoreFont, "Kills: " + playerManager.TotalKills.ToString(), 100,Color.Wheat);
            DrawCustomString(ScoreFont, playerManager.GetAliveTime(), 30, Color.Wheat);
            spriteBatch.End();
        }
    }
}
