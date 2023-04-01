using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameLibrary
{
    public class CollisionManager : GameComponent
    {
        public static CollisionManager instance;
        List<Sprite> collidableObjects;
        public CollisionManager(Game game) : base(game)
        {
            collidableObjects= new List<Sprite>();
            instance = this;
            Game.Components.Add(this);
        }

        public void AddCollidableObj(Sprite collidableObj)
        {
            collidableObjects.Add(collidableObj);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            checkCollisions();
        }

        void checkCollisions()
        {
            //will this foreach configuration produce any duplicate cases?
            foreach(var obj in collidableObjects)
            {
                foreach(var obj2 in collidableObjects)
                {
                    //skip iteration if either object is not active
                    if(!obj.Enabled || !obj2.Enabled)
                    {
                        continue;
                    }
                    //dont check if object collides with itself
                    if(obj == obj2)
                    {
                        continue;
                    }
                    if(ObjectsCollide(obj.Rect, obj2.Rect))
                    {
                        obj.OnCollision(obj2.collisonTag);
                        obj2.OnCollision(obj.collisonTag);
                    }
                }
            }
        }

        bool ObjectsCollide(Rectangle A, Rectangle B)
        {
            //.top returns y cord of top of rect, .left returns x coord of left side of rect. etc...
            //if the top of rect a is less the the bottom of rect b or vis versa, they do not overlap
            if(A.Top < B.Bottom || B.Top < A.Bottom)
            {
                return false;
            }
            //if the right side of rect a is less the nthe left side of rect b or vis versa they do not overlap
            if(A.Right < B.Left || B.Right < A.Left)
            {
                return false;
            }
            return true;
        }
    }
}
