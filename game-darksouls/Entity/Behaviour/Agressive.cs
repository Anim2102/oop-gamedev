using game_darksouls.Animation;
using game_darksouls.Component;
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

        //private Timer waitTimerBeforeAttack;

        private bool isAttacking = false;
        private bool attackPossible = false;

        private CloseAttack attackBox;

        private Vector2 currentPosition => collisionBox.Position;
        private Vector2 playerPosition => player.CollisionBox.CenterOfBox();

        private float rangeOfAttack;

        public Agressive(EntityMovementType movementType,Player player,Box collisionbox, IMovementBehaviour npcMovementManager, CloseAttack attackBox,float rangeOfAttack)
        {
            this.player = player;
            this.npcMovementManager = npcMovementManager;

            //waitTimerBeforeAttack = new Timer(3);
            this.attackBox = attackBox;
            this.collisionBox = collisionbox;
            this.entityMovement = movementType;
            this.rangeOfAttack = rangeOfAttack;

        }

        public void Behave(GameTime gameTime)
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
            //Debug.WriteLine("currentposition" + currentPosition);
            //Debug.WriteLine("player position" + playerPosition);
            Debug.WriteLine("attacking: " + isAttacking + " aaval mogelijk: " + attackPossible);
            //Debug.WriteLine(distanceBetweenPlayer);


            if (!isAttacking && distanceBetweenPlayer < rangeOfAttack)
            {
                attackPossible = true;
            }
        
            if (attackPossible && distanceBetweenPlayer < rangeOfAttack)
            {
                isAttacking = true;
                npcMovementManager.ResetDirection();
                attackBox.AttackWithFrame();

                if (attackBox.AttackFinished)
                {
                    isAttacking = false;
                    attackPossible = false;
                }
            }

            if (isAttacking && distanceBetweenPlayer > rangeOfAttack)
            {
                isAttacking = false;
                attackPossible = false;
            }
            

            if (!isAttacking && currentPosition != playerPosition && distanceBetweenPlayer > rangeOfAttack)
            {
                attackBox.RemoveAttackFrame();
                Vector2 normalized = Vector2.Normalize(playerPosition - currentPosition);
                //Debug.WriteLine("onderweg:" + normalized);
                npcMovementManager.Push(normalized);
            }
        }
        
        
        
    }
}
