using game_darksouls.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_darksouls.GameManaging
{
    public class GameplayState : IStateLevel
    {
        private GameManager gameManager;
        private ILevel currentLevel;

        public GameplayState(GameManager gameManager, ILevel level)
        {
            this.gameManager = gameManager;

            //default first level for testing
            currentLevel = level;
        }

        public void Play()
        {
            
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
                if (gameManager.LevelManager.CheckLastLevel(currentLevel))
                {
                    gameManager.SetState(new MenuState(gameManager));
                }
                else
                {
                    gameManager.SetState(new GameplayState(gameManager,gameManager.LevelManager.GetNextLevel(currentLevel)));
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentLevel.Draw(spriteBatch);
        }
    }
}
