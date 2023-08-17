using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_darksouls.Menus
{
    public class BaseButton
{
        public Rectangle ButtonRectangle { get; set; }
        public bool ClickedButton { get; private set; }
        public string ButtonText { get; set; }
        public SpriteFont Font { get; set; }
        public Color BaseCollor { get; set; }
        public Color secondCollor { get; set; }
        public float TextSize { get; set; }
        public int ValueButton { get; set; }

        private Color currentColor = Color.White;

        public bool IsHover
        {
            get
            {
                return IsHovering();
            }
        }

        public bool IfClicked()
        {
            var mousePosition = Mouse.GetState();

            return (mousePosition.X < ButtonRectangle.X + ButtonRectangle.Width 
                && mousePosition.Y < ButtonRectangle.Y + ButtonRectangle.Height
                && mousePosition.LeftButton == ButtonState.Pressed);
        }

        private bool IsHovering()
        {
            var mousePosition = Mouse.GetState();

            return (mousePosition.X > ButtonRectangle.X &&
            mousePosition.X < ButtonRectangle.X + ButtonRectangle.Width &&
            mousePosition.Y > ButtonRectangle.Y &&
            mousePosition.Y < ButtonRectangle.Y + ButtonRectangle.Height);
        }
        public void Update(GameTime gameTime)
        {
            ClickedButton = IfClicked();

            if (IsHovering())
            {
                currentColor = secondCollor;
            }
            else
            {
                currentColor = BaseCollor;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, ButtonText, new Vector2(ButtonRectangle.X, ButtonRectangle.Y), currentColor, 0f, Vector2.Zero, TextSize, SpriteEffects.None, 0f); 
        }
    }
}
