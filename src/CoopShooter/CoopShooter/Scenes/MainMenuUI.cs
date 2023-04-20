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
    public class MainMenuUI : MenuUI
    {
        public MainMenuUI(Game game) : base(game)
        {

        }

        protected override void drawUI()
        {
            base.drawUI();
            if (GamePad.GetState(0).IsConnected)
            {
                DrawCustomString(ScoreFont, "Press Y to play", 400, Color.Green);
            }
            else
            {
                DrawCustomString(ScoreFont, "Press SPACE to play", 400, Color.Green);
            }
            DrawCustomString(ScoreFont, "Coop Shooter", 100, Color.DarkSlateGray);
        }
    }
}
