using Collectible;
using game_darksouls.Collectible;
using game_darksouls.Component;
using game_darksouls.Entity;
using game_darksouls.Entity.EntityUtils;
using game_darksouls.Level;
using game_darksouls.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace game_darksouls.Levels.worlds
{
    internal class LevelTwo : LevelSetup, ILevel
    {
        private Camera camera;
        private Hud hud;
        private BackgroundGame backgroundGame;

        private ICollectibleManager collectibleManager;

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

        public LevelTwo(ContentManager contentManager, Viewport viewport) : base(Game1.dungeonTexture, "Levels/csv levels/level-two-final.csv")
        {
            collectibleManager = new CollectibleManager();
            CollisionManager collisionManager = new CollisionManager(this, collectibleManager);

            player = new Player(contentManager.Load<Texture2D>("Knight"),contentManager,collisionManager);
            player.StartPosition(new Vector2(500, 0));
            entitys.Add(player);


            IEntity skeleton = EntityFactory.EntityCreator(contentManager, "skeleton", player, collisionManager,new Vector2(1260,250),new Vector2(1900,250));
            IEntity wingedMob = EntityFactory.EntityCreator(contentManager, "Brain Mole", player, collisionManager, new Vector2(2000,650),new Vector2(2900,600));
            IEntity wizard = EntityFactory.EntityCreator(contentManager, "wizard", player, collisionManager);

            skeleton.StartPosition(new Vector2(1500, 250));
            entitys.Add(skeleton);

            wizard.StartPosition(new Vector2(2955, 400));
            entitys.Add(wizard);

            wingedMob.StartPosition(new Vector2(2600, 650));
            entitys.Add(wingedMob);



            Vector2 gemOnePosition = new Vector2(1790 ,650);
            Crystal gemOne = new Crystal(contentManager.Load<Texture2D>("crystal"), gemOnePosition);

            Vector2 gemTwoPosition = new Vector2(3555, 180);
            Crystal gemTwo = new Crystal(contentManager.Load<Texture2D>("crystal"), gemTwoPosition);

            collectibleManager.AddCollectible(gemTwo);
            collectibleManager.AddCollectible(gemOne);

            camera = new Camera(viewport, player);
            hud = new Hud(player.Health, contentManager, viewport);
            Texture2D backgroundTexture = contentManager.Load<Texture2D>("BgGame");
            backgroundGame = new BackgroundGame(backgroundTexture, viewport, camera);
        }

        public override void Update(GameTime gameTime)
        {
            camera.Update();
            hud.Update();
            backgroundGame.Update();
            collectibleManager.Update(gameTime);
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
        }
    }
}
