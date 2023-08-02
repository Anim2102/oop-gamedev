using game_darksouls.Animation;
using game_darksouls.Component;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_darksouls.Entity
{
    public class WingedMob : AnimatedObject, IEntity
    {
        public WingedMob(Texture2D texture)
        {
            this.texture = texture;
            this.collisionBox = new Box(50, 50, 50, 50,Vector2.Zero);
            this.drawingBox = new Box(50, 50, 50, 50, Vector2.Zero);
            this.animationManager = new AnimationManager(AnimationFactory.LoadBrainMobAnimations());

        }

        public void Update(GameTime gameTime)
        {
            this.animationManager.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Game1.redsquareDebug, drawingBox.Rectangle, Color.Green);
            //spriteBatch.Draw(Game1.redsquareDebug, collisionBox.Rectangle, Color.Green);


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
