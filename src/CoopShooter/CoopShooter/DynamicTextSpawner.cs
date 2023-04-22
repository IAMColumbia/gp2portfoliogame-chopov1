using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopShooter
{
    public class DynamicTextSpawner : Spawner
    {
        public static DynamicTextSpawner instance;
        public DynamicTextSpawner(Game game, Camera c) : base(game, c, 20)
        {
            instance = this;
        }

        public void ActivateText(string text, Vector2 pos)
        {
            Sprite dt = SpawnObject(pos);
            ((DynamicText)dt).Activate(text);
        }

        public override Sprite createSpawnableObject()
        {
            return new DynamicText(Game, camera, this);
        }



    }
}
