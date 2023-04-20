using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopShooter
{
    public class GunController : GameComponent
    {
        List<ProjectileSpawner> guns;
        Stack<ProjectileSpawner> gunUpgrades;
        public int ActiveGuns { get { return guns.Count; } }
        int TotalGuns;
        Player player;
        public GunController(Game game, Player p, Camera camera) : base(game)
        {
            TotalGuns = 6;
            player = p;
            guns = new List<ProjectileSpawner>();
            gunUpgrades = new Stack<ProjectileSpawner>();
            guns.Add(new ProjectileSpawner(game, camera, 3, p, 0));
            createGuns(game, player, camera);
        }

        void createGuns(Game game, Player p, Camera camera)
        {
            for(int i =0; i < TotalGuns; i++) {
                gunUpgrades.Push(new RangedProjectileSpawner(game, camera, 3, p, i *60));
            }
        }

        public void AddGun()
        {
            guns.Add(gunUpgrades.Pop());
        }

        public void Shoot()
        {
            foreach (var gun in guns)
            {
                gun.Shoot(player.Position);
            }
        }

        public void Reset()
        {
            for (int i = guns.Count-1; i > 0; i--)
            {
                gunUpgrades.Push(guns[i]);
                guns.RemoveAt(i); 
            }
        }
    }
}
