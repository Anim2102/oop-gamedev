using Entity.Behaviour.Attack;
using game_darksouls.Animation;
using game_darksouls.Component;
using game_darksouls.Entity.Behaviour.Attack;
using game_darksouls.Entity.EntityMovement;
using game_darksouls.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace game_darksouls.Entity.Behaviour
{
    internal class Agressive : IBehave
    {
        private EntityMovementType entityMovement;
        private readonly Player player;
        private Box collisionBox;
        private readonly IMovementBehaviour npcMovementManager;

        private bool isAttacking = false;
        private bool attackPossible = false;

        private IAttack attackBox;

        private Vector2 currentPosition => collisionBox.Position;
        private Vector2 playerPosition => player.CollisionBox.CenterOfBox();


        private BehaveController behaveController;

        private const float RANGEOFATTACK = 60f;
        private const int PATROLRANGE = 100;

        public Agressive(EntityMovementType movementType,Player player,Box collisionbox, IMovementBehaviour npcMovementManager, IAttack attackBox,BehaveController behaveController)
        {
            this.player = player;
            this.npcMovementManager = npcMovementManager;
            this.behaveController = behaveController;
            this.attackBox = attackBox;
            this.collisionBox = collisionbox;
            this.entityMovement = movementType;

        }
        private float CalculateDistance()
        {
            float distanceBetweenPlayer = 0f;
            if (entityMovement == EntityMovementType.FLYING)
            {
                distanceBetweenPlayer = VectorHelpingClass.ReturnDistanceBetweenPlayerXandY(currentPosition, player.CollisionBox);
            }

            if (entityMovement == EntityMovementType.GROUND)
            {
                distanceBetweenPlayer = VectorHelpingClass.ReturnDistanceBetweenPlayerLinear(currentPosition, player.CollisionBox);
            }
            return distanceBetweenPlayer;
            
        }
        public void Behave(GameTime gameTime)
        {

            float distanceBetweenPlayer = CalculateDistance();

            if (distanceBetweenPlayer > PATROLRANGE)
            {
                behaveController.SetBehaveState(behaveController.PatrolState);
                return;
            }

            if (!isAttacking && distanceBetweenPlayer < RANGEOFATTACK)
            {
                attackPossible = true;
            }
        
            if (attackPossible && distanceBetweenPlayer < RANGEOFATTACK)
            {
                isAttacking = true;
                npcMovementManager.ResetDirection();
                attackBox.PerformAttack();

                if (attackBox.IsAttackFinished)
                {
                    isAttacking = false;
                    attackPossible = false;
                }
            }

            if (isAttacking && distanceBetweenPlayer > RANGEOFATTACK)
            {
                isAttacking = false;
                attackPossible = false;
            }
            

            if (!isAttacking && currentPosition != playerPosition && distanceBetweenPlayer > RANGEOFATTACK)
            {
                attackBox.RemoveAttackFrame();
                Vector2 normalized = Vector2.Normalize(playerPosition - currentPosition);
                npcMovementManager.Push(normalized);
            }
        }
        
        
        
    }
}
