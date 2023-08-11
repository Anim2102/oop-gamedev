using game_darksouls.Component;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace game_darksouls.Entity.Behaviour
{
    internal class BehaveController : IComponent
    {
        
        private readonly Player player;
        private readonly AnimatedObject animatedObject;

        private IBehave currentBehaviour;

        public Vector2 PatrolPointA { get; set; }
        public Vector2 PatrolPointB { get; set; }
        public EntityMovementType MovementType { get; set; }
        public Attack Attack { get; set; }

        public BehaveController(Player player,AnimatedObject animatedObject,EntityMovementType movementType,
            Vector2 patrolPointA,Vector2 patrolPointB, Attack attack)
        {
            
            this.player = player;
            this.animatedObject = animatedObject;

            PatrolPointA = patrolPointA;
            PatrolPointB = patrolPointB;

            this.Attack = attack;
            this.MovementType = movementType;
        }   

        public void Update(GameTime gameTime)
        {
            ControleState();
            currentBehaviour.Behave(gameTime);
        }

        private void ControleState()
        {
            Vector2 currentPosition = animatedObject.CollisionBox.CenterOfBox();
            Vector2 playerPosition = player.CollisionBox.CenterOfBox();

            float distanceBetweenPlayer = CalculateDistanceBetweenTwoVectorsOnX(currentPosition, playerPosition);

            if (distanceBetweenPlayer < 100)
            {
                currentBehaviour = new Agressive(MovementType,player,animatedObject,animatedObject.MovementBehaviour,animatedObject.AnimationManager,Attack);
            }
            else
            {
                currentBehaviour = new LinearPatrol(PatrolPointA, PatrolPointB, animatedObject, animatedObject.MovementBehaviour);
            }
        }

        private float CalculateDistanceBetweenTwoVectorsOnX(Vector2 a, Vector2 b)
        {
            return Math.Abs(a.X - b.X);
        }

        
       

    }
}
