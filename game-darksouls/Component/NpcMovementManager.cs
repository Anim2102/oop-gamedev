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

        private const float SPEED = 2F;
        private Vector2 direction;
        private bool onFloor;

        public NpcMovementManager(AnimatedObject animatedObject, CollisionManager collisionManager)
        {
            this.animatedObject = animatedObject;
            this.collisionManager = collisionManager;

            onFloor = false;
            direction = Vector2.Zero;
        }
        public void Update(GameTime gameTime)
        {
            CheckGravity();
            UpdatePosition(gameTime);
        }

        private void UpdatePosition(GameTime gameTime)
        {
            Rectangle updatedRectangle = animatedObject.drawingBox.DrawingRectangle;

            updatedRectangle.X += (int)(direction.X * SPEED * gameTime.ElapsedGameTime.Milliseconds);
            updatedRectangle.Y += (int)(direction.Y * SPEED * gameTime.ElapsedGameTime.Milliseconds);
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
                onFloor = true;
            }
            else
            {
                onFloor = false;
                direction.Y = 1;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle feetRectangle = new Rectangle(animatedObject.drawingBox.DrawingRectangle.X,
               animatedObject.drawingBox.DrawingRectangle.Y + animatedObject.drawingBox.DrawingRectangle.Height,
               animatedObject.drawingBox.DrawingRectangle.Width, 5);
            spriteBatch.Draw(Game1.redsquareDebug, feetRectangle, Color.Black);
        }
    }
}
