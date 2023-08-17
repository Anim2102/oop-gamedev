using Component.Health;
using game_darksouls.Animation;
using game_darksouls.Component;
using game_darksouls.Component.Health;
using game_darksouls.Entity.Behaviour;
using game_darksouls.Entity.EntityMovement;
using game_darksouls.Sound;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace game_darksouls.Entity
{
    public class Skeleton : AnimatedObject, IEntity
    {
        private BehaveController entityStateController;
        private CloseAttack attackBox;

        private readonly ISoundManager soundManager;
        private readonly IMovementBehaviour movementBehaviour;
        private readonly IHealth health;

        public IHealth HealthManager
        {
            get
            {
                return health;
            }
        }

        public Skeleton(Texture2D texture, Player player,CollisionManager collisionManager,Vector2 patrolPointA,Vector2 patrolPointB) : base(texture)
        {
            this.soundManager = new SoundManager();
            collisionBox = new Box();
            collisionBox.Rectangle = new Rectangle(2405, 650, 60, 50);
            drawingBox = new Box(0, 0, 64 * 2, 64 * 2 , new Vector2(-35, -50));

            this.texture = texture;
            animationManager = new AnimationManager(AnimationFactory.LoadSkeletonAnimations());
            movementBehaviour = new GroundMovement(collisionManager,animationManager,collisionBox);
            health = new Health(3,movementBehaviour,(IDeathAnimation)animationManager);

            attackBox = new CloseAttack(this,animationManager,collisionBox,collisionManager);
            attackBox.AttackStartFrame = 5;
            attackBox.AttackEndFrame = 10;
            attackBox.WidthAttackFrame = 90;
            attackBox.HeightAttackFrame = 50;
           

            
            entityStateController = new BehaveController(player,this,EntityMovementType.GROUND,patrolPointA,patrolPointB,attackBox,movementBehaviour,collisionBox);
            entityStateController.RangeOfAttack = 40f;
        }


        public override void Update(GameTime gameTime)
        {
            if (health.CurrentState != State.DYING &&
                health.CurrentState != State.DEATH)
            {
                movementBehaviour.Update(gameTime);
                entityStateController.Update(gameTime);
                health.Update(gameTime);
            }
            animationManager.Update(gameTime);
            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
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
