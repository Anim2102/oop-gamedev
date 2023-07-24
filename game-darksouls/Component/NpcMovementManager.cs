using game_darksouls.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace game_darksouls.Component
{
    internal class NpcMovementManager : IComponent
    {
        private readonly AnimatedObject animatedObject;
        private readonly CollisionManager collisionManager;

        private Vector2 SPEED = new Vector2(0.2f, 0.4f);
        private Vector2 direction;

        public NpcMovementManager(AnimatedObject animatedObject, CollisionManager collisionManager)
        {
            this.animatedObject = animatedObject;
            this.collisionManager = collisionManager;

            direction = Vector2.Zero;
            direction.X = 1;
        }
        public void Update(GameTime gameTime)
        {
            CheckGravity();
            UpdatePosition(gameTime);
            Debug.WriteLine(direction);
        }

        private void UpdatePosition(GameTime gameTime)
        {
            Rectangle updatedRectangle = animatedObject.drawingBox.DrawingRectangle;

            updatedRectangle.X += (int)(direction.X * SPEED.X * gameTime.ElapsedGameTime.Milliseconds);
            updatedRectangle.Y += (int)(direction.Y * SPEED.Y * gameTime.ElapsedGameTime.Milliseconds);
            animatedObject.drawingBox.DrawingRectangle = updatedRectangle;
            animatedObject.drawingBox.DrawingRectangle = updatedRectangle;
        }

        private void CheckGravity()
        {
            Rectangle feetRectangle = new Rectangle(animatedObject.drawingBox.DrawingRectangle.X,
               animatedObject.drawingBox.DrawingRectangle.Y + animatedObject.drawingBox.DrawingRectangle.Height,
               animatedObject.drawingBox.DrawingRectangle.Width, 5);
            
            if (collisionManager.CheckForCollision(feetRectangle))
            {
                direction.Y = 0;
            }
            else
            {
                direction.Y = 1;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //debugging
            Rectangle feetRectangle = new Rectangle(animatedObject.drawingBox.DrawingRectangle.X,
               animatedObject.drawingBox.DrawingRectangle.Y + animatedObject.drawingBox.DrawingRectangle.Height,
               animatedObject.drawingBox.DrawingRectangle.Width, 5);
            spriteBatch.Draw(Game1.redsquareDebug, feetRectangle, Color.Black);
        }
    }
}
