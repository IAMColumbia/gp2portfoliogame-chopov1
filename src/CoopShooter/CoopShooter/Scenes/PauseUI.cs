using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopShooter
{
    public class PauseUI : MenuUI
    {
        public PauseUI(Game game) : base(game)
        {

        }

        protected override void drawUI()
        {
            base.drawUI();
            DrawCustomString(ScoreFont, "Game Paused", 100, Color.DarkSlateGray);
            if (GamePad.GetState(0).IsConnected)
            {
                DrawCustomString(ScoreFont, "Press Y to resume", 400, Color.Green);
            }
            else
            {
                DrawCustomString(ScoreFont, "Press SPACE to play", 400, Color.Green);
            }
        }
    }
}
