using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using RhythmGameLibrary;

namespace RhythmShooter
{
    public class mySceneManager : SceneManager
    {
        Scene mainMenu;
        public mySceneManager(Game game) : base(game)
        {
            mainMenu = new MainScene(game, this);
        }
    }
}
