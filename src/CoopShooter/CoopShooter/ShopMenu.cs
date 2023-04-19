using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopShooter
{
    public class ShopMenu : MenuUI
    {
        public ShopMenu(Game game) : base(game)
        {

        }

        protected override void drawUI()
        {
            base.drawUI();
            DrawCustomString(ScoreFont, "SHOP", 500, Color.Beige);
            DrawCustomString(ScoreFont, "PRESS G To Return To Game", 550, Color.Beige);
            DrawCustomString(ScoreFont, "PRESS R To Upgrade p1 gun (not complete)", 600, Color.Beige);
        }
    }
}
