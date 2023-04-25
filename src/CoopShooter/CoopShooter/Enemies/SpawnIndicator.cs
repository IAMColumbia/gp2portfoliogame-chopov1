using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopShooter.Enemies
{
    public class SpawnIndicator : AnimatableSprite
    {
        protected MovingSprite Parent;
        Rectangle bounds;
        public SpawnIndicator(Game game, Camera camera, MovingSprite parent) : base(game, new AnimationData("SpawnIndicator", 2, 0.2f), camera)
        {
            Parent = parent;
            bounds = Game.GraphicsDevice.Viewport.Bounds;
            //bounds = new Rectangle(bounds.X + (bounds.Width / 4), bounds.Y + (bounds.Height / 4), bounds.Width / 2, bounds.Height / 2);
            State = SpriteState.inactive;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            bounds = new Rectangle(bounds.X, bounds.Y, bounds.Width - (spriteTexture.Width / frames), bounds.Height - (spriteTexture.Height * 2));
        }

        protected override void StateBasedUpdate()
        {
            switch (State)
            {
                case SpriteState.active:
                    Visible = true;
                    break;
                case SpriteState.inactive:
                    Visible = false;
                    break;
            }
        }

        public void Activate()
        {
            State = SpriteState.active;
        }
        
        void updateVisibility()
        {
            if(Parent.State == SpriteState.inactive || Parent.State == SpriteState.dead)
            {
                State = SpriteState.inactive;
            }
            else if(Parent.Position.X < bounds.Right && Parent.Position.X > bounds.Left && Parent.Position.Y < bounds.Bottom && Parent.Position.Y > bounds.Top) 
            { 
                State = SpriteState.inactive;
            }
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            updatePos();
            updateVisibility();
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(spriteMarker, new Vector2(bounds.Left, bounds.Top), Color.White);
            spriteBatch.Draw(spriteMarker, new Vector2(bounds.Right, bounds.Top), Color.White);
            spriteBatch.Draw(spriteMarker, new Vector2(bounds.Left, bounds.Bottom), Color.White);
            spriteBatch.Draw(spriteMarker, new Vector2(bounds.Right, bounds.Bottom), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public void updatePos()
        {
            Position = FindIntersection(Parent.Position, Parent.target, bounds);
        }

        //chatgpt helped write this method after 7 hours of research, trial and error, and the lots of chatgpt prompts
    public Vector2 FindIntersection(Vector2 inside, Vector2 outside, Rectangle rect)
    {
        // Step 1: Determine the equation of the line segment
        float m = (outside.Y - inside.Y) / (outside.X - inside.X);
        float b = inside.Y - m * inside.X;

        // Step 2: Find the intersection point of the line with each of the four edges of the rectangle
        Vector2[] intersections = new Vector2[4];
        int count = 0;
        if (m != 0f)
        {
            intersections[count++] = new Vector2((rect.Top - b) / m, rect.Top);
            intersections[count++] = new Vector2((rect.Bottom - b) / m, rect.Bottom);
        }
            else
            {
                Debug.WriteLine("uh oh");
            }
        if (outside.X != inside.X)
        {
            intersections[count++] = new Vector2(rect.Left, m * rect.Left + b);
            intersections[count++] = new Vector2(rect.Right, m * rect.Right + b);
        }

        // Step 3: Check which intersection point lies on the line segment
        float minDistSq = float.MaxValue;
        Vector2 closest = Vector2.Zero;
        foreach (Vector2 intersection in intersections)
        {
            if (float.IsNaN(intersection.X) || float.IsNaN(intersection.Y)) continue;
            float dx = intersection.X - inside.X;
            float dy = intersection.Y - inside.Y;
            float distSq = dx * dx + dy * dy;
            if (distSq < minDistSq && intersection.X >= Math.Min(inside.X, outside.X) && intersection.X <= Math.Max(inside.X, outside.X) && intersection.Y >= Math.Min(inside.Y, outside.Y) && intersection.Y <= Math.Max(inside.Y, outside.Y))
            {
                minDistSq = distSq;
                closest = intersection;
            }
        }

        // Step 4: Return the closest intersection point
        return closest;
    }


    //https://www.topcoder.com/thrive/articles/Geometry%20Concepts%20part%202:%20%20Line%20Intersection%20and%20its%20Applications helped me write this method
    Vector2 lineLineIntersection(Vector2 p1, Vector2 p2, Vector2 r1, Vector2 r2)
        {
            float A1 = p2.Y - p1.Y;
            float B1 = p2.X - p1.X;
            float C1 = A1*p1.X + B1*p1.Y;

            float A2 = r2.Y - r1.Y;
            float B2 = r2.X - r1.X;
            float C2 = A2 * r1.X + B2 * r1.Y;

            float det = A1 * B2 - A2 * B1;
            float x = (B2 * C1 - B1 * C2) / det;
            float y = (A1 * C2 - A2 * C1) / det;

            return new Vector2(x, y);
        }
    }
}
