using game_darksouls.Collectible;
using game_darksouls.Entity;
using game_darksouls.GameManaging;
using game_darksouls.Level;
using game_darksouls.Levels;
using game_darksouls.Levels.worlds;
using game_darksouls.Menus;
using game_darksouls.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.Diagnostics;

namespace game_darksouls
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static Texture2D redsquareDebug;

        public static Texture2D dungeonTexture;

        private ILevel firstLevel;
        private ILevel secondLevel;

        private GameManager gameManager;
        private LevelManager levelManager;
        Song testaudi;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 700;
            _graphics.ApplyChanges();


            Debug.WriteLine(Content.RootDirectory);
            // TODO: Add your initialization logic here
            base.Initialize();
            
            firstLevel = (ILevel)new LevelOne(Content,GraphicsDevice.Viewport);
            secondLevel = (ILevel)new LevelTwo(Content,GraphicsDevice.Viewport);

            //testaudi = Content.Load<Song>("sounds/Dark Ambient 3");
            //MediaPlayer.Play(testaudi);

            levelManager = new LevelManager();
            levelManager.AddLevel(firstLevel);
            levelManager.AddLevel(secondLevel);

            gameManager = new GameManager(levelManager,GraphicsDevice.Viewport,Content);
            gameManager.SetState(new MenuState(gameManager,Content));
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            redsquareDebug = Content.Load<Texture2D>("square");
            dungeonTexture = Content.Load<Texture2D>("Dungeon Tile Set");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            gameManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            gameManager.Draw(_spriteBatch);
            base.Draw(gameTime);
        }

        
    }
}