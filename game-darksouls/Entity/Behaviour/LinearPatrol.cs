using game_darksouls.Component;
using game_darksouls.Entity.EntityMovement;
using game_darksouls.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace game_darksouls.Entity.Behaviour
{
    internal class LinearPatrol : IBehave, IPatrol
    {
        private readonly Box collisionBox;
        public readonly  IMovementBehaviour npcMovementManager;

        public Vector2 PatrolPointA { get; set; }
        public Vector2 PatrolPointB { get; set; }

        private Vector2 currentTarget;

        private const float MARGINGTARGET = 100f;
        private const int waitTime = 3;
        private Timer timer;

        

        public LinearPatrol(Vector2 positionA, Vector2 positionB,
            Box collisionBox, IMovementBehaviour npcMovementManager)
        {
            this.collisionBox = collisionBox;

            PatrolPointA = positionA;
            PatrolPointB = positionB;

            currentTarget = positionB;

            this.npcMovementManager = npcMovementManager;

            timer = new Timer(waitTime);
        }

        public void Behave(GameTime gameTime)
        {
            timer.Update(gameTime);
            npcMovementManager.ChangeMovingState();

            Vector2 currentPosition = collisionBox.CenterOfBox();

            float distanceToTarget = (float)CalculateDistanceBetweenTwoVectorsOnX(currentPosition, currentTarget);

            if (distanceToTarget > MARGINGTARGET && !timer.timeRunning)
            {
                Vector2 normalized = Vector2.Normalize(currentTarget - currentPosition);
                npcMovementManager.Push(normalized);
            }

            if (distanceToTarget <= MARGINGTARGET)
            {
                timer.Reset();
                timer.Start();
                SwitchTargets();
                npcMovementManager.ResetDirection();
            }
                
        }

        private void SwitchTargets()
        {
            if (currentTarget == PatrolPointA)
                currentTarget = PatrolPointB;
            else
                currentTarget = PatrolPointA;
        }

        private float CalculateDistanceBetweenTwoVectorsOnX(Vector2 a, Vector2 b)
        {
            return Math.Abs(a.X - b.X);
        }

     
    }
}
