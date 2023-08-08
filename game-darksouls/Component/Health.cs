﻿using game_darksouls.Entity.EntityMovement;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace game_darksouls.Component
{
    public class Health
    {
        public int HealthPoints { get; private set; }
        public bool Hit { get; private set; }
        public Color CurrentColor { get; private set; }
        public bool Alive { get; private set; }
        public bool Invurnable { get; private set; }
        public IMovementBehaviour MovementBehaviour { get; set; }

        private float invurnableTime = 3f;
        private float currentInvurnableTime = 0f;

        private float currentColorTime = 0f;
        private const float MAXCOLORTIME = 5f;
        private const float colorInterval = 1f;

        private int maxHealthPoints;

        public Health(int maxHealth, IMovementBehaviour movementBehaviour)
        {
            this.MovementBehaviour = movementBehaviour;
            maxHealthPoints = maxHealth;
            HealthPoints = maxHealth;
            CurrentColor = Color.White;
        }

        public void TakeDamage()
        {
            if (Invurnable)
                return;

            HealthPoints -= 1;

            if (HealthPoints < 0)
            {
                Alive = false;
            }
            
            Hit = true;


        }

        public void Update(GameTime gameTime)
        {
           // Debug.WriteLine("Hp: " + HealthPoints + "geraakt: " + Hit + "onzichtbaar: " + Invurnable);
            ChangeInvurnableTime(gameTime);
            ChangeColor(gameTime);
        }

        private void ChangeInvurnableTime(GameTime gameTime)
        {
            if (!Hit)
                return;

            currentInvurnableTime += (float)gameTime.ElapsedGameTime.TotalSeconds;


            if (currentInvurnableTime < invurnableTime)
            {
                Invurnable = true;
            }

            else if (currentInvurnableTime >= invurnableTime)
            {
                Invurnable = false;
                currentInvurnableTime = 0f;
                Hit = false;
            }


        }
        private void ChangeColor(GameTime gameTime)
        {
            if (Invurnable)
            {
                currentColorTime = (float)gameTime.TotalGameTime.TotalSeconds;
                if (currentColorTime < colorInterval)
                    CurrentColor = Color.Red;
                else if (currentColorTime > colorInterval)
                {
                    CurrentColor = Color.White;
                }
            }
        }
        

    }
}