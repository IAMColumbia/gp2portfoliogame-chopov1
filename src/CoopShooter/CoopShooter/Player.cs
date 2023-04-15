﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoopShooter;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RhythmGameLibrary;

namespace RhythmShooter
{
    public class Player : CollidableSprite
    {
        PlayerController controller;
        float acceleration;
        float friction;
        
        float maxSpeed;

        ProjectileSpawner gun;

        public Vector2 ShootDir;

        Vector2 rotationDir;

        Texture2D deadTexture;
        Texture2D liveTexture;

        public int Kills;


        public Player(Game game, int playerNumber ,string texturename, int frames, float frameTime, Camera camera) : base(game, texturename, camera)
        {
            colInfo.tag = CollisionTag.Player;
            Position= new Vector2(100, 100);
            controller= new PlayerController(playerNumber);
            maxSpeed= 1.0f;
            acceleration = 0.06f;
            friction = 0.02f;
            gun = new ProjectileSpawner(game, this,camera, 3);
        }

        public void ResetPlayer(Vector2 startPos)
        {
            gun.ResetObjects();
            Kills = 0;
            state = SpriteState.alive;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            deadTexture = Game.Content.Load<Texture2D>("TestPlayerDead");
            liveTexture = spriteTexture;
        }

        public void AddScore()
        {
            Kills++;
        }

        protected override void updateRotation()
        {
            Rotation = (float)Math.Atan2((double)rotationDir.Y, (double)rotationDir.X);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            controller.Update();
            Direction = controller.Direction;
            movePlayer(gameTime);
            checkForShoot();
            keepOnScreen();
            setVelocity();
        }

        protected override void StateBasedUpdate()
        {
            switch (state) {
                case SpriteState.alive:
                    if(spriteTexture != liveTexture)
                    {
                        spriteTexture = liveTexture;
                    }
                    break;
                case SpriteState.dead:
                    if (spriteTexture != deadTexture)
                    {
                        spriteTexture = deadTexture;
                    }
                    break;
            }
        }

        bool collidingWithPlayer;
        protected override void onCollision(CollisionObj obj)
        {
            base.onCollision(obj);
            switch (obj.tag)
            {
                case CollisionTag.Player:
                    collideWithSimilarType(obj);
                    break;
                case CollisionTag.Enemy:
                    state = SpriteState.dead;
                    
                    break;
                case CollisionTag.none:
                    collidingWithPlayer = false;
                    break;
            }
        }

        Rectangle intersection;
        Rectangle otherRect;
        void collideWithSimilarType(CollisionObj obj)
        {
            otherRect = CollisionManager.instance.GetCollidable(obj.id).GetRect();
            intersection = CollisionManager.instance.Intersection(colInfo.id, obj.id);
            if (Math.Abs(intersection.Width) < Math.Abs(intersection.Height))
            {
                if(Rect.Right > otherRect.Right)
                {
                    Position.X += intersection.Width + 1;
                }
                else
                {
                    Position.X -= intersection.Width + 1;
                }
                
            }
            else
            {
                if (Rect.Top > otherRect.Top)
                {
                    Position.Y += intersection.Height+ 1;
                }
                else
                {
                    Position.Y -= intersection.Height + 1;
                }
            }

            if (!collidingWithPlayer)
            {
                collidingWithPlayer = true;
                velocity *= -1;
            }

        }
        Vector2 otherVel;
        protected override void onCollisionEnd(CollisionObj obj)
        {
            base.onCollisionEnd(obj);
            switch (obj.tag)
            {
                case CollisionTag.Enemy:
                    spriteTexture = liveTexture;
                    break;
                case CollisionTag.Player:
                    //need to handle velocity zero case in collisionEnd becuase the order in which the velocity is flipped affects the direction in which the ship is launched. (toward or away from the other ship) this way we only launch the ships away from eachother becuase we get abs value and we know for sure the other player flipped its velocity already.
                    /*if (collidingWithPlayer)
                    {
                        if (velocity == Vector2.Zero)
                        {
                            otherVel = CollisionManager.instance.GetCollidable(obj.id).GetVelocity() / 2;
                            velocity = new Vector2(Math.Abs(otherVel.X), Math.Abs(otherVel.Y));
                            if (otherVel.X > 0)
                            {
                                velocity.X *= -1;
                            }
                            if (otherVel.Y > 0)
                            {
                                velocity.Y *= -1;
                            }
                        }
                    }*/
                    collidingWithPlayer = false;
                    break;

            }
        }

        public void SetRotation(Vector2 otherpos)
        {
            rotationDir = Vector2.Normalize(Position - otherpos);
        }

        private bool hasShot;
        private void checkForShoot()
        {
            ShootDir = getDirectionFromRotation();
            if (controller.IsShooting)
            {
                if (!hasShot)
                {
                    gun.SpawnObject(this.Position);
                    hasShot = true;
                }
            }
            else
            {
                hasShot = false;
            }
        }

        public void movePlayer(GameTime gameTime)
        {
            Position += velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            addFriction();
        }

        private Vector2 getDirectionFromMouse()
        {
            return Vector2.Normalize(Position - controller.MousePos);
        }

        private void setVelocity()
        {
            if(Direction.X > 0)
            {
                velocity.X = Math.Min(maxSpeed, velocity.X + acceleration);
            }
            if(Direction.X < 0)
            {
                velocity.X = Math.Max(-maxSpeed, velocity.X - acceleration);
            }
            if (Direction.Y > 0)
            {
                velocity.Y = Math.Min(maxSpeed, velocity.Y + acceleration);
            }
            if (Direction.Y < 0)
            {
                velocity.Y = Math.Max(-maxSpeed, velocity.Y - acceleration);
            }
        }
        private void addFriction()
        {
            if(Direction.X == 0)
            {
                if (velocity.X > 0)
                {
                    velocity.X = Math.Max(0, velocity.X - friction);
                }
                if (velocity.X < 0)
                {
                    velocity.X = Math.Min(0, velocity.X + friction);
                }
            }
            if(Direction.Y == 0)
            {
                if (velocity.Y > 0)
                {
                    velocity.Y = Math.Max(0, velocity.Y - friction);
                }
                if (velocity.Y < 0)
                {
                    velocity.Y = Math.Min(0, velocity.Y + friction);
                }
            }
        }

        private void keepOnScreen()
        {
            if(Rect.Left + Rect.Width > GraphicsDevice.Viewport.Width || Rect.Left < 0)
            {
                if(Rect.Left < 0)
                {
                    Position.X = 1;
                }
                else
                {
                    Position.X = GraphicsDevice.Viewport.Width - 1 - Rect.Width;
                }
                velocity.X *= -1;
            }
            if(Rect.Top + Rect.Height > GraphicsDevice.Viewport.Height || Rect.Top < 0)
            {
                if(Rect.Top < 0)
                {
                    Position.Y = 1;
                }
                else
                {
                    Position.Y = GraphicsDevice.Viewport.Height - 1 - Rect.Height;
                }
                velocity.Y *= -1;
            }
        }
    }
}
