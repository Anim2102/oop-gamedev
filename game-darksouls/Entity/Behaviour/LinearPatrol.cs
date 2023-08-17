using game_darksouls.Component;
using game_darksouls.Entity.EntityMovement;
using game_darksouls.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace game_darksouls.Entity.Behaviour
{
    internal class LinearPatrol : IBehave
    {
        private readonly AnimatedObject animatedObject;
        public readonly  IMovementBehaviour npcMovementManager;

        private Vector2 positionA;
        private Vector2 positionB;
        private Vector2 currentTarget;

        private const float MARGINGTARGET = 100f;
        private const int waitTime = 3;
        private Timer timer;

        public LinearPatrol(Vector2 positionA, Vector2 positionB,
            AnimatedObject animatedObject, IMovementBehaviour npcMovementManager)
        {
            this.positionA = positionA;
            this.positionB = positionB;

            currentTarget = positionB;

            this.animatedObject = animatedObject;
            this.npcMovementManager = npcMovementManager;

            timer = new Timer(waitTime);
        }

        public void Behave(GameTime gameTime)
        {
            timer.Update(gameTime);
            npcMovementManager.ChangeMovingState();

            Vector2 currentPosition = animatedObject.CollisionBox.CenterOfBox();


            //Debug.WriteLine("positie: " + currentPosition + "   target positie: " + currentTarget);
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
            if (currentTarget == positionA)
                currentTarget = positionB;
            else
                currentTarget = positionA;
        }

        private float CalculateDistanceBetweenTwoVectorsOnX(Vector2 a, Vector2 b)
        {
            return Math.Abs(a.X - b.X);
        }

        void IBehave.Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }



        /*public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle pointA = new Rectangle((int)positionA.X, (int)positionA.Y,10, 10);
            Rectangle pointB = new Rectangle((int)positionB.X, (int)positionB.Y, 10, 10);
            

            Rectangle centerPoint = new Rectangle((int)this.animatedObject.DrawingBox.CenterOfBox().X,
                (int)this.animatedObject.DrawingBox.CenterOfBox().Y,10, 10);

            spriteBatch.Draw(Game1.redsquareDebug, pointA, Color.Red);
            spriteBatch.Draw(Game1.redsquareDebug, pointB, Color.Red);

            spriteBatch.Draw(Game1.redsquareDebug, centerPoint, Color.Blue);

        }
       */


    }
}
