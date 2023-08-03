using game_darksouls.Component;
using game_darksouls.Entity.EntityMovement;
using game_darksouls.Enum;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_darksouls.Entity.Behaviour
{
    internal class RangeAttack : IBehave
{
        private readonly Player player;
        private readonly AnimatedObject animatedObject;
        private readonly AnimationManager animationManager;
        private readonly IMovementBehaviour movementBehaviour;

        private Vector2 currentPosition => animatedObject.collisionBox.Position;

        private const double AIMRANGE = 200;

        public RangeAttack(Player player, AnimatedObject animatedObject, AnimationManager animationManager, IMovementBehaviour movementBehaviour)
        {
            this.player = player;
            this.animatedObject = animatedObject;
            this.animationManager = animationManager;
            this.movementBehaviour = movementBehaviour;
        }

        public void Behave(GameTime gameTime)
        {
            if (ReturnDistanceBetweenPlayer() <= AIMRANGE)
            {
                Vector2 normalized = Vector2.Normalize(player.collisionBox.Position - currentPosition);
                movementBehaviour.MoveNpc(normalized);
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
