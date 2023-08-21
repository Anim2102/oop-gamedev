using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace game_darksouls.GameManaging
{
    public class VictoryState : IStateGame
    {
        private GameManager gameManager;
        private ContentManager contentManager;

        private SpriteFont font;
        private const string MESSAGE = "One step closer to finding \nit but a win is a win";
        private Viewport viewport;


        private float waitTime = 0;
        private const float MAXWAITTIME = 5000f;

        public VictoryState(GameManager gameManager)
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
            waitTime += gameTime.ElapsedGameTime.Milliseconds;

            if (waitTime > MAXWAITTIME)
            {
                gameManager.SetState(new MenuState(gameManager));

            }



        }

    }
}
