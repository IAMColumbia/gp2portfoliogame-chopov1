using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopShooter
{
    public class CollidableAnimatableSprite : AnimatableSprite, ICollidable
    {
        public CollidableAnimatableSprite(Game game, string texturename, int frames, float frameTime, Camera camera) : base(game, texturename, frames, frameTime, camera)
        {
            currentOverlaps = new List<CollisionObj>();
            overlapsToRemove = new List<CollisionObj>();
            colInfo.tag = CollisionTag.none;
            CollisionManager.instance.AddCollidableObj(this);
        }

        protected CollisionObj colInfo;
        protected Vector2 velocity;
        protected List<CollisionObj> currentOverlaps;
        List<CollisionObj> overlapsToRemove;

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            removePreviousOverlaps();
        }

        void removePreviousOverlaps()
        {
            if (currentOverlaps.Count > 0)
            {
                foreach (CollisionObj otherObj in currentOverlaps)
                {
                    if (!CollisionManager.instance.Overlapping(colInfo.id, otherObj.id))
                    {
                        overlapsToRemove.Add(otherObj);
                        onCollisionEnd(otherObj);
                    }
                }
                foreach (CollisionObj otherObj in overlapsToRemove)
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

        public void OnOverlap(CollisionObj obj)
        {
            currentOverlaps.Add(obj);
            onCollision(obj);

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

        public void SetVelocity(Vector2 vel)
        {
            velocity = vel;
        }

        public void setState(CollisionState state)
        {
            throw new NotImplementedException();
        }

        public void addOverlap(CollisionObj obj)
        {
            currentOverlaps.Add(obj);
        }
    }
}
