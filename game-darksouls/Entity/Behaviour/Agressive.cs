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
        private readonly Player player;
        private readonly AnimatedObject animatedObject;
        private readonly AnimationManager animationManager;
        private readonly IMovementBehaviour npcMovementManager;

        private Timer waitTimerBeforeAttack;

        private bool attacking = false;
        private bool attackPossible = false;

        private Attack attackBox;

        private Vector2 currentPosition => animatedObject.collisionBox.Position;
        private Vector2 playerPosition => player.drawingBox.CenterOfBox();

        public float RangeOfAttack { get; set; } = 40f;

        public Agressive(Player player, AnimatedObject animatedObject, IMovementBehaviour npcMovementManager, AnimationManager animationManager)
        {
            this.player = player;
            this.animatedObject = animatedObject;
            this.npcMovementManager = npcMovementManager;
            this.animationManager = animationManager;

            //waitTimerBeforeAttack = new Timer(3);
            attackBox = new Attack(Rectangle.Empty, 0, 0, animationManager, animatedObject, npcMovementManager);

        }

        public void Behave(GameTime gameTime)
        {
            Debug.WriteLine("attack behave");
            //waitTimerBeforeAttack.Update(gameTime);
            float distanceBetweenPlayer = ReturnDistanceBetweenPlayer();

            /*Debug.WriteLine("mogelijk om aan te vallen" + attackPossible);
            Debug.WriteLine("bezig met aantevallen" + attacking);
            Debug.WriteLine("animatie klaar: " + attackBox.attackFinished);
            */

            if (!attacking && distanceBetweenPlayer < RangeOfAttack)
            {
                attackPossible = true;
            }
        
            if (attackPossible && distanceBetweenPlayer < RangeOfAttack)
            {
                attacking = true;
                npcMovementManager.ResetDirection();
                attackBox.AttackWithFrame();

                if (attackBox.attackFinished)
                {
                    attacking = false;
                    attackPossible = false;
                }
            }


            //eerste animatie finishe
            if (attacking && distanceBetweenPlayer > RangeOfAttack)
            {
                attacking = false;
                attackPossible = false;
            }
            

            if (!attacking && currentPosition != playerPosition)
            {
                //animationManager.PlayAnimation(Enum.MovementState.MOVING);
                attackBox.RemoveAttackFrame();
                Vector2 normalized = Vector2.Normalize(playerPosition - currentPosition);
                //Debug.WriteLine(normalized );
                npcMovementManager.MoveNpc(normalized);
                //Debug.WriteLine(true);

            }
            else
            {
                //Debug.WriteLine(false);
            }


        }

        private float ReturnDistanceBetweenPlayer()
        {
            Vector2 currentPosition = new Vector2(animatedObject.collisionBox.Rectangle.X,
                animatedObject.collisionBox.Rectangle.Y);

            Vector2 playerPosition = player.drawingBox.CenterOfBox();

            return CalculateDistanceBetweenTwoVectorsOnX(currentPosition, playerPosition);
        }


        private float CalculateDistanceBetweenTwoVectorsOnX(Vector2 a, Vector2 b)
        {
            return Math.Abs(a.X - b.X);
        }

    }
}
