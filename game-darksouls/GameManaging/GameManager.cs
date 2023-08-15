using game_darksouls.Level;
using game_darksouls.Levels;
using game_darksouls.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_darksouls.GameManaging
{
    public class GameManager
{
        public List<ILevel> Levels { get; set; } = new();
        public Menu GameMenu { get; set; }

        private IStateLevel currentState;

        public GameManager(MenuState menuState)
        {
            currentState = menuState;
        }
        public GameManager() {
            GameMenu = new Menu();
        }

        public void AddLevel(ILevel newLevel)
        {
            Levels.Add(newLevel);
        }

        public void SetState(IStateLevel stateLevel)
        {
            currentState = stateLevel;
        }

        public void Update(GameTime gameTime)
        {
            currentState.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentState.Draw(spriteBatch);
        }
    }
}
