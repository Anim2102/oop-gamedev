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

        public GameplayState(GameManager gameManager, ILevel level,ContentManager contentManager)
        {
            this.gameManager = gameManager;
            this.contentManager = contentManager;
            //default first level for testing
            currentLevel = level;
        }

        public void Play()
        {
            
        }

        public void Stop()
        {
            currentLevel = null;
            gameManager.SetState(new MenuState(gameManager,contentManager));
        }

        public void Update(GameTime gameTime)
        {
            currentLevel.Update(gameTime);

            if (currentLevel.IsComplete)
            {
                if (gameManager.LevelManager.CheckLastLevel(currentLevel))
                {
                    gameManager.SetState(new MenuState(gameManager,contentManager));
                }
                else
                {
                    gameManager.SetState(new GameplayState(gameManager,gameManager.LevelManager.GetNextLevel(currentLevel),contentManager));
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentLevel.Draw(spriteBatch);
        }
    }
}
