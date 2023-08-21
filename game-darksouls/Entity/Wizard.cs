using Component.Health;
using Entity.Behaviour.Attack;
using game_darksouls.Animation;
using game_darksouls.Animation.EntityAnimations;
using game_darksouls.Component;
using game_darksouls.Component.Health;
using game_darksouls.Entity.EntityMovement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_darksouls.Entity
{
    public class Wizard : AnimatedObject, IEntity
    {
        private readonly IHealth health;
        private readonly IMovementBehaviour movementBehaviour;
        private readonly IAnimationManager animationManager;
        private readonly RangeAttack rangeAttack;
        private readonly FlyingObject flyingObject;

        private const int Health = 1;
        private const int DrawingBoxWidth = 256;
        private const int DrawingBoxHeight = 256;
        private const int CollisionBoxWidth = 64;
        private const int CollisionBoxHeight = 100;

        private const int FireBallSize = 10;

        public Wizard(Texture2D texture, Texture2D fireball, Player player, CollisionManager collisionManager) : base(texture)
        {
            this.texture = texture;
            WizardAnimationFactory wizardAnimationFactory = new WizardAnimationFactory();
            animationManager = new AnimationManager(wizardAnimationFactory.LoadAnimations());

            drawingBox = new Box(0, 0, DrawingBoxWidth, DrawingBoxHeight, new Vector2(-100, -130));
            collisionBox = new Box(0, 0, CollisionBoxWidth, CollisionBoxHeight, new Vector2(0, 0));
            movementBehaviour = new GroundMovement(collisionManager, animationManager, collisionBox);

            
            health = new Health(Health, movementBehaviour, (IDeathAnimation)animationManager);
            


            flyingObject = new FlyingObject(fireball,FireBallSize);
            rangeAttack = new RangeAttack(player, this, animationManager, collisionManager, flyingObject); ;
            rangeAttack.RangeOfAttack = 600;
        }

        public override void Update(GameTime gameTime)
        {

            if (health.CurrentState != State.DYING &&
                health.CurrentState != State.DEATH)
            {
                movementBehaviour.Update(gameTime);
                rangeAttack.Behave(gameTime);

            }
            animationManager.Update(gameTime);
            rangeAttack.UpdateSpell(gameTime);
            drawingBox.UpdatePosition(collisionBox.Position);
            base.Update(gameTime);
        }

        public void TakeDamage()
        {
            health.TakeDamage();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            rangeAttack.Draw(spriteBatch);
            spriteBatch.Draw(texture,
                drawingBox.Rectangle,
                animationManager.CurrentAnimation.CurrentFrame.SourceRectangle,
                Color.White,
                0f,
                Vector2.Zero,
                animationManager.SpriteFlip,
                0f);
        }
    }
}
