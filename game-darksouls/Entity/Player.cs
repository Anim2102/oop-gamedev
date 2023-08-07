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

            animationManager = new AnimationManager(AnimationFactory.LoadPlayerAnimations());
            playerMovement = new(this,new CollisionManager(),animationManager,new());

            collisionBox = new Box(350, 600, 30, 40);
            drawingBox.Rectangle = new Rectangle(0, 0, 50, 50);
            drawingBox.Offset = new Vector2(-10, -10);
            
        }
        public void Update(GameTime gameTime)
        {
            drawingBox.UpdatePosition(collisionBox.Position);
            playerMovement.Update(gameTime);
            animationManager.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Game1.redsquareDebug, collisionBox.Rectangle, Color.Red);
            //spriteBatch.Draw(Game1.redsquareDebug, drawingBox.Rectangle, Color.Red);
            playerMovement.Draw(spriteBatch);
            spriteBatch.Draw(texture, 
                drawingBox.Rectangle,
                animationManager.currentAnimation.CurrentFrame.SourceRectangle,
                Color.White, 
                0f,
                Vector2.Zero,
                animationManager.SpriteFLip,
                0f );
        }
    }
}
