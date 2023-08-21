using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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

        public void Stop()
        {
            contentManager.Unload();
        }

        private Vector2 CalculateTextPosition()
        {
            Vector2 centerScreen = new Vector2(viewport.Width / 2, viewport.Height / 2);
            Vector2 position = new Vector2(centerScreen.X * 0.8f, centerScreen.Y * 0.3f);
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
            
        }

    }
}
