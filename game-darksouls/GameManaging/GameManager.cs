using game_darksouls.Level;
using game_darksouls.Levels;
using game_darksouls.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        
        public Menu GameMenu { get; set; }

        private ContentManager contentManager;

        public ContentManager ContentManager
        {
            get { return contentManager; }
        }
        private Viewport viewport;
        public Viewport Viewport { get { return viewport; } }
        private IStateGame currentState;

        public GameManager(Viewport viewport,ContentManager contentManager) {
            this.viewport = viewport;
            this.contentManager = contentManager;
            GameMenu = new Menu(viewport,contentManager);
            
        }

        public void AddLevel(ILevel newLevel)
        {
            LevelManager.GetInstance().AddLevel(newLevel);
        }

        public void SetState(IStateGame stateLevel)
        {
            currentState = stateLevel;
            currentState.Play();
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
