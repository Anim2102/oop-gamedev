using game_darksouls.Animation;
using game_darksouls.Component;
using game_darksouls.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_darksouls.Collectible
{
    public class Crystal : AnimatedObject, IEntity
    {
        public Crystal(Texture2D crystalTexture)
        {
            texture = crystalTexture;
            animationManager = new AnimationManager(AnimationFactory.LoadCrystalAnimations());
            collisionBox = new Box(2405, 820, 30, 64);
            drawingBox = new Box(0, 0, 64, 64);
            drawingBox.Offset = new Vector2(-18,0);

            animationManager.PlayAnimation(Enum.MovementState.IDLE);
        }

        public void Update(GameTime gameTime)
        {
            drawingBox.UpdatePosition(collisionBox.Position);
            animationManager.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Game1.redsquareDebug, collisionBox.Rectangle, Color.Red);
            spriteBatch.Draw(texture,
                drawingBox.Rectangle,
                animationManager.currentAnimation.CurrentFrame.SourceRectangle,
                Color.White,
                0f,
                Vector2.Zero,
                animationManager.SpriteFLip,
                0f);
        }
    }
}
