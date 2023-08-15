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

        private bool waiting = false;
        private double waitStartTime;
        private double waitDuration = 2.0;

        public GameplayState(GameManager gameManager) 
        {
            this.gameManager = gameManager;
            Play();
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
            if (waiting)
            {
                double elapsedSeconds = (gameTime.TotalGameTime.TotalMilliseconds - waitStartTime) / 1000.0;

                if (elapsedSeconds >= waitDuration)
                {
                    waiting = false;
                    currentLevel= gameManager.Levels[1];
                }
            }
            else
            {
                waiting = true;
                waitStartTime = gameTime.TotalGameTime.TotalMilliseconds;
            }

            currentLevel.Update(gameTime);  
        }

        public void Draw(SpriteBatch spriteBatch)
        {
           currentLevel.Draw(spriteBatch);
        }
    }
}
