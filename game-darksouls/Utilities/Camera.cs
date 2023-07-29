using game_darksouls.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_darksouls.Utilities
{
    internal class Camera
    {
        public Matrix Matrix { get; set; }
        public Vector2 CameraPosition { get; set; }

        private Viewport viewport;
        private Player player;

        public Camera(int x,int y, int width, int height, Player player)
        {
            viewport = new Viewport(x,y,width,height);
            this.player = player;
        }

        public void Update(GameTime gameTime)
        {

        }
    }
}
