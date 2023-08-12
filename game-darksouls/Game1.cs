﻿using game_darksouls.Collectible;
using game_darksouls.Entity;
using game_darksouls.Level;
using game_darksouls.Levels.worlds;
using game_darksouls.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

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
        public static Skeleton skeleton;

        private Texture2D wingedMobTexture;
        public  static WingedMob wingedMob;

        private Texture2D wizardTexture;
        public  static Wizard wizard;
        private Texture2D wizardFireBall;

        

        private LevelOne firstLevel;
        //private LevelOneTemp tempLevel;
        public static Texture2D dungeonTexture;

        //private LevelOneTemp levelOne;

        private Texture2D redsquare;
        public static Texture2D redsquareDebug;

        private Texture2D crystalTexture;
        private Crystal crystal;

        private List<IEntity> entities;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            

            Debug.WriteLine(Content.RootDirectory);
            // TODO: Add your initialization logic here
            base.Initialize();
            player = new Player(knightSpritesheet);
            skeleton = new Skeleton(skeletonTexture, player);
            wingedMob = new WingedMob(wingedMobTexture, player);
            wizard = new Wizard(wizardTexture,wizardFireBall, player);
            crystal = new Crystal(crystalTexture);
            //tempLevel = new();
            entities = new List<IEntity>();

            firstLevel = new LevelOne(Content,GraphicsDevice.Viewport);

            


        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            knightSpritesheet = Content.Load<Texture2D>("Knight");
            redsquare = Content.Load<Texture2D>("square");
            redsquareDebug = Content.Load<Texture2D>("square");

            skeletonTexture = Content.Load<Texture2D>("Skeleton");
            wingedMobTexture = Content.Load<Texture2D>("Brain Mole");
            wizardTexture = Content.Load<Texture2D>("wizard");
            wizardFireBall = Content.Load<Texture2D>("fireball");

            crystalTexture = Content.Load<Texture2D>("crystal");

            dungeonTexture = Content.Load<Texture2D>("Dungeon Tile Set");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            //wingedMob.Update(gameTime);
            // TODO: Add your update logic here
            //player.Update(gameTime);
            //skeleton.Update(gameTime);
            //wizard.Update(gameTime);
            //crystal.Update(gameTime);
            firstLevel.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            firstLevel.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}