using game_darksouls.Component;
using game_darksouls.Enum;
using game_darksouls.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace game_darksouls.Entity.Behaviour
{
    internal class Agressive : IBehave
    {
        private readonly Player player;
        private readonly AnimatedObject animatedObject;
        private readonly AnimationManager animationManager;
        private readonly NpcMovementManager npcMovementManager;

        private Timer waitTimerBeforeAttack;
        private bool attacking = false;

        private Vector2 currentPosition;

        public Agressive(Player player, AnimatedObject animatedObject, NpcMovementManager npcMovementManager, AnimationManager animationManager)
        {
            this.player = player;
            this.animatedObject = animatedObject;
            this.npcMovementManager = npcMovementManager;
            this.animationManager = animationManager;

            waitTimerBeforeAttack = new Timer(3);

        }

        public void Behave(GameTime gameTime)
        {
            Vector2 currentPosition = new Vector2(animatedObject.drawingBox.DrawingRectangle.X,
                animatedObject.drawingBox.DrawingRectangle.Y);

            Vector2 playerPosition = player.drawingBox.CenterOfBox();

            float distanceBetweenPlayer = CalculateDistanceBetweenTwoVectorsOnX(currentPosition, playerPosition);

            waitTimerBeforeAttack.Update(gameTime);

            if (currentPosition != playerPosition && !attacking)
            {
                Vector2 normalized = Vector2.Normalize(playerPosition - currentPosition);
                npcMovementManager.MoveNpc(normalized);
            }

            if (distanceBetweenPlayer < 40 && !attacking)
            {
                waitTimerBeforeAttack.Start();
                npcMovementManager.ResetDirection();
                attacking = true;
            }

            if (attacking && !waitTimerBeforeAttack.timeRunning)
            {
                Attack();
                attacking = false;
                waitTimerBeforeAttack.Reset();
            }
            


        }
        private void Attack()
        {
            animationManager.PlayAnimation(MovementState.ATTACK);
        }
        private float CalculateDistanceBetweenTwoVectorsOnX(Vector2 a, Vector2 b)
        {
            return Math.Abs(a.X - b.X);
        }
    }
}
