using game_darksouls.Entity.EntityMovement;
using game_darksouls.Input;
using game_darksouls.Sound;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace game_darksouls.Component
{
    internal class PlayerAbilities : IComponent
    {
        private readonly IMovementBehaviour playerMovement;
        private readonly SoundManager soundManager;
        public Attack attackBox;
        private InputManager inputManager;

        public bool Attacking { get; set; }

        public PlayerAbilities(IMovementBehaviour movementBehaviour, Attack attackBox, InputManager inputManager,SoundManager soundManager)
        {
            this.playerMovement = movementBehaviour;
            this.attackBox = attackBox;
            this.inputManager = inputManager;
            this.soundManager = soundManager;
        }

        public void Update(GameTime gameTime)
        {
            Attacking = false;
            if (inputManager.PressedAttack())
            {
                Attacking = true;
                playerMovement.ResetDirection();
            }
            
            if (Attacking)
            {
                attackBox.AttackWithFrame();
                soundManager.PlaySoundEffect("swing");
            }

            if (attackBox.AttackFinished && Attacking)
            {
                Attacking = false;
                attackBox.ResetAttackAnimation();
            }

            if (!Attacking)
                attackBox.RemoveAttackFrame();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.redsquareDebug, attackBox.attackFrame, Color.Red);
        }
    }
}
