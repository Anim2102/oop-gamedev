using game_darksouls.Component;
using game_darksouls.Level;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_darksouls.Entity
{
    public class Player : AnimatedObject, IEntity
    {
        private PlayerMovement playerMovement;
        private PlayerAnimation playerAnimation;

        public Player(Texture2D texturePlayer) {
            texture = texturePlayer;

            playerMovement = new(this);
            playerAnimation = new();

            drawingBox.DrawingRectangle = new Rectangle(1,1, 50,50);
            sourceBox.DrawingRectangle = new Rectangle(5, 10, 21, 26);


            //setup animations
            playerAnimation.AddAnimation()

        }
        public void Update(GameTime gameTime)
        {
            playerMovement.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,drawingBox.DrawingRectangle,sourceBox.DrawingRectangle,Color.White);
        }
    }
}
