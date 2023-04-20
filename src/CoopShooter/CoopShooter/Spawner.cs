using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CoopShooter
{
    public class Spawner : GameComponent, ISceneComponenet
    {
        protected Random rnd;
        protected int numOfObjects;
        public int NumberOfObjects { get { return objects.Count; } }
        protected Camera camera;

        protected Queue<Sprite> objects;
        public List<Sprite> ActiveObjs { get; protected set; }
        public Spawner(Game game, Camera c, int numberOfObjects) : base(game)
        {
            Game.Components.Add(this);
            numOfObjects = numberOfObjects;
            camera = c;
            ActiveObjs = new List<Sprite>();
            objects = new Queue<Sprite>();
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
            //magic number, put a varibale for a location to store objects.
            s.Position = new Vector2(-800, -800);
            s.Enabled = false;
            s.Visible = false;
        }


        public List<Sprite> getActiveObjs()
        {
            ActiveObjs.Clear();
            foreach (Sprite s in objects)
            {
                if (s.Enabled)
                {
                    ActiveObjs.Add(s);
                }
            }
            return ActiveObjs;
        }

        public void Load()
        {
            foreach(Sprite s in objects)
            {
                s.Load();
            }
            Enabled = true;
        }

        public void UnLoad()
        {
            foreach (Sprite s in objects)
            {
                s.UnLoad();
            } 
            Enabled = false;
        }
    }
}
