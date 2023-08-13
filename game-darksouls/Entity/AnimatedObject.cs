

using Component.Health;
using game_darksouls.Component;
using game_darksouls.Entity.EntityMovement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_darksouls.Entity
{
    public abstract class AnimatedObject
    {
        public Texture2D Texture { get; set; }
        public AnimationManager AnimationManager { get; set; }
        public Health HealthManager { get; set; }
        public CollisionManager CollisionManager { get; set; }
        public IMovementBehaviour MovementBehaviour { get; set; }
        public Box DrawingBox { get; set; }
        public Box CollisionBox { get; set; }

        public AnimatedObject(CollisionManager collisionManager)
        {
            this.CollisionManager = collisionManager;
        }
        public virtual void Update(GameTime gameTime)
        {
            DrawingBox.UpdatePosition(CollisionBox.Position);
        }
        public void StartPosition(Vector2 startPosition)
        {
            this.CollisionBox.UpdatePosition(startPosition);
        }

        public virtual void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(Texture,
                DrawingBox.Rectangle,
                AnimationManager.CurrentAnimation.CurrentFrame.SourceRectangle,
                HealthManager.CurrentColor,
                0f,
                Vector2.Zero,
                AnimationManager.SpriteFLip,
                0f);
        }

    }
}
