using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_darksouls.Menus
{
    public class BaseButton
{
        public Rectangle ButtonRectangle { get; set; }
        public bool ClickedButton { get; private set; }


        public bool IfClicked()
        {
            var mousePosition = Mouse.GetState();

            return (mousePosition.X < ButtonRectangle.X + ButtonRectangle.Width 
                && mousePosition.Y < ButtonRectangle.Y + ButtonRectangle.Height
                && mousePosition.LeftButton == ButtonState.Pressed);
        }
        public void Update(GameTime gameTime)
        {
            ClickedButton = IfClicked();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.redsquareDebug, ButtonRectangle, Color.White);
        }
    }
}
