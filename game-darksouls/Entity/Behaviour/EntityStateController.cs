using game_darksouls.Component;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace game_darksouls.Entity.Behaviour
{
    internal class EntityStateController : IComponent
    {
        
        private readonly Player player;
        private readonly AnimatedObject animatedObject;


        private IBehave Patrol;
        public IBehave agressive;
        private IBehave currentBehaviour;

        public EntityStateController(IBehave Patrol, IBehave agressive,Player player,AnimatedObject animatedObject)
        {
            this.Patrol = Patrol;
            this.agressive = agressive;
            this.player = player;
            this.animatedObject = animatedObject;
        }

        public void Update(GameTime gameTime)
        {
           
            ControleState();
            currentBehaviour.Behave(gameTime);
        }

        private void ControleState()
        {
            Vector2 currentPosition = animatedObject.collisionBox.CenterOfBox();
            Vector2 playerPosition = player.collisionBox.CenterOfBox();

            float distanceBetweenPlayer = CalculateDistanceBetweenTwoVectorsOnX(currentPosition, playerPosition);

            if (distanceBetweenPlayer < 100)
            {
                currentBehaviour = agressive;
            }
            else
            {
                currentBehaviour = Patrol;
            }
        }

        private float CalculateDistanceBetweenTwoVectorsOnX(Vector2 a, Vector2 b)
        {
            return Math.Abs(a.X - b.X);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Patrol.Draw(spriteBatch);
            agressive.Draw(spriteBatch);
        }
       

    }
}
