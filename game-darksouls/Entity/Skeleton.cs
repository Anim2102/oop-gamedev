using game_darksouls.Animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_darksouls.Entity
{
    public class Skeleton : AnimatedObject, IEntity
    {
        public Skeleton(Texture2D texture) {
            this.texture = texture;
            this.animationManager = new(AnimationFactory.LoadSkeletonAnimations());

            this.drawingBox.DrawingRectangle = new Rectangle(140, 60, 80, 80);
        }
      

        public void Update(GameTime gameTime)
        {
            animationManager.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, drawingBox.DrawingRectangle, animationManager.currentAnimation.CurrentFrame.SourceRectangle, Color.White);

        }
    }
}
