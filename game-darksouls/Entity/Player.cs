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

        public Player(Texture2D texturePlayer)
        {
            texture = texturePlayer;

            animationManager = new(AnimationFactory.LoadPlayerAnimations());
            playerMovement = new(this, animationManager);

            drawingBox.DrawingRectangle = new Rectangle(1, 1, 50, 50);
            
        }
        public void Update(GameTime gameTime)
        {
            playerMovement.Update(gameTime);
            animationManager.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, drawingBox.DrawingRectangle, animationManager.currentAnimation.CurrentFrame.SourceRectangle
               , Color.White, 0f,Vector2.Zero,animationManager.SpriteFLip,0f );
        }
    }
}
