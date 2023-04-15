using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameLibrary
{
    public class CollidableSprite : Sprite, ICollidable
    {
        
        protected CollisionState collisionState;
        protected CollisionObj colInfo;
        protected Vector2 velocity;
        protected List<CollisionObj> currentOverlaps;
        List<CollisionObj> overlapsToRemove;
        public CollidableSprite(Game game, string texturename, Camera camera) : base(game, texturename, camera)
        {
            currentOverlaps = new List<CollisionObj>();
            overlapsToRemove = new List<CollisionObj>();
            colInfo.tag = CollisionTag.none;
            CollisionManager.instance.AddCollidableObj(this);
        }

        protected virtual void StateBasedUpdate()
        {

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            removePreviousOverlaps();
            StateBasedUpdate();
        }

        protected bool isOverlappingSameType()
        {
            foreach (CollisionObj collisionObj in currentOverlaps)
            {
                if(collisionObj.tag == colInfo.tag)
                {
                    return true;
                }
            }
            return false;
        }

        void removePreviousOverlaps()
        {
            if(currentOverlaps.Count > 0) {
                foreach (CollisionObj otherObj in currentOverlaps)
                {
                    if(!CollisionManager.instance.Overlapping(colInfo.id, otherObj.id))
                    {
                        overlapsToRemove.Add(otherObj);
                        onCollisionEnd(otherObj);
                    }
                }
                foreach(CollisionObj otherObj in overlapsToRemove)
                {
                    currentOverlaps.Remove(otherObj);
                }
                overlapsToRemove.Clear();
            }
        }

        protected virtual void onCollisionEnd(CollisionObj obj)
        {

        }

        public CollisionObj getObjInfo()
        {
            return colInfo;
        }

        public Rectangle GetRect()
        {
            return Rect;
        }

        public Vector2 GetPosition()
        {
            return Position;
        }

        public Vector2 GetVelocity()
        {
            return velocity;
        }

        public void SetVelocity(Vector2 vel)
        {
            velocity = vel;
        }

        public void OnOverlap(CollisionObj obj)
        {
            if(!IsOverlappingID(obj.id))
            {
                currentOverlaps.Add(obj);
                onCollision(obj);
            }
        }

        public void SetID(int id)
        {
            colInfo.id = id;
        }

        public bool IsActive()
        {
            return Enabled;
        }

        protected virtual void onCollision(CollisionObj obj)
        {
           // Debug.WriteLine("The object with ID " +  colInfo.id + " and tag " + colInfo.tag, " is overlapping the object with ID " + obj.id + " and tag " + obj.tag);
        }

        public bool IsOverlappingType(CollisionTag tag)
        {
            foreach(CollisionObj info in currentOverlaps)
            {
                if(info.tag == tag)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsOverlappingID(int id)
        {
            foreach (CollisionObj info in currentOverlaps)
            {
                if (info.id == id)
                {
                    return true;
                }
            }
            return false;
        }

        public void setState(CollisionState state)
        {
            collisionState = state;
        }

        public void addOverlap(CollisionObj obj)
        {
            currentOverlaps.Add(obj);
        }
    }
}
