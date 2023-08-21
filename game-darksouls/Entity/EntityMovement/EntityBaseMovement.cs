using game_darksouls.Animation;
using game_darksouls.Component;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_darksouls.Entity.EntityMovement
{
    public abstract class EntityBaseMovement
{
        protected readonly Box collisionBox;
        protected readonly CollisionManager collisionManager;
        protected readonly IAnimationManager animationManager;
        protected Vector2 direction;
        protected Vector2 speed = Vector2.Zero;

        public EntityBaseMovement(Box collisionBox, CollisionManager collisionManager, IAnimationManager animationManager)
        {
            this.collisionBox = collisionBox;
            this.collisionManager = collisionManager;
            this.animationManager = animationManager;
        }

        public virtual void Update(GameTime gameTime)
        {
            UpdatePosition(gameTime);
            ChangeFlipOnDirection();
        }
        private void UpdatePosition(GameTime gameTime)
        {
            Rectangle updatedRectangle = collisionBox.Rectangle;

            updatedRectangle.X += (int)(direction.X * speed.X * gameTime.ElapsedGameTime.Milliseconds);
            updatedRectangle.Y += (int)(direction.Y * speed.Y * gameTime.ElapsedGameTime.Milliseconds);
            MoveWithCollision(updatedRectangle);

        }

        protected void MoveWithCollision(Rectangle futurePosition)
        {
            if (!collisionManager.CheckForCollision(futurePosition))
            {
                Vector2 future = new Vector2(futurePosition.X, futurePosition.Y);
                collisionBox.UpdatePosition(future);
            }
        }

        private void ChangeFlipOnDirection()
        {
            if (direction.X > 0)
            {
                animationManager.FacingLeft = false;
            }
            else if (direction.X < 0)
            {
                animationManager.FacingLeft = true;
            }
        }
    }
}
