using game_darksouls.Animation;
using game_darksouls.Component;
using game_darksouls.Component.Health;
using game_darksouls.Entity.EntityMovement;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Component.Health
{
    public class Health : IHealth
    {
        private int healthPoints;

        public int HealthPoints
        {
            get
            {
                return healthPoints;
            }
        }
        public bool Hit { get; private set; }
        public Color CurrentColor { get; private set; }

        private bool alive;
        public bool Alive
        {
            get
            {
                return alive;
            }
        }

        private State currentState;
        public State CurrentState
        {
            get
            {
                return currentState;
            }
        }

        public bool Invurnable { get; private set; }
        private readonly IDeathAnimation deathAnimation;
        private readonly IMovementBehaviour movementBehaviour;

        private float invurnableTime = 3f;
        private float currentInvurnableTime = 0f;

        private float currentColorTime = 0f;
        private const float colorInterval = 0.8f;

        public Health(int maxHealth, IMovementBehaviour movementBehaviour, IDeathAnimation animationManager)
        {
            this.movementBehaviour = movementBehaviour;
            this.deathAnimation = animationManager;
            healthPoints = maxHealth;
            CurrentColor = Color.White;
            alive = true;

        }

        public void TakeDamage()
        {
            if (Invurnable)
                return;

            healthPoints -= 1;

            if (HealthPoints <= 0)
            {
                deathAnimation.PlayDeathAnimation();
                currentState = State.DYING;
            }
            if (CurrentState == State.DYING && deathAnimation.DeathAnimationComplete)
                currentState = State.DEATH;

            Hit = true;
            movementBehaviour.PushAfterHit(new Vector2(0, -1));
        }

        public void Update(GameTime gameTime)
        {
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
                currentColorTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (currentColorTime >= colorInterval)
                {
                    if (CurrentColor == Color.White)
                        CurrentColor = Color.Red;
                    else
                        CurrentColor = Color.White;

                    currentColorTime = 0;
                }
                
            }
            else
            {
                CurrentColor = Color.White;
            }

            
        }


    }
}
