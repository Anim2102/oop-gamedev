using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_darksouls.Menus
{
    public class Menu
{
        private List<BaseButton> buttons = new();

        public bool GetButtonPressed
        {
            get
            {
                return buttons[0].ClickedButton;
            }
        }

        public Menu()
        {
            BaseButton button = new BaseButton();
            button.ButtonRectangle = new Rectangle(100, 100, 100, 100);
            BaseButton buttonSecond = new BaseButton();
            buttonSecond.ButtonRectangle = new Rectangle(200, 200, 100, 100);

            buttons.Add(button);
            buttons.Add(buttonSecond);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var button in buttons)
            {
                button.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (var button in buttons)
            {
                button.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
}
}
