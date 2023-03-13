using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmShooter
{
    public class GameStateManager : GameComponent
    {
        mySceneManager sceneManager;
        public GameStateManager(Game game) : base(game)
        {
            sceneManager = new mySceneManager(game);
        }
    }
}
