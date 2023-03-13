using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using RhythmGameLibrary;

namespace RhythmShooter
{
    public class Player : AnimatableSprite
    {
        PlayerController controller;
        float acceleration;
        float friction;
        Vector2 velocity;
        float maxSpeed;

        ProjectileSpawner gun;

        public Vector2 ShootDir;

        public Player(Game game, int playerNumber ,string texturename, int frames, float frameTime, Camera camera) : base(game, texturename, frames, frameTime, camera)
        {
            controller= new PlayerController(playerNumber);
            maxSpeed= 1.0f;
            acceleration = 0.06f;
            friction = 0.02f;

            gun = new ProjectileSpawner(game, this,camera, 3);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            controller.Update();
            Direction= controller.Direction;
            movePlayer(gameTime);
            checkForShoot();
            setVelocity();
            keepOnScreen();
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
            updateVelocity();
            Position += velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            addFriction();
        }

        private void updateVelocity()
        {
            if (controller.IsAccelerating)
            {
                setVelocity();
            }
        }
        #region oldVelocity
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
        #endregion
        private void addFriction()
        {
            //Debug.WriteLine(Direction);
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

        private void addForce()
        {

        }

        private void keepOnScreen()
        {
            if(Position.X > GraphicsDevice.Viewport.Width || Position.X < 0)
            {
                velocity.X *= -1;
            }
            if(Position.Y > GraphicsDevice.Viewport.Height || Position.Y < 0)
            {
                velocity.Y *= -1;
            }
        }
    }
}
