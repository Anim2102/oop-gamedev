using game_darksouls.Menus;
using game_darksouls.Sound;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace game_darksouls.GameManaging
{
    public class MenuState : IStateGame
    {
        private Menu gameMenu;
        private GameManager gameManager;
        private IBackGroundPlayer soundManager;
        private ContentManager contentManager;
        private Viewport viewport;

        private Song backgroundSong;

        public MenuState(GameManager gameManager,ContentManager contentManager)
        {
            this.gameManager = gameManager;
            this.contentManager = contentManager;
        }

        public void Start()
        {
            gameMenu = new Menu(gameManager.Viewport,contentManager);
            soundManager = new SoundManager();
        
            this.backgroundSong = contentManager.Load<Song>("sounds/Dungeon Theme");
            soundManager.PlayBackGroundSong(backgroundSong);
        }
        public void Stop()
        {
            soundManager = null;
            contentManager = null;
            MediaPlayer.Stop();
        }

        public void Update(GameTime gameTime)
        {
            gameMenu.Update(gameTime);

            if (gameMenu.GetButtonPressed)
            {
                int buttonValue = gameMenu.ButtonValue();
                gameManager.SetState(new GameplayState(gameManager, gameManager.LevelManager.GetLevelByIndex(buttonValue), contentManager));
                Stop();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            gameMenu.Draw(spriteBatch);
        }
    }
}
