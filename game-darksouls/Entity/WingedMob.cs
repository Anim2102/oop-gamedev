using Component.Health;
using Entity.Behaviour.Attack;
using game_darksouls.Animation;
using game_darksouls.Component;
using game_darksouls.Component.Health;
using game_darksouls.Entity.Behaviour;
using game_darksouls.Entity.Behaviour.Attack;
using game_darksouls.Entity.EntityMovement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace game_darksouls.Entity
{
    public class WingedMob : AnimatedObject, IEntity
    {
       
        private BehaveController entityStateController;

        private IAttack attackBox;
        private AttackSquare attackSquare;

        private IAnimationManager animationManager;
        private readonly IMovementBehaviour movementBehaviour;
        private readonly IHealth health;
      
        public WingedMob(Texture2D texture, Player player,CollisionManager collisionManager,Vector2 patrolPointA,Vector2 patrolPointB) : base(texture)
        {
            this.texture = texture;
            animationManager = new AnimationManager(AnimationFactory.LoadBrainMobAnimations());
            collisionBox = new Box(500, 700, 50, 50);
            drawingBox = new Box(2405, 700, 50, 50);

            movementBehaviour = new FlyMovement(collisionManager,animationManager,collisionBox);


            attackSquare = new AttackSquare(90, 50, 2, 3);
            attackBox = new CloseAttack(this,animationManager,collisionBox,collisionManager,attackSquare);
                      
            entityStateController = new BehaveController(player,EntityMovementType.FLYING,patrolPointA,patrolPointB,attackBox,movementBehaviour,collisionBox);

            health = new Health(2,movementBehaviour,(IDeathAnimation)animationManager);
        }

        public void TakeDamage()
        {
            health.TakeDamage();
        }

        public override void Update(GameTime gameTime)
        {
            
            if (health.CurrentState != State.DYING &&
                health.CurrentState != State.DEATH)
            {
                movementBehaviour.Update(gameTime);
                entityStateController.Update(gameTime);
            }
            animationManager.Update(gameTime);
            health.Update(gameTime);
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
