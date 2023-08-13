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
    public class WingedMob : AnimatedObject, IEntity
    {
       
        private BehaveController entityStateController;

        private Player player;

        private LinearPatrol linearPatrol;
        private Agressive agressive;
        private Attack attackBox;


        public WingedMob(Texture2D texture, Player player,CollisionManager collisionManager) : base(collisionManager)
        {
            this.Texture = texture;
            this.player = player;

            AnimationManager = new AnimationManager(AnimationFactory.LoadBrainMobAnimations());
            CollisionBox = new Box(500, 700, 50, 50);
            DrawingBox = new Box(2405, 700, 50, 50);

            MovementBehaviour = new FlyMovement(CollisionManager, AnimationManager, CollisionBox);

            attackBox = new Attack(this, AnimationManager, CollisionBox, Vector2.Zero, collisionManager);
            attackBox.AttackStartFrame = 2;
            attackBox.AttackEndFrame = 3;
            attackBox.WidthAttackFrame = 90;
            attackBox.HeightAttackFrame = 50;

            linearPatrol = new(new Vector2(2275, 799), new Vector2(2500, 799), this, MovementBehaviour);
            agressive = new Agressive(EntityMovementType.FLYING, player, this, MovementBehaviour, AnimationManager, attackBox);
            agressive.RangeOfAttack = 50;
            

            

            entityStateController = new BehaveController(player,this,EntityMovementType.FLYING,new Vector2(2275,799),new Vector2(2500,799),attackBox);

            HealthManager = new Health(1, MovementBehaviour, AnimationManager);



        }

        public override void Update(GameTime gameTime)
        {
            
            if (HealthManager.CurrentState != Component.Health.State.DYING &&
                HealthManager.CurrentState != Component.Health.State.DEATH)
            {
                MovementBehaviour.Update(gameTime);
                entityStateController.Update(gameTime);
            }

            AnimationManager.Update(gameTime);
            HealthManager.Update(gameTime);
            base.Update(gameTime);
        }

        
    }
}
