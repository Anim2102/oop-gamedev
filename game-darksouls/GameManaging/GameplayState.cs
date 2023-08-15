using game_darksouls.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_darksouls.GameManaging
{
    public class GameplayState : IStateLevel
    {
        private GameManager gameManager;
        private ILevel currentLevel;

        public GameplayState(GameManager gameManager)
        {
            this.gameManager = gameManager;


            //default first level for testing
            currentLevel = gameManager.Levels[0];

        }

        public void Play()
        {
            currentLevel = gameManager.Levels[0];
        }

        public void Stop()
        {
            currentLevel = null;
            gameManager.SetState(new MenuState(gameManager));
        }

        public void Update(GameTime gameTime)
        {
            currentLevel.Update(gameTime);

            if (currentLevel.IsComplete)
            {
                gameManager.SetState(new MenuState(gameManager));
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentLevel.Draw(spriteBatch);
        }
    }
}
