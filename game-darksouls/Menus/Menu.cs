using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

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

        private const string TITLE = "Lost Knight";
        private SpriteFont font;
        private Vector2 titlePosition;

        public bool GetButtonPressed
        {
            get
            {
                return buttons.Any(button => button.ClickedButton);
            }
        }

        public int ButtonValue()
        {
            foreach (var button in buttons)
            {
                if (button.ClickedButton)
                    return button.ValueButton;
            }
            return 0;
        }



        public Menu(Viewport viewport, ContentManager contentManager)
        {
            this.viewport = viewport;
            font = contentManager.Load<SpriteFont>("fonts/font");

            backgroundTexture = contentManager.Load<Texture2D>("menu background");
            backGroundDrawingRectangle = new Rectangle(0, 0, viewport.Width, viewport.Height);

            titlePosition = CalculateTextPosition();

            BaseButton button = new BaseButton();
            button.ButtonRectangle = new Rectangle((viewport.Width - WIDTH) / 2, (viewport.Height - HEIGHT) / 2 - HEIGHT, WIDTH, HEIGHT);
            button.Font = font;
            button.ButtonText = "level 1";
            button.BaseCollor = Color.White;
            button.secondCollor = Color.Red;
            button.TextSize = 2.5f;
            button.ValueButton = 1;

            BaseButton secondButton = new BaseButton();
            secondButton.ButtonRectangle = new Rectangle(((viewport.Width - WIDTH) / 2), (viewport.Height + HEIGHT) / 2, WIDTH, HEIGHT);
            secondButton.Font = font;
            secondButton.ButtonText = "level 2";
            secondButton.BaseCollor = Color.White;
            secondButton.secondCollor = Color.Red;
            secondButton.TextSize = 2.5f;
            button.ValueButton = 0;


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
        private Vector2 CalculateTextPosition()
        {
            Vector2 centerScreen = new Vector2(viewport.Width / 2,viewport.Height / 2);
            Vector2 position = new Vector2(centerScreen.X * 0.8f, centerScreen.Y * 0.3f);
            return position;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, backGroundDrawingRectangle, Color.White);
            spriteBatch.DrawString(font, TITLE, titlePosition, Color.White, 0f, Vector2.Zero, 4f, SpriteEffects.None, 0f); ;

            foreach (var button in buttons)
            {
                button.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
