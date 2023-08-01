﻿using game_darksouls.Component;
using Microsoft.Xna.Framework;
using System;

namespace game_darksouls.Entity.Behaviour
{
    internal class EntityStateController : IComponent
    {
        private LinearPatrol linearPatrol;
        private Agressive agressive;
        private readonly Player player;
        private readonly NpcMovementManager npcMovementManager;
        private readonly AnimatedObject animatedObject;

        private IBehave currentBehaviour;


        public EntityStateController(LinearPatrol linearPatrol, Agressive agressive,Player player,AnimatedObject animatedObject)
        {
            this.linearPatrol = linearPatrol;
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
            Vector2 currentPosition = animatedObject.drawingBox.CenterOfBox();
            Vector2 playerPosition = player.drawingBox.CenterOfBox();

            float distanceBetweenPlayer = CalculateDistanceBetweenTwoVectorsOnX(currentPosition, playerPosition);
            
            if (distanceBetweenPlayer < 200)
            {
                currentBehaviour = agressive;
            }
            else
            {
                currentBehaviour = linearPatrol;
            }
        }

        private float CalculateDistanceBetweenTwoVectorsOnX(Vector2 a, Vector2 b)
        {
            return Math.Abs(a.X - b.X);
        }

    }
}