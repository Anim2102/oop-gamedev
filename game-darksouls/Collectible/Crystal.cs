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
            Texture = crystalTexture;
            AnimationManager = new AnimationManager(AnimationFactory.LoadCrystalAnimations());
            CollisionBox = new Box(2405, 820, 30, 64);
            DrawingBox = new Box(0, 0, 64, 64);
            DrawingBox.Offset = new Vector2(-18,0);

            AnimationManager.PlayAnimation(Enum.MovementState.IDLE);
        }

        public void Update(GameTime gameTime)
        {
            DrawingBox.UpdatePosition(CollisionBox.Position);
            AnimationManager.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Game1.redsquareDebug, collisionBox.Rectangle, Color.Red);
            spriteBatch.Draw(Texture,
                DrawingBox.Rectangle,
                AnimationManager.currentAnimation.CurrentFrame.SourceRectangle,
                Color.White,
                0f,
                Vector2.Zero,
                AnimationManager.SpriteFLip,
                0f);
        }
    }
}
