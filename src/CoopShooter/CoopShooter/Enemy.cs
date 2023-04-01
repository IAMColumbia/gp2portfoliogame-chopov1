using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmShooter
{
    public class Enemy : Sprite
    {
        int health;
        float speed;
        Vector2 target;

        EnemyManager em;
        public Enemy(Game game, EnemyManager em,Camera c) : base (game, "TestEnemy", c){
            collisonTag = CollisionTag.Enemy;
            this.em = em;
            Position = new Vector2 (100,100);
            speed = 0.001f;
            Direction = new Vector2(1,0);
        }

        public void setTarget(Vector2 t)
        {
            target= t;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            setDirection();
            moveEnemy(gameTime);
        }

        public void moveEnemy(GameTime gameTime)
        {
            Position += Direction * speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            //Position = target;
        }

        void setDirection()
        {
            Direction = target - Position;
        }
    }
}
