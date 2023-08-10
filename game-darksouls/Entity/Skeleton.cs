﻿using Component.Health;
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
        private BehaveController entityStateController;

        //temp switch to manager
        private LinearPatrol linearPatrol;
        private Agressive agressive;

        private Attack attackBox;
        public Skeleton(Texture2D texture, Player player)
        {
            CollisionBox = new Box();
            CollisionBox.Rectangle = new Rectangle(2405, 700, 60, 50);
            this.DrawingBox = new Box(170, 20, 64 * 2, 64 * 2 , new Vector2(-35, -50));

            this.Texture = texture;
            this.AnimationManager = new(AnimationFactory.LoadSkeletonAnimations());
            this.npcMovementManager = new GroundMovement(new CollisionManager(), AnimationManager,CollisionBox);
            this.HealthManager = new Health(5, npcMovementManager,AnimationManager);

            this.linearPatrol = new(new Vector2(1450, 650), new Vector2(2000, 650), this, npcMovementManager);

            this.attackBox = new Attack(AnimationManager, CollisionBox, Vector2.Zero);
            attackBox.AttackStartFrame = 5;
            attackBox.AttackEndFrame = 10;
            attackBox.WidthAttackFrame = 90;
            attackBox.HeightAttackFrame = 50;
            this.agressive = new Agressive(EntityMovementType.GROUND,player, this, npcMovementManager, AnimationManager,attackBox);

            entityStateController = new BehaveController(linearPatrol, agressive, player, this);
            
        }


        public void Update(GameTime gameTime)
        {
            AnimationManager.Update(gameTime);
            npcMovementManager.Update(gameTime);
            DrawingBox.UpdatePosition(CollisionBox.Position);
            entityStateController.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, 
                DrawingBox.Rectangle,
                AnimationManager.currentAnimation.CurrentFrame.SourceRectangle,
                Color.White,
                0f, 
                Vector2.Zero,
                AnimationManager.SpriteFLip,
                0f);
            
            //agressive.Draw(spriteBatch);
        }
    }
}
