using game_darksouls.Level;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace game_darksouls.GameManaging
{
    public class GameplayState : IStateLevel
    {
        private GameManager gameManager;
        private LevelSetup currentLevel;

        public GameplayState(GameManager gameManager) 
        {
            this.gameManager = gameManager;
            
        }
        public void Play()
        {
            currentLevel = gameManager.Levels[0];
        }
        public void Stop()
        {
            currentLevel= null;
            gameManager.SetState(new MenuState(gameManager.GameMenu,gameManager));
        }

        public void Update(GameTime gameTime)
        {
            

            currentLevel.Update(gameTime);  
        }

        public void Draw(SpriteBatch spriteBatch)
        {
           currentLevel.Draw(spriteBatch);
        }
    }
}
