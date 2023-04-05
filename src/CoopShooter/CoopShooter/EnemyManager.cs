using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhythmShooter
{
    public class EnemyManager : GameComponent
    {
        PlayerManager pm;
        List<Enemy> enemies;
        public EnemyManager(Game game, PlayerManager pm, Camera c) : base(game)
        {
            enemies= new List<Enemy>();
            this.pm = pm;
            Game.Components.Add(this);
            createEnemies(c);
            addEnemiesToCollisionManager();
        }

        private void createEnemies(Camera c)
        {
            Enemy e = new Enemy(Game, this, c);
            enemies.Add(e);
        }

        private void addEnemiesToCollisionManager()
        {
            foreach(Enemy e in enemies)
            {
                CollisionManager.instance.AddCollidableObj(e);
            }
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            updateEnemies();
        }

        void updateEnemies()
        {
            foreach(Enemy e in enemies)
            {
                e.setTarget(pm.GetEnemyTargetPos());
            }
        }
    }
}
