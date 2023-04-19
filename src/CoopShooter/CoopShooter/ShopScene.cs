using Microsoft.Xna.Framework;
using RhythmGameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopShooter
{
    public class ShopScene : Scene
    {
        PlayerManager players;
        new mySceneManager sceneManager;
        ShopMenu shopUI;
        
        Stack<ProjectileDecorator> availableUpgrades;

        public ShopScene(Game game, mySceneManager manager, PlayerManager pm) : base(game, manager)
        {
            availableUpgrades = new Stack<ProjectileDecorator>();
            shopUI = new ShopMenu(game);
            addComponentToScene(shopUI);
            sceneManager = manager;
            Game.Components.Add(this);
            players = pm;
            createUpgradeStack();
        }

        void createUpgradeStack()
        {
            for(int i =6; i > 0;i--)
            {
                //make it rotate by 60 degrees
                availableUpgrades.Push(new ProjectileDecorator(Game, players.p1, players.p1.camera, 3, i*60));
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (sceneManager.sceneInput.ReleasedKey(Microsoft.Xna.Framework.Input.Keys.G))
            {
                closeShop();
            }
            if (sceneManager.sceneInput.ReleasedKey(Microsoft.Xna.Framework.Input.Keys.R))
            {
                UpgradeGun(players.p1);
            }
        }

        private void closeShop()
        {
            sceneManager.ChangeScene(sceneManager.shop, sceneManager.gamePlay);
        }

        private void UpgradeGun(Player p)
        {
            //p.AddProjectileDecorator(new ProjectileDecorator(Game, p, p.camera, 3, p.GetShootMod()));
            if(availableUpgrades.Count > 0)
            {
                p.AddProjectileDecorator(availableUpgrades.Pop());
            }
        }
    }
}
