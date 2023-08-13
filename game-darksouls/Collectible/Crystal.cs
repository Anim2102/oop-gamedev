using game_darksouls.Animation;
using game_darksouls.Component;
using game_darksouls.Enum;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace game_darksouls.Collectible
{
    public class Crystal : ICollectible
    {
        private Texture2D texture;
        private AnimationManager animationManager;
        private Box collisionBox;
        private Box drawingBox;
        private bool isCollected;

        public bool IsCollected
        {
            get
            {
                return isCollected;
            }
        }

        public Box CollisionBox
        {
            get { return collisionBox; }
        }

        public Crystal(Texture2D crystalTexture, Vector2 position)
        {
            texture = crystalTexture;
            animationManager = new AnimationManager(AnimationFactory.LoadCrystalAnimations());
            collisionBox = new Box((int)position.X, (int)position.Y, 30, 64);
            drawingBox = new Box((int)position.X, (int)position.Y - 50, 64, 64);
            drawingBox.Offset = new Vector2(-18, 0);
            animationManager.PlayAnimation(MovementState.IDLE);
            isCollected = false;
        }

        public void CollectedGem()
        {
            isCollected = true;
        }

        public void Update(GameTime gameTime)
        {
            drawingBox.UpdatePosition(collisionBox.Position);
            animationManager.Update(gameTime);
            Debug.WriteLine(isCollected);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Game1.redsquareDebug, collisionBox.Rectangle, Color.Red);
            spriteBatch.Draw(texture, drawingBox.Rectangle, animationManager.CurrentAnimation.CurrentFrame.SourceRectangle, Color.White);
        }
    }
}
