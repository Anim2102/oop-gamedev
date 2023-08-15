using game_darksouls.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_darksouls.GameManaging
{
    public class MenuState : IStateLevel
    {
        private Menu GameMenu { get; set; }
        private GameManager gameManager;


        private bool waiting = false;
        private double waitStartTime;
        private double waitDuration = 2.0;

        public MenuState(Menu gameMenu, GameManager gameManager)
        {
            GameMenu = gameMenu;
            this.gameManager = gameManager;
        }

        public void Start()
        {

        }
        public void Stop()
        {

        }

        public void Update(GameTime gameTime)
        {
            GameMenu.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            GameMenu.Draw(spriteBatch);
        }
    }
}
