using game_darksouls.Animation;
using game_darksouls.Component;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_darksouls.Entity
{
    public abstract class AnimatedObject
    {
        protected Texture2D texture;
        protected Box drawingBox;
        protected Box collisionBox;

        public Box CollisionBox
        {
            get
            {
                return collisionBox;
            }
        }

      
        public AnimatedObject(Texture2D texture)
        {
            this.texture = texture;
        }

        public virtual void Update(GameTime gameTime)
        {
            drawingBox.UpdatePosition(collisionBox.Position);
        }
        public void StartPosition(Vector2 startPosition)
        {
            collisionBox.UpdatePosition(startPosition);
        }
    }
}
