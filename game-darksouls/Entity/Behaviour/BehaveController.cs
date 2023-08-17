using game_darksouls.Animation;
using game_darksouls.Component;
using game_darksouls.Entity.EntityMovement;
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
        private readonly IMovementBehaviour movementBehaviour;
        private readonly Box collesionBox;
        private readonly IAnimationManager animationManager;
        private IBehave currentBehaviour;

        public Vector2 PatrolPointA { get; set; }
        public Vector2 PatrolPointB { get; set; }
        public EntityMovementType MovementType { get; set; }
        public CloseAttack Attack { get; set; }
        public float RangeOfAttack { get; set; }

        public BehaveController(Player player,AnimatedObject animatedObject,EntityMovementType movementType,
            Vector2 patrolPointA,Vector2 patrolPointB, CloseAttack attack, IMovementBehaviour movementBehaviour, Box collissionBox)
        {
            
            this.player = player;
            this.animatedObject = animatedObject;

            PatrolPointA = patrolPointA;
            PatrolPointB = patrolPointB;

            this.Attack = attack;
            this.MovementType = movementType;

            this.movementBehaviour = movementBehaviour;

            this.collesionBox = collissionBox;
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
                currentBehaviour = new Agressive(MovementType,player, collesionBox, movementBehaviour,Attack,RangeOfAttack);
            }
            else
            {
                currentBehaviour = new LinearPatrol(PatrolPointA, PatrolPointB, animatedObject, movementBehaviour);
            }
        }

        private float CalculateDistanceBetweenTwoVectorsOnX(Vector2 a, Vector2 b)
        {
            return Math.Abs(a.X - b.X);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (currentBehaviour is Agressive aggresive)
             aggresive.Draw(spriteBatch);
        }
       

    }
}
