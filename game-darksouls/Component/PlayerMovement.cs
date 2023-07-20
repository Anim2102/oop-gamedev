using game_darksouls.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace game_darksouls.Component
{
    internal class PlayerMovement : IComponent
    {
        private readonly Player player;

        public PlayerMovement(Player player)
        {
            this.player = player;
        }
        public void Update(GameTime gameTime)
        {
            var state = Keyboard.GetState();
            Vector2 direction = Vector2.Zero;

            if (state.IsKeyDown(Keys.Left))
            {
                direction.X = -1;
            }
            if (state.IsKeyDown(Keys.Right))
            {
                direction.X = +1;
            }
            if (state.IsKeyDown(Keys.Up))
            {
                direction.Y = -1;
            }
            if (state.IsKeyDown(Keys.Down))
            {
                direction.Y = +1;
            }

            Rectangle updatedRectangle = player.drawingBox.DrawingRectangle;
            updatedRectangle.X += (int)direction.X;
            updatedRectangle.Y += (int)direction.Y;
            player.drawingBox.DrawingRectangle = updatedRectangle;
        }
    }
}
