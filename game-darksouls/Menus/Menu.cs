using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_darksouls.Menus
{
    public class Menu
{
        public void Update(GameTime gameTime)
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            Debug.WriteLine("in het meny");
            spriteBatch.Draw(Game1.redsquareDebug, new Rectangle(200, 200, 200, 200), Color.White);
            spriteBatch.End();
        }
}
}
