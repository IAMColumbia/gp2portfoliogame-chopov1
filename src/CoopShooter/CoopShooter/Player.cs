using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using CoopShooter;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RhythmGameLibrary;

namespace CoopShooter
{
    public class Player : AnimatableSprite
    {
        public int Level;
        public bool HasShield;
        PlayerController controller;
        float acceleration;
        float friction;
        float maxSpeed;
        public int GunLevel { get { return guns.ActiveGuns; } }
        public Vector2 ShootDir;
        Vector2 rotationDir;
        public int Kills;
        //create a timer that needs to finish before player can fire again. check if this is complete before spawning an object(wont need this if take rhythm approach)
        GunController guns;
        public int Range;
        Texture2D ShieldTexture;
        Timer fireTimer;
        float originalScale;
        float scaleIncrease;

        public Player(Game game, int playerNumber ,AnimationData anim, int frames, float frameTime, Camera camera) : base(game, anim, camera)
        {
            originalScale = scale;
            scaleIncrease = 0.05f;
            Range = 200;
            guns = new GunController(game, this, camera);
            this.camera = camera;
            colInfo.tag = CollisionTag.Player;
            Position= new Vector2(100, 100);
            controller= new PlayerController(playerNumber);
            maxSpeed= 1.0f;
            acceleration = 0.06f;
            friction = 0.02f;
            fireTimer = new Timer(600);
            fireTimer.Elapsed += shootProjectiles;
        }

        void shootProjectiles(Object o, ElapsedEventArgs e)
        {
            if(Enabled)
            {
                guns.Shoot();
            }
        }

        #region PowerUpMethods
        public void AddGun()
        {
            guns.AddGun();
            IncreaseScale(scaleIncrease);
        }

        public void UpgradeRange(int r)
        {
            Range += r;
            IncreaseScale(scaleIncrease);
        }

        public void AddShield()
        {
            HasShield = true;
            IncreaseScale(scaleIncrease);
        }

        public void UpgradeReloadSpeed(int s)
        {
            guns.UpgradeReloadSpeed(s);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            if(HasShield)
            {
                DrawLayer(ShieldTexture,
                new Vector2(Rect.X + ShieldTexture.Width, Rect.Y + ShieldTexture.Height),
                null,
                Color.White * transparency,
                Rotation,
                origin,
                scale);
            }
        }

        #endregion
        public void ResetPlayer(Vector2 startPos)
        {
            SetScale(originalScale);
            guns.Reset();
            Level = 0;
            Kills = 0;
            State = SpriteState.alive;
            HasShield = false;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            ShieldTexture = Game.Content.Load<Texture2D>("Shield");
        }

        public void AddScore()
        {
            Kills++;
            Level++;
            
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
            switch (State) {
                case SpriteState.alive:
                    if (!fireTimer.Enabled)
                    {
                        fireTimer.Start();
                    }
                    break;
                case SpriteState.dead:
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
                    //collideWithSimilarType(obj);
                    break;
                case CollisionTag.Enemy:
                    //check if player has shield
                    if (HasShield)
                    {

                        HasShield = false;
                    }
                    else
                    {
                        State = SpriteState.dead;

                    }
                    break;
                case CollisionTag.none:
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
        protected override void onCollisionEnd(CollisionObj obj)
        {
            base.onCollisionEnd(obj);
            switch (obj.tag)
            {
                case CollisionTag.Enemy:
                    break;
                case CollisionTag.Player:
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
            /*if (controller.IsShooting)
            {
                if (!hasShot)
                {
                    shootProjectiles();
                    hasShot = true;
                }
            }
            else
            {
                hasShot = false;
            }*/
        }
        public void movePlayer(GameTime gameTime)
        {
            Position += velocity * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            addFriction();
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
