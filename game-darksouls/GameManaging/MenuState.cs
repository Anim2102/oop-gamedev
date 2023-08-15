using game_darksouls.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace game_darksouls.GameManaging
{
    public class MenuState : IStateLevel
    {
       
        private Menu GameMenu { get; set; } 

        public MenuState(Menu gameMenu)
        {
            GameMenu = gameMenu;
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
