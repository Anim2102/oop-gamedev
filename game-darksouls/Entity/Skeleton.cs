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
        private IMovementBehaviour npcMovementManager;

        private readonly Player player;
        private EntityStateController entityStateController;

        //temp switch to manager
        private LinearPatrol linearPatrol;
        private Agressive agressive;

        private Attack attackBox;
        public Skeleton(Texture2D texture, Player player)
        {
            collisionBox = new Box();
            collisionBox.Rectangle = new Rectangle(2405, 700, 60, 50);
            this.drawingBox = new Box(170, 20, 64 * 2, 64 * 2 , new Vector2(-35, -50));

            this.texture = texture;
            this.animationManager = new(AnimationFactory.LoadSkeletonAnimations());
            this.npcMovementManager = new GroundMovement(new CollisionManager(), animationManager,collisionBox);
            this.HealthManager = new Health(5, npcMovementManager);

            this.linearPatrol = new(new Vector2(1450, 650), new Vector2(2000, 650), this, npcMovementManager);

            this.attackBox = new Attack(animationManager, collisionBox, Vector2.Zero);
            attackBox.AttackStartFrame = 5;
            attackBox.AttackEndFrame = 10;
            attackBox.WidthAttackFrame = 90;
            attackBox.HeightAttackFrame = 50;
            this.agressive = new Agressive(EntityMovementType.GROUND,player, this, npcMovementManager, animationManager,attackBox);

            entityStateController = new EntityStateController(linearPatrol, agressive, player, this);
            
        }


        public void Update(GameTime gameTime)
        {
            animationManager.Update(gameTime);
            npcMovementManager.Update(gameTime);
            drawingBox.UpdatePosition(collisionBox.Position);
            entityStateController.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, 
                drawingBox.Rectangle,
                animationManager.currentAnimation.CurrentFrame.SourceRectangle,
                Color.White,
                0f, 
                Vector2.Zero,
                animationManager.SpriteFLip,
                0f);
            
            agressive.Draw(spriteBatch);
        }
    }
}
