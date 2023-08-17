using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_darksouls.Component
{
    public class FlyingObject
{
        public Rectangle Object { get; set; }

        private int size;
        private Texture2D textureObject;
        
        public FlyingObject(Texture2D objectTexture,int size)
        {
            this.textureObject = objectTexture;
            this.size = size;
        }

        public void RemoveObjectScreen()
        {
            this.Object = Rectangle.Empty;
        }
        public void ResetPosition(Vector2 position)
        {
            Object = new Rectangle((int)position.X, (int)position.Y, size, size);

        }

     

        public void UpdatePositionObject(GameTime gameTime,Vector2 direction)
        {
            Vector2 normalizedDirection = Vector2.Normalize(direction);
            Rectangle updatedRectangle = Object;

            updatedRectangle.X += (int)(normalizedDirection.X * 0.5f * gameTime.ElapsedGameTime.Milliseconds);
            updatedRectangle.Y += (int)(normalizedDirection.Y * 0.5f * gameTime.ElapsedGameTime.Milliseconds);
            Object = updatedRectangle;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureObject, Object, Color.White);
        }

    }
}
