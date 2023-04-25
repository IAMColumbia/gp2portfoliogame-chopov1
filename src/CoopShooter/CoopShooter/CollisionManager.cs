using CSCore.XAudio2.X3DAudio;
using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace RhythmGameLibrary
{

    public enum CollisionTag { none, Player, Enemy, Projectile }
    public enum CollisionState { Moving, AboutToCollide, Overlapping };
    public struct CollisionObj
    {
        public int id;
        public CollisionTag tag;
    }
    public interface ICollidable
    {
        public void setState(CollisionState state);
        public bool IsActive();
        public void SetID(int id);
        public CollisionObj getObjInfo();
        public void OnOverlap(CollisionObj obj);

        public void addOverlap(CollisionObj obj);

        public Vector2 GetPosition();
        public Rectangle GetRect();

        public void SetVelocity(Vector2 vel);
        public Vector2 GetVelocity();
    }
    public class CollisionManager : GameComponent
    {
        bool assignedIDs;

        public static CollisionManager instance;

        List<ICollidable> collidableObjects;
        public CollisionManager(Game game) : base(game)
        {
            collidableObjects= new List<ICollidable>();
            instance = this;
            Game.Components.Add(instance);
        }

        public override void Initialize()
        {
            base.Initialize();
            
        }

        void assignIDs()
        {
            for(int i =0; i < collidableObjects.Count; i++)
            {
                collidableObjects[i].SetID(i);
            }
            assignedIDs = true;
        }

        public void AddCollidableObj(ICollidable collidableObj)
        {
            collidableObjects.Add(collidableObj);
        }

        public ICollidable GetCollidable(int id)
        {
            return collidableObjects[id];
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            checkCollisions();
            if (!assignedIDs)
            {
                assignIDs();
            }
        }


        ICollidable ObjA;
        ICollidable ObjB;
        void checkCollisions()
        {
            //maybe make this regular for loops so we only check each object against eachother once to be more performant
           for(int i =0; i < collidableObjects.Count; i++)
            {
                for(int j = i+1; j < collidableObjects.Count; j++)
                {
                    ObjA = collidableObjects[i];
                    ObjB = collidableObjects[j];
                    if(!ObjA.IsActive() || !ObjB.IsActive())
                    {
                        continue;
                    }
                    if (ObjA == ObjB)
                    {
                        continue;
                    }
                    if(ObjA.getObjInfo().tag == ObjB.getObjInfo().tag)
                    {
                        //KeepSimilarObjsFromOverlap(ObjA, ObjB);
                    }
                    if (ObjectsCollide(ObjA.GetRect(), ObjB.GetRect()))
                    {
                        ObjA.OnOverlap(ObjB.getObjInfo());
                        ObjB.OnOverlap(ObjA.getObjInfo());
                    }
                }
            }
        }

        public bool ObjectsCollide(Rectangle RectA, Rectangle RectB)
        {
            
            if(RectA.Left < RectB.Right && RectA.Right > RectB.Left &&
             RectA.Top < RectB.Bottom && RectA.Bottom > RectB.Top)
            {
                return true;
            }
            return false;
        }

        public void KeepSimilarObjsFromOverlap(ICollidable A, ICollidable B)
        {
            if(A.GetVelocity().X > 0 && isTouchingLeft(A, B) || A.GetVelocity().X < 0 && isTouchingRight(A, B))
            {
                A.SetVelocity(new Vector2(0, A.GetVelocity().Y));
                B.SetVelocity(new Vector2(0, B.GetVelocity().Y));
            }
            if(A.GetVelocity().Y > 0 && isTouchingTop(A, B) || A.GetVelocity().Y < 0 && isTouchingBottom(A, B))
            {
                A.SetVelocity(new Vector2(A.GetVelocity().X, 0));
                B.SetVelocity(new Vector2(B.GetVelocity().X, 0));
            }
        }

        public bool isTouchingLeft(ICollidable A, ICollidable B)
        {
            return A.GetRect().Right + A.GetVelocity().X > B.GetRect().Left && 
                A.GetRect().Left < B.GetRect().Left && 
                A.GetRect().Bottom > B.GetRect().Top &&
                A.GetRect().Top < B.GetRect().Bottom;
        }

        public bool isTouchingRight(ICollidable A, ICollidable B)
        {
            return A.GetRect().Left + A.GetVelocity().X < B.GetRect().Right &&
                A.GetRect().Right > B.GetRect().Right &&
                A.GetRect().Bottom > B.GetRect().Top &&
                A.GetRect().Top < B.GetRect().Bottom;
        }

        public bool isTouchingTop(ICollidable A, ICollidable B)
        {
            return A.GetRect().Bottom + A.GetVelocity().Y > B.GetRect().Top &&
                A.GetRect().Top < B.GetRect().Top &&
                A.GetRect().Right > B.GetRect().Left &&
                A.GetRect().Left < B.GetRect().Right;
        }

        public bool isTouchingBottom(ICollidable A, ICollidable B)
        {
            return A.GetRect().Top + A.GetVelocity().Y < B.GetRect().Bottom &&
                A.GetRect().Bottom > B.GetRect().Bottom &&
                A.GetRect().Right > B.GetRect().Left &&
                A.GetRect().Left < B.GetRect().Right;
        }

        public bool Overlapping(int id1, int id2)
        {
            if (ObjectsCollide(collidableObjects[id1].GetRect(), collidableObjects[id2].GetRect()))
            {
                return true;
            }
            return false;
        }

        Rectangle a;
        Rectangle b;
        int x;
        int y;
        int width;
        int height;
        public Rectangle Intersection(int id1, int id2)
        {
            a = collidableObjects[id1].GetRect();
            b = collidableObjects[id2].GetRect();
            x = Math.Max(a.X, b.X);
            y = Math.Max(a.Y, b.Y);

            width = Math.Min(a.X + a.Width, b.X + b.Width) - x;
            height = Math.Min(a.Y + a.Height, b.Y + b.Height) - y;
            return new Rectangle(x, y, width, height);
        }

        
    }
}
