using game_darksouls.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace game_darksouls.GameManaging
{
    public class GameplayState : IStateGame
    {
        private GameManager gameManager;
        private ILevel currentLevel;
        private ContentManager contentManager;

        private int currentLevelIndex;

        public GameplayState(GameManager gameManager,int levelIndex)
        {
            this.gameManager = gameManager;
            this.currentLevelIndex = levelIndex;
            this.contentManager = gameManager.ContentManager;
            
            Play();
            //default first level for testing
        }

        public void Play()
        {
            currentLevel = gameManager.LevelManager.GetLevelByIndex(currentLevelIndex);
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
                    //increment last level index for getting the next one
                    gameManager.SetState(new GameplayState(gameManager, currentLevelIndex++));
                }
            }

            if (currentLevel.IsLost)
            {
                gameManager.SetState(new DeathState(gameManager));
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentLevel.Draw(spriteBatch);
        }
    }
}
