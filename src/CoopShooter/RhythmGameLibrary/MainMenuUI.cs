using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameLibrary
{
    public class MainMenuUI : MenuUI
    {

        public MainMenuUI(Game game) : base(game)
        {
        }

        protected override void drawUI()
        {
           DrawCustomString(BasicFont, "Hello", 200, Color.White);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }
    }
}
