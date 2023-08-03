using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using game_darksouls.Animation;
using game_darksouls.Component;
using game_darksouls.Entity.EntityMovement;
using game_darksouls.Entity.Behaviour;

namespace game_darksouls.Entity
{
    public class StaticArcher : AnimatedObject, IEntity
    {
        
        private IMovementBehaviour movementBehaviour;

        private RangeAttack rangeAttack;

        public StaticArcher(Texture2D texture, Player player)
        {
            this.texture = texture;
            this.animationManager = new AnimationManager(AnimationFactory.LoadArcherAnimations());

            this.drawingBox = new Box(170, 20, 64 * 2, 64 * 2, new Vector2(-25, -25));
            this.collisionBox = new Box(170, 20, 64, 75, new Vector2(0, 0));

            this.movementBehaviour = new GroundMovement(new CollisionManager(), animationManager, collisionBox);

            rangeAttack = new RangeAttack(player, this, animationManager, movementBehaviour);
              
        }

        public void Update(GameTime gameTime)
        {
            this.drawingBox.UpdatePosition(collisionBox.Position);  
            this.animationManager.Update(gameTime);
            this.movementBehaviour.Update(gameTime);

            rangeAttack.Behave(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Game1.redsquareDebug, drawingBox.Rectangle, Color.Green);
            spriteBatch.Draw(Game1.redsquareDebug, collisionBox.Rectangle, Color.Green);

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
