﻿using Component.Health;
using game_darksouls.Animation;
using game_darksouls.Component;
using game_darksouls.Component.Health;
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


        private LinearPatrol linearPatrol;
        private Agressive agressive;
        private MeleeAttack attackBox;

        private readonly IMovementBehaviour movementBehaviour;
        private readonly IHealth health;
        private readonly Player player;

        public IHealth HealthManager
        {
            get
            {
                return health;
            }
        }

        public WingedMob(Texture2D texture, Player player,CollisionManager collisionManager,Vector2 patrolPointA,Vector2 patrolPointB) : base(texture)
        {
            this.texture = texture;
            this.player = player;

            animationManager = new AnimationManager(AnimationFactory.LoadBrainMobAnimations());
            collisionBox = new Box(500, 700, 50, 50);
            drawingBox = new Box(2405, 700, 50, 50);

            movementBehaviour = new FlyMovement(collisionManager,animationManager,collisionBox);

            attackBox = new MeleeAttack(this,animationManager,collisionBox,collisionManager);
            attackBox.AttackStartFrame = 2;
            attackBox.AttackEndFrame = 3;
            attackBox.WidthAttackFrame = 90;
            attackBox.HeightAttackFrame = 50;

            linearPatrol = new(new Vector2(2275, 799), new Vector2(2500, 799), this, movementBehaviour);
            agressive = new Agressive(EntityMovementType.FLYING,player,collisionBox,movementBehaviour,animationManager,attackBox);
            agressive.RangeOfAttack = 50;
            

            

            entityStateController = new BehaveController(player,this,EntityMovementType.FLYING,patrolPointA,patrolPointB,attackBox,movementBehaviour,collisionBox);

            health = new Health(1,movementBehaviour,(IDeathAnimation)animationManager);



        }

        IHealth IEntity.HealthManager => throw new System.NotImplementedException();

        public override void Update(GameTime gameTime)
        {
            
            if (health.CurrentState != State.DYING &&
                HealthManager.CurrentState != State.DEATH)
            {
                movementBehaviour.Update(gameTime);
                entityStateController.Update(gameTime);
            }
            animationManager.Update(gameTime);
            //HealthManager.Update(gameTime);
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
