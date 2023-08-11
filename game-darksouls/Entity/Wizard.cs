using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using game_darksouls.Animation;
using game_darksouls.Component;
using game_darksouls.Entity.EntityMovement;
using game_darksouls.Entity.Behaviour;
using System.Diagnostics;
using Component.Health;

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
           this.Texture = texture;
           this.fireball = fireball;

           AnimationManager = new AnimationManager(AnimationFactory.LoadWizardAnimations());
           collisionManager = new CollisionManager();
            HealthManager = new Health(1, movementBehaviour, AnimationManager);
           DrawingBox = new Box(250, 20, 64 * 4, 64 * 4, new Vector2(-100, -130));
           CollisionBox = new Box(2575, 720, 64, 100, new Vector2(0, 0));
           
           movementBehaviour = new GroundMovement(new CollisionManager(), AnimationManager, CollisionBox);

           rangeAttack = new RangeAttack(player, this, AnimationManager, movementBehaviour, collisionManager,fireball);
           rangeAttack.RangeOfAttack = 300;
        }

        public void Update(GameTime gameTime)
        {
            AnimationManager.Update(gameTime);
            rangeAttack.Behave(gameTime);
            rangeAttack.UpdateSpell(gameTime);
            DrawingBox.UpdatePosition(CollisionBox.Position);  
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
            spriteBatch.Draw(Texture,
                DrawingBox.Rectangle,
                AnimationManager.CurrentAnimation.CurrentFrame.SourceRectangle,
                Color.White,
                0f,
                Vector2.Zero,
                AnimationManager.SpriteFLip,
                0f);
        }
    }
}
