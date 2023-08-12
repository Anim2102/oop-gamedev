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
        private Player player;

        public Camera(Viewport viewport, Player player)
        {
            this.viewport = viewport; 
            this.player = player;

            OriginCenter = new Vector2(viewport.Width / 2, viewport.Height / 2);

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
            CameraPosition = this.player.DrawingBox.CenterOfBox();
        }
    }

}
