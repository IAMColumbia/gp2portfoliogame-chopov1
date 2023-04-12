using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace RhythmShooter
{
    public class PlayerManager : GameComponent
    {
        Player p1;
        Player p2;
        public PlayerManager(Game game, Camera camera) : base(game) {
            Game.Components.Add(this);
            p1 = new Player(game, 0, "TestShip2", 1, 1, camera);
            p2 = new Player(game, 1, "TestShip1", 1, 2, camera);
            p2.Position = p1.Position + new Vector2(200, 200);
            CollisionManager.instance.AddCollidableObj(p1);
            CollisionManager.instance.AddCollidableObj(p2);
        }

        public override void Update(GameTime gameTime)
        {
            p1.SetRotation(p2.Position);
            p2.SetRotation(p1.Position);
            base.Update(gameTime);
        }

        public Vector2 GetEnemyTargetPos()
        {
            return new Vector2((p1.Position.X + p2.Position.X)/2, (p1.Position.Y + p2.Position.Y )/2);
        }

    }
}
