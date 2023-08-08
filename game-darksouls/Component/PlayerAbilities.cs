﻿using game_darksouls.Entity.EntityMovement;
using game_darksouls.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace game_darksouls.Component
{
    internal class PlayerAbilities : IComponent
    {
        private readonly IMovementBehaviour playerMovement;
        private readonly AnimationManager animationManager;
        public Attack attackBox;
        private InputManager inputManager;

        public bool Attacking { get; set; }

        public PlayerAbilities(IMovementBehaviour movementBehaviour, Attack attackBox, InputManager inputManager,AnimationManager animationManager)
        {
            this.playerMovement = movementBehaviour;
            this.attackBox = attackBox;
            this.inputManager = inputManager;
            this.animationManager = animationManager;
        }

        public void Update(GameTime gameTime)
        {

            if (inputManager.PressedAttack())
            {
                Attacking = true;
                playerMovement.ResetDirection();
            }
            else
            {
                Attacking = false;
            }


            if (Attacking)
            {
                attackBox.AttackWithFrame(Game1.skeleton);
            }

            if (attackBox.attackFinished && Attacking)
            {
                Attacking = false;
                attackBox.ResetAttackAnimation();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.redsquareDebug, attackBox.attackFrame, Color.Red);
        }
    }
}
