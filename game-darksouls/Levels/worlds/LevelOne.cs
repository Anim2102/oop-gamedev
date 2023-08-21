using Collectible;
using game_darksouls.Collectible;
using game_darksouls.Component;
using game_darksouls.Entity;
using game_darksouls.Entity.EntityUtils;
using game_darksouls.Level;
using game_darksouls.Utilities;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace game_darksouls.Levels.worlds
{
    public class LevelOne : LevelSetup, ILevel
    {
        private Camera camera;
        private Hud hud;
        private BackgroundGame backgroundGame;
        private ICollectibleManager collectibleManager;
        private CollisionManager collisionManager;

        private Player player;

        public bool IsComplete
        {
            get
            {
                return collectibleManager.IsComplete;
            }
        }

        public bool IsLost
        {
            get
            {
                return !player.IsAlive;
            }
        }

        public LevelOne(ContentManager contentManager, Viewport viewport) : base(Game1.dungeonTexture, "Levels/csv levels/level-one-fixed.csv")
        {
            collectibleManager = new CollectibleManager();
            collisionManager = new CollisionManager(this, collectibleManager);

            

            player = new Player(contentManager.Load<Texture2D>("Knight"),contentManager,collisionManager);
            player.StartPosition(new Vector2(500, 700));
            entitys.Add(player);


            IEntity skeleton = EntityFactory.EntityCreator(contentManager, "skeleton", player,collisionManager,new Vector2(1500,658),new Vector2(2120,658));
            IEntity wingedMob = EntityFactory.EntityCreator(contentManager, "Brain Mole", player,collisionManager, new Vector2(2900,550),new Vector2(3250,500));
            IEntity wizard = EntityFactory.EntityCreator(contentManager, "wizard", player, collisionManager);


            wizard.StartPosition(new Vector2(2065, 250));
            entitys.Add(wizard);
            skeleton.StartPosition(new Vector2(1700, 648));
            entitys.Add(skeleton);
            wingedMob.StartPosition(new Vector2(3100, 550));
            entitys.Add(wingedMob);
            
            
            Vector2 gemOnePosition = new Vector2(2510, 836);
            Crystal gemOne = new Crystal(contentManager.Load<Texture2D>("crystal"),gemOnePosition);


            //{ X: 3215 Y: 490}
            Vector2 gemTwoPosition = new Vector2(3215, 490);
            Crystal gemTwo = new Crystal(contentManager.Load<Texture2D>("crystal"), gemTwoPosition);

            //{ X: 3165 Y: 97}
            Vector2 gemThirdPosition = new Vector2(3165, 97);
            Crystal gemThird = new Crystal(contentManager.Load<Texture2D>("crystal"), gemThirdPosition);


            collectibleManager.AddCollectible(gemOne);
            collectibleManager.AddCollectible(gemTwo);
            collectibleManager.AddCollectible(gemThird);

            camera = new Camera(viewport, player);
            hud = new Hud(player.Health,contentManager,viewport);

            Texture2D backgroundTexture = contentManager.Load<Texture2D>("BgGame");
            backgroundGame = new BackgroundGame(backgroundTexture, viewport, camera);
        }
        
        public override void Update(GameTime gameTime)
        {
            camera.Update();
            hud.Update();
            backgroundGame.Update();
            collectibleManager.Update(gameTime);
            collisionManager.CheckOutOfBounds();

            
            //Debug.WriteLine(IsComplete);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
          
            spriteBatch.Begin(transformMatrix: camera.CreateTransformation());
            backgroundGame.Draw(spriteBatch);
            collectibleManager.Draw(spriteBatch);
            base.Draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin();
            hud.Draw(spriteBatch);
            spriteBatch.End();


            spriteBatch.Begin();
            camera.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
