using game_darksouls.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_darksouls.Utilities
{
    internal class Camera
    {
        //source of knowledge: http://www.david-amador.com/2009/10/xna-camera-2d-with-zoom-and-rotation/
        public Matrix Transform { get; set; }
        public Vector2 CameraPosition { get; set; }
        public Vector2 OriginCenter { get; set; }

        private Viewport viewport;
        private AnimatedObject player;

        private const int WIDTH_OFFSET = 160;
        private const int HEIGHT_OFFSET = 100;

        public Camera(Viewport viewport, AnimatedObject player)
        {
            this.viewport = viewport; 
            this.player = player;

            OriginCenter = new Vector2((viewport.Width / 2)-player.CollisionBox.Rectangle.Width - WIDTH_OFFSET, (viewport.Height / 2)-player.CollisionBox.Rectangle.Height - HEIGHT_OFFSET);

        }
        public Matrix CreateTransformation()
        {
            //negative cameraposition for garanty opposite direction 
            var newTransform = Matrix.CreateTranslation(new Vector3(-CameraPosition + OriginCenter, 0)) *
                Matrix.CreateScale(new Vector3(1.3f, 1.3f, 1));

            return newTransform;
        }
        public void Update()
        {

            float cameraX = player.CollisionBox.Position.X;
            float cameraY = player.CollisionBox.Position.Y;

            CameraPosition = new Vector2(cameraX, cameraY);
        }


        
        public void Draw(SpriteBatch spriteBatch)
        {
            /*
            float cameraX = player.CollisionBox.Position.X;
            float cameraY = player.CollisionBox.Position.Y;

            spriteBatch.Draw(Game1.redsquareDebug, new Vector2(cameraX, cameraY), Color.Red);
            spriteBatch.Draw(Game1.redsquareDebug, OriginCenter, Color.Red);
            */
        }
        
    }

}
