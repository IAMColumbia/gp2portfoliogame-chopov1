using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using RhythmShooter;
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
            DrawCustomString(ScoreFont, playerManager.TotalKills.ToString(), 30,Color.Wheat);
            spriteBatch.End();
        }
    }
}
