using game_darksouls.Levels;
using game_darksouls.Sound;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace game_darksouls.GameManaging
{
    public class GameplayState : IStateGame
    {
        private GameManager gameManager;
        private ILevel currentLevel;
        private ContentManager contentManager;
        private IBackGroundPlayer soundManager;

        private Song backGroundSong;

        private int currentLevelIndex;

        public GameplayState(GameManager gameManager,int levelIndex)
        {
            this.gameManager = gameManager;
            this.currentLevelIndex = levelIndex;
            this.contentManager = gameManager.ContentManager;
            this.soundManager = new SoundManager();
            Play();
        }

        public void Play()
        {
            MediaPlayer.Stop();

            currentLevel = LevelManager.GetInstance().GetLevelByIndex(currentLevelIndex);
            currentLevel.Reset();
            backGroundSong = contentManager.Load<Song>("sounds/ambient");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.1f;
            soundManager.PlayBackGroundSong(backGroundSong);
        }


        public void Update(GameTime gameTime)
        {
            currentLevel.Update(gameTime);

            if (currentLevel.IsComplete)
            {
                if (LevelManager.GetInstance().CheckLastLevel(currentLevel))
                {
                    
                    gameManager.SetState(new VictoryState(gameManager));
                }

                else
                {
                    //increment last level index for getting the next one
                    currentLevel.Reset();
                    gameManager.SetState(new GameplayState(gameManager, currentLevelIndex += 1));
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
