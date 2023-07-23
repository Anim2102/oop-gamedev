using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace game_darksouls.Input
{
    internal class InputManager
    {
        public InputManager() { }

        public Vector2 GetInput()
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
            return direction;
        }
    }
}
