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
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace game_darksouls.Levels.worlds
{
    public class LevelOne : LevelSetup
    {
        private Camera camera;
        private Hud hud;

        private ICollectibleManager collectibleManager;

        public LevelOne(ContentManager contentManager, Viewport viewport) : base(Game1.dungeonTexture, @"Content\test.3.csv")
        {
            collectibleManager = new CollectibleManager();
            CollisionManager collisionManager = new CollisionManager(this, collectibleManager);

            Player player = new Player(contentManager.Load<Texture2D>("Knight"),collisionManager);
            player.StartPosition(new Vector2(500, 700));
            entitys.Add(player);


            AnimatedObject skeleton = EntityFactory.EntityCreator(contentManager, "skeleton", player,collisionManager);
            AnimatedObject wingedMob = EntityFactory.EntityCreator(contentManager, "Brain Mob", player,collisionManager);
            AnimatedObject wizard = EntityFactory.EntityCreator(contentManager, "wizard", player, collisionManager);
            
            skeleton.StartPosition(new Vector2(1700, 648));
            entitys.Add(skeleton);
            //skeleton {X:1785 Y:658}
            //mob {X:2130 Y:658}
            //wiz {X:2130 Y:658}

            Vector2 gemOnePosition = new Vector2(1700, 628);
            Crystal gemOne = new Crystal(contentManager.Load<Texture2D>("crystal"),gemOnePosition);
            
            collectibleManager.AddCollectible(gemOne);

            camera = new Camera(viewport, player);
            hud = new Hud(player.HealthManager,contentManager,viewport);
        }

        public override void Update(GameTime gameTime)
        {
            camera.Update();
            hud.Update();
            collectibleManager.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
          
            spriteBatch.Begin(transformMatrix: camera.CreateTransformation());
            collectibleManager.Draw(spriteBatch);
            base.Draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin();
            hud.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
