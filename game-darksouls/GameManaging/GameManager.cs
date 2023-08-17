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
        public LevelManager LevelManager { get; private set; }
        public Menu GameMenu { get; set; }

        private IStateGame currentState;

        public GameManager(LevelManager levelManager) {
            GameMenu = new Menu();
            this.LevelManager = levelManager;
        }

        public void AddLevel(ILevel newLevel)
        {
            LevelManager.AddLevel(newLevel);
        }

        public void SetState(IStateGame stateLevel)
        {
            currentState = stateLevel;
            currentState.Start();
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
