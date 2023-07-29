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

        private Viewport viewport;
        private Player player;

        public Camera(int x, int y, int width, int height, Player player)
        {
            viewport = new Viewport(x, y, width, height);
            this.player = player;

        }
        public Matrix CreateTransformation(GraphicsDevice graphicsDevice)
        {
            var newTransForm = Matrix.CreateTranslation(new Vector3(-CameraPosition.X, -CameraPosition.Y, 0)) *

                                         Matrix.CreateTranslation(new Vector3(viewport.Width * 0.5f, viewport.Height * 0.5f, 0));
            return newTransForm;
        }
        public void Update(GameTime gameTime)
        {
            Vector2 newCameraPosition = CameraPosition;
            newCameraPosition.X = this.player.drawingBox.DrawingRectangle.X;
            newCameraPosition.Y = this.player.drawingBox.DrawingRectangle.Y;
            CameraPosition = newCameraPosition;
        }
    }

}
