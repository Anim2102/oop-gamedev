using game_darksouls.Entity;
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

        public LevelOne(ContentManager contentManager, Viewport viewport) : base(Game1.dungeonTexture, @"Content\test.3.csv")
        {
            Player player = new Player(contentManager.Load<Texture2D>("Knight"));
            player.StartPosition(new Vector2(500, 700));
            entitys.Add(player);

            camera = new Camera(viewport, player);
        }

        public override void Update(GameTime gameTime)
        {
            camera.Update();
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: camera.CreateTransformation());
            base.Draw(spriteBatch);
        }
    }
}
