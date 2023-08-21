using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace game_darksouls.Input
{
    internal class InputManager : IInput
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

            return direction;
        }

        public bool PressedAttack()
        {
            var state = Keyboard.GetState();

            return state.IsKeyDown(Keys.A);


        }
        public bool IsJumpButtonPress()
        {
            bool jumped = Keyboard.GetState().IsKeyDown(Keys.Space);
            return jumped;
        }

    }
}
