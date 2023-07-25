using game_darksouls.Component;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace game_darksouls.Entity.Behaviour
{
    internal class LinearPatrol : IBehave
    {
        private readonly AnimatedObject animatedObject;
        public readonly  NpcMovementManager npcMovementManager;

        private Vector2 positionA;
        private Vector2 positionB;
        private Vector2 currentTarget;



        public LinearPatrol(Vector2 positionA, Vector2 positionB,
            AnimatedObject animatedObject, NpcMovementManager npcMovementManager)
        {
            this.positionA = positionA;
            this.positionB = positionB;

            currentTarget = positionB;

            this.animatedObject = animatedObject;
            this.npcMovementManager = npcMovementManager;
        }

        public void Behave()
        {
            Vector2 currentPosition = new Vector2(animatedObject.drawingBox.DrawingRectangle.X,
                animatedObject.drawingBox.DrawingRectangle.Y);


            if (currentPosition != currentTarget)
            {
                Vector2 normalized = Vector2.Normalize(currentTarget - currentPosition);
                normalized.Y = 0;
                npcMovementManager.MoveNpc(normalized);
                
            }


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle pointA = new Rectangle((int)positionA.X, (int)positionA.Y,10, 10);
            Rectangle pointB = new Rectangle((int)positionB.X, (int)positionB.Y, 10, 10);

            spriteBatch.Draw(Game1.redsquareDebug, pointA, Color.Orange);
            spriteBatch.Draw(Game1.redsquareDebug, pointB, Color.Orange);

        }


    }
}
