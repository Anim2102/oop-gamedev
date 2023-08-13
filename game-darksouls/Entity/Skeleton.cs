using Component.Health;
using game_darksouls.Animation;
using game_darksouls.Component;
using game_darksouls.Entity.Behaviour;
using game_darksouls.Entity.EntityMovement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace game_darksouls.Entity
{
    public class Skeleton : AnimatedObject, IEntity
    {
        private BehaveController entityStateController;
        private Attack attackBox;

       


        public Skeleton(Texture2D texture, Player player,CollisionManager collisionManager) : base(collisionManager)
        {
            CollisionBox = new Box();
            CollisionBox.Rectangle = new Rectangle(2405, 650, 60, 50);
            DrawingBox = new Box(0, 0, 64 * 2, 64 * 2 , new Vector2(-35, -50));

            Texture = texture;
            AnimationManager = new(AnimationFactory.LoadSkeletonAnimations());
            MovementBehaviour = new GroundMovement(CollisionManager, AnimationManager,CollisionBox);
            HealthManager = new Health(1, MovementBehaviour,AnimationManager);

            attackBox = new Attack(this,AnimationManager, CollisionBox, Vector2.Zero,collisionManager);
            attackBox.AttackStartFrame = 5;
            attackBox.AttackEndFrame = 10;
            attackBox.WidthAttackFrame = 90;
            attackBox.HeightAttackFrame = 50;

            //niet goed
            Vector2 patrolPointA = new Vector2(CollisionBox.Position.X - 200, CollisionBox.Position.Y);
            Vector2 patrolPointB = new Vector2(CollisionBox.Position.X + 200,CollisionBox.Position.Y);
            entityStateController = new BehaveController(player,this,EntityMovementType.GROUND,patrolPointA,patrolPointB,attackBox);
            
        }


        public override void Update(GameTime gameTime)
        {
            if (HealthManager.CurrentState != Component.Health.State.DYING &&
                HealthManager.CurrentState != Component.Health.State.DEATH)
            {
                MovementBehaviour.Update(gameTime);
                entityStateController.Update(gameTime);
                HealthManager.Update(gameTime);
            }
            AnimationManager.Update(gameTime);
            DrawingBox.UpdatePosition(CollisionBox.Position);
            base.Update(gameTime);
        }

        
    }
}
