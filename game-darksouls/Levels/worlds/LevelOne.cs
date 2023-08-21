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


            IEntity skeleton = EntityFactory.EntityCreator(contentManager, "skeleton", player,collisionManager);
            IEntity wingedMob = EntityFactory.EntityCreator(contentManager, "Brain Mole", player,collisionManager);
            IEntity wizard = EntityFactory.EntityCreator(contentManager, "wizard", player, collisionManager);
            
            skeleton.StartPosition(new Vector2(1700, 648));
            entitys.Add(skeleton);

            wingedMob.StartPosition(new Vector2(1865, 608));
            entitys.Add(wingedMob);

            wizard.StartPosition(new Vector2(1865, 608));
            entitys.Add(wizard);
            //skeleton {X:1785 Y:658}
            //mob {X:2130 Y:658}
            //wiz {X:2130 Y:658}

            Vector2 gemOnePosition = new Vector2(1700, 200);
            Crystal gemOne = new Crystal(contentManager.Load<Texture2D>("crystal"),gemOnePosition);
            
            collectibleManager.AddCollectible(gemOne);

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
