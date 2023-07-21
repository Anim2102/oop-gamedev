using game_darksouls.Animation;
using game_darksouls.Component;
using game_darksouls.Enum;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_darksouls.Entity
{
    public class Player : AnimatedObject, IEntity
    {
        private PlayerMovement playerMovement;
        private PlayerAnimation playerAnimation;

        public Player(Texture2D texturePlayer)
        {
            texture = texturePlayer;

            playerAnimation = new();
            playerMovement = new(this, playerAnimation);

            drawingBox.DrawingRectangle = new Rectangle(1, 1, 50, 50);
            sourceBox.DrawingRectangle = new Rectangle(5, 10, 21, 26);

            
        }
        public void Update(GameTime gameTime)
        {
            playerMovement.Update(gameTime);
            playerAnimation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, drawingBox.DrawingRectangle, playerAnimation.currentAnimation.CurrentFrame.SourceRectangle, Color.White);
        }
    }
}
