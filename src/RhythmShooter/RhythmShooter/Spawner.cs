using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RhythmShooter
{
    public class Spawner : GameComponent
    {
        protected Player player;
      
        protected Random rnd;
        protected int numOfObjects;
        public int NumberOfObjects { get { return objects.Count; } }
        protected Camera camera;

        protected Queue<Sprite> objects;
        List<Sprite> activeObjs;
        public Spawner(Game game, Player p, Camera c, int numberOfObjects) : base(game)
        {
            Game.Components.Add(this);
            camera= c;
            activeObjs = new List<Sprite>();
            objects = new Queue<Sprite>();
            player = p;
            numOfObjects = numberOfObjects;
            rnd = new Random();
        }

        public override void Initialize()
        {
            base.Initialize();
            objects = createObjects(numOfObjects);
        }

        public virtual Sprite createSpawnableObject()
        {
            return null;
        }

        public Queue<Sprite> createObjects(int size)
        {
            Queue<Sprite> objs = new Queue<Sprite>();
            for (int i = 0; i < size; i++)
            {
                Sprite sprite = createSpawnableObject();
                sprite.Enabled= false;
                objs.Enqueue(sprite);
            }
            return objs;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public virtual Sprite SpawnObject(Vector2 pos)
        {
            if (objects.Count > 0)
            {
                if (objects.Peek().Enabled == false)
                {
                    Sprite objToSpawn = objects.Dequeue();
                    objects.Enqueue(objToSpawn);
                    objToSpawn.Position = pos;
                    objToSpawn.Enabled = true;
                    objToSpawn.Visible = true;
                    return objToSpawn;
                }
            }
            return null;
        }

        protected virtual bool canSpawn(Vector2 posToSpawn)
        {
            return true;
        }

        public virtual void ResetObjects()
        {
            foreach (Sprite s in objects)
            {
                DeSpawn(s);
            }
        }

        public virtual void DeSpawn(Sprite s)
        {
            s.Enabled = false;
            s.Visible = false;
        }


        public List<Sprite> getActiveObjs()
        {
            activeObjs.Clear();
            foreach (Sprite s in objects)
            {
                if (s.Enabled)
                {
                    activeObjs.Add(s);
                }
            }
            return activeObjs;
        }
    }
}
