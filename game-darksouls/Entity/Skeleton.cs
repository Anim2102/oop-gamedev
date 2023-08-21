using Component.Health;
using Entity.Behaviour.Attack;
using game_darksouls.Animation;
using game_darksouls.Animation.EntityAnimations;
using game_darksouls.Component;
using game_darksouls.Component.Health;
using game_darksouls.Entity.Behaviour;
using game_darksouls.Entity.Behaviour.Attack;
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
        private IAttack attackBox;
        private AttackSquare attackSquare;

        private IAnimationManager animationManager;

        private readonly IMovementBehaviour movementBehaviour;
        private readonly IHealth health;


        private const int CollisionBoxWidth = 60;
        private const int CollisionBoxHeight = 50;
        private const int DrawingBoxWidth = 128;
        private const int DrawingBoxHeight = 128;

        private const float OffsetDrawingX = -35f;
        private const float OffsetDrawingY = -50f;


        public Skeleton(Texture2D texture, Player player,CollisionManager collisionManager,Vector2 patrolPointA,Vector2 patrolPointB) : base(texture)
        {
            collisionBox = new Box();
            collisionBox.Rectangle = new Rectangle(0, 0, CollisionBoxWidth, CollisionBoxHeight);
            drawingBox = new Box(0, 0, DrawingBoxWidth, DrawingBoxHeight, new Vector2(OffsetDrawingX, OffsetDrawingY));

            this.texture = texture;

            SkeletonAnimationFactory skeletonAnimationFactory = new SkeletonAnimationFactory();

            animationManager = new AnimationManager(skeletonAnimationFactory.LoadAnimations());
            movementBehaviour = new GroundMovement(collisionManager,animationManager,collisionBox);
            health = new Health(3,movementBehaviour,(IDeathAnimation)animationManager);

            attackSquare = new AttackSquare(90, 50, 5, 10);
            attackBox = new CloseAttack(this,animationManager,collisionBox,collisionManager,attackSquare);
            
           
            entityStateController = new BehaveController(player,EntityMovementType.GROUND,patrolPointA,patrolPointB,attackBox,movementBehaviour,collisionBox);
            entityStateController.RangeOfAttack = 40f;
        }

        public void TakeDamage()
        {
            health.TakeDamage();
        }

        public void Destroy()
        {
            health.Destroy();
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
