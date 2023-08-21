using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace game_darksouls.GameManaging
{
    public class DeathState : IStateGame
    {
        private GameManager gameManager;
        private ContentManager contentManager;

        private SpriteFont font;
        private const string MESSAGE = "Still lost...lost forever";
        private Viewport viewport;

        private float waitTime = 0;
        private const float MAXWAITTIME = 5000f;

        private const float OffsetX = 0.4f;
        private const float OffsetY = 0.3f;

        public DeathState(GameManager gameManager)
        {
            this.gameManager = gameManager;
            this.contentManager = gameManager.ContentManager;
            this.viewport = gameManager.Viewport;   
            Play();
        }
        
        public void Play()
        {
            font = contentManager.Load<SpriteFont>("fonts/font");
        }

       

        private Vector2 CalculateTextPosition()
        {
            Vector2 centerScreen = new Vector2(viewport.Width / 2, viewport.Height / 2);
            Vector2 position = new Vector2(centerScreen.X * OffsetX, centerScreen.Y * OffsetY);
            return position;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, MESSAGE, CalculateTextPosition(), Color.White, 0f, Vector2.Zero, 4f, SpriteEffects.None, 0f); ;
            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {

            waitTime += gameTime.ElapsedGameTime.Milliseconds;

            if (waitTime > MAXWAITTIME)
            {
                gameManager.SetState(new MenuState(gameManager));

            }
        }

    }
}
