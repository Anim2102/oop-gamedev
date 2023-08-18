using game_darksouls.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_darksouls.Levels
{
    public class BackgroundGame
{
        public Rectangle BackgroundDrawingRectangle { get; set; }
        public Texture2D BackGroundTexture { get; private set; }

        private Camera camera;
        private Viewport viewport;

        public BackgroundGame(Texture2D texture2D, Viewport viewPort, Camera camera)
        {
            BackgroundDrawingRectangle = Rectangle.Empty;
            BackGroundTexture = texture2D;
            this.viewport = viewPort;
            this.camera = camera;
        }

        public void Update()
        {
            BackgroundDrawingRectangle = new Rectangle((int)camera.CameraPosition.X - (viewport.Width / 2),
                (int)camera.CameraPosition.Y - (viewport.Height / 2),
                viewport.Width, viewport.Height);
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(BackGroundTexture,BackgroundDrawingRectangle,Color.White);
        }
    }
}
