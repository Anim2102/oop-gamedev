using Component.Health;
using game_darksouls.Animation;
using game_darksouls.Component;
using game_darksouls.Component.Health;
using game_darksouls.Entity.Behaviour;
using game_darksouls.Entity.EntityMovement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_darksouls.Entity
{
    public class Wizard : AnimatedObject, IEntity
    {
        private readonly IHealth health;
        private readonly IMovementBehaviour movementBehaviour;
        private readonly FlyingObject flyingObject;

        private RangeAttack rangeAttack;
        private Texture2D fireball;

        public IHealth HealthManager
        {
            get
            {
                return health;
            }
        }

        public Wizard(Texture2D texture, Texture2D fireball, Player player, CollisionManager collisionManager) : base(texture)
        {
            this.texture = texture;
            this.fireball = fireball;

            animationManager = new AnimationManager(AnimationFactory.LoadWizardAnimations());
            health = new Health(1, movementBehaviour, (IDeathAnimation)animationManager);
            drawingBox = new Box(250, 20, 64 * 4, 64 * 4, new Vector2(-100, -130));
            collisionBox = new Box(2575, 720, 64, 100, new Vector2(0, 0));

            movementBehaviour = new GroundMovement(collisionManager, animationManager, collisionBox);
            health = new Health(2, movementBehaviour, (IDeathAnimation)animationManager);

            flyingObject = new FlyingObject(fireball,10);
            rangeAttack = new RangeAttack(player, this, animationManager, collisionManager, flyingObject); ;
            rangeAttack.RangeOfAttack = 300;
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

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Game1.redsquareDebug, drawingBox.Rectangle, Color.Green);
            //spriteBatch.Draw(Game1.redsquareDebug, collisionBox.Rectangle, Color.Green);

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
