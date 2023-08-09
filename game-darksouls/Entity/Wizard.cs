using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using game_darksouls.Animation;
using game_darksouls.Component;
using game_darksouls.Entity.EntityMovement;
using game_darksouls.Entity.Behaviour;
using System.Diagnostics;

namespace game_darksouls.Entity
{
    public class Wizard : AnimatedObject, IEntity
    {
        
        private IMovementBehaviour movementBehaviour;
        private CollisionManager collisionManager;

        private RangeAttack rangeAttack;
        private Texture2D fireball;

        public Wizard(Texture2D texture,Texture2D fireball, Player player)
        {
           this.texture = texture;
           this.fireball = fireball;

           animationManager = new AnimationManager(AnimationFactory.LoadWizardAnimations());
           collisionManager = new CollisionManager();
           
           drawingBox = new Box(250, 20, 64 * 4, 64 * 4, new Vector2(-100, -130));
           collisionBox = new Box(2575, 720, 64, 100, new Vector2(0, 0));
           
           movementBehaviour = new GroundMovement(new CollisionManager(), animationManager, collisionBox);

           rangeAttack = new RangeAttack(player, this, animationManager, movementBehaviour, collisionManager,fireball);
           rangeAttack.RangeOfAttack = 300;
        }

        public void Update(GameTime gameTime)
        {
            animationManager.Update(gameTime);
            rangeAttack.Behave(gameTime);
            rangeAttack.UpdateSpell(gameTime);
            drawingBox.UpdatePosition(collisionBox.Position);  
            movementBehaviour.Update(gameTime);

            //Debug.WriteLine(animationManager.currentAnimation.name);
            //Debug.WriteLine(animationManager.currentAnimation.Counter);
            //Debug.WriteLine(animationManager.currentAnimation.Complete);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Game1.redsquareDebug, drawingBox.Rectangle, Color.Green);
            //spriteBatch.Draw(Game1.redsquareDebug, collisionBox.Rectangle, Color.Green);

            rangeAttack.Draw(spriteBatch);
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
