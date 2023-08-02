using game_darksouls.Entity;
using game_darksouls.Level;
using game_darksouls.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace game_darksouls
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        //temporary for classes&textures
        private Texture2D knightSpritesheet;
        private Player player;
        private Texture2D skeletonTexture;
        private Skeleton skeleton;
        private Texture2D wingedMobTexture;
        private WingedMob wingedMob;

        

        private Camera camera;

        private TempLevel tempLevel;
        private Texture2D redsquare;
        public static Texture2D redsquareDebug;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            player = new Player(knightSpritesheet);
            skeleton = new Skeleton(skeletonTexture, player);
            wingedMob = new WingedMob(wingedMobTexture);
            tempLevel = new();

            camera = new Camera(GraphicsDevice.Viewport,player);


        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            knightSpritesheet = Content.Load<Texture2D>("Knight");
            redsquare = Content.Load<Texture2D>("square");
            redsquareDebug = Content.Load<Texture2D>("square");

            skeletonTexture = Content.Load<Texture2D>("Skeleton");
            wingedMobTexture = Content.Load<Texture2D>("Brain Mole");

            
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            player.Update(gameTime);
            skeleton.Update(gameTime);
            wingedMob.Update(gameTime);
            camera.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(transformMatrix: camera.CreateTransformation(GraphicsDevice));

            player.Draw(_spriteBatch);
            skeleton.Draw(_spriteBatch);
            wingedMob.Draw(_spriteBatch);
            tempLevel.Draw(_spriteBatch,redsquare);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}