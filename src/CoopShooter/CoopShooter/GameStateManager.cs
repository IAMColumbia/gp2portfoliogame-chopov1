using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopShooter
{
    public enum GameState { playing, dead, reset, menu}
    public class GameStateManager : GameComponent
    {
        public GameState GameState;
        mySceneManager sceneManager;
        public GameStateManager(Game game) : base(game)
        {
            sceneManager = new mySceneManager(game);
        }
    }
}
