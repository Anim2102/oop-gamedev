using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        private Viewport viewport;

        private Texture2D backgroundTexture;
        private Rectangle backGroundDrawingRectangle;

        private const int WIDTH = 50;
        private const int HEIGHT = 50;

        public bool GetButtonPressed
        {
            get
            {
                return buttons[0].ClickedButton;
            }
        }

        public Menu(Viewport viewport,ContentManager contentManager)
        {
            this.viewport = viewport;
            SpriteFont font = contentManager.Load<SpriteFont>("fonts/font");

            backgroundTexture = contentManager.Load<Texture2D>("menu background");
            backGroundDrawingRectangle = new Rectangle(0,0,viewport.Width,viewport.Height);

            BaseButton button = new BaseButton();
            button.ButtonRectangle = new Rectangle((viewport.Width-WIDTH)/2, (viewport.Height - HEIGHT) / 2 - HEIGHT, WIDTH, HEIGHT);
            button.Font = font;
            button.ButtonText = "level 1";
            button.BaseCollor = Color.White;
            button.secondCollor = Color.Red;
            button.TextSize = 2.5f;

            BaseButton secondButton = new BaseButton();
            secondButton.ButtonRectangle = new Rectangle(((viewport.Width - WIDTH) / 2), (viewport.Height + HEIGHT) / 2, WIDTH, HEIGHT);
            secondButton.Font = font;
            secondButton.ButtonText = "level 2";
            secondButton.BaseCollor = Color.White;
            secondButton.secondCollor = Color.Red;
            secondButton.TextSize = 2.5f;


            buttons.Add(button);
            buttons.Add(secondButton);
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
            spriteBatch.Draw(backgroundTexture, backGroundDrawingRectangle ,Color.White);
            foreach (var button in buttons)
            {
                button.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
}
}
