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
    public class GameOverUI : MenuUI
    {
        public int HighScore;
        public string timeAlive;
        public GameOverUI(Game game) : base(game)
        {
        }

        protected override void drawUI()
        {
            DrawCustomString(ScoreFont, "Game Over", 100, Color.DarkSlateGray);
            DrawCustomString(ScoreFont, "Your highest kills is " + HighScore, 200, Color.DarkSlateGray);
            DrawCustomString(ScoreFont, "You lasted " + timeAlive + " this run", 300, Color.DarkSlateGray);

            if (GamePad.GetState(0).IsConnected)
            {
                DrawCustomString(ScoreFont, "Press Y to play again", 500, Color.Green);
                DrawCustomString(ScoreFont, "Press B to return to menu", 550, Color.Green);
            }
            else
            {
                DrawCustomString(ScoreFont, "Press SPACE to play", 500, Color.Green);
                DrawCustomString(ScoreFont, "Press Back to return to menu", 550, Color.Green);
            }

        }
    }
}
