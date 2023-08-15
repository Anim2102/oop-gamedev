﻿using Microsoft.Xna.Framework;
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
        private BaseButton button;

        public bool GetButtonPressed
        {
            get
            {
                return button.ClickedButton;
            }
        }

        public Menu()
        {
            button= new BaseButton();
            button.ButtonRectangle = new Rectangle(100, 100, 100, 100);
        }

        public void Update(GameTime gameTime)
        {
            button.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            button.Draw(spriteBatch); 
            spriteBatch.End();
        }
}
}
