using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_darksouls.Menus
{
    public class BaseButton
{
        public Rectangle Button { get; set; }

        public bool IfClicked()
        {
            var mousePosition = Mouse.GetState();

            return (mousePosition.X < Button.X + Button.Width && mousePosition.Y < Button.Y + Button.Height
                && mousePosition.LeftButton == ButtonState.Pressed);
        }
        public void Update(GameTime gameTime)
        {
            IfClicked();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.redsquareDebug, Button, Color.White);
        }
    }
}
