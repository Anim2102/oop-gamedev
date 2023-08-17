using game_darksouls.Entity.EntityMovement;
using game_darksouls.Input;
using game_darksouls.Sound;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace game_darksouls.Component
{
    internal class PlayerAbilities
    {
        private readonly ISoundManager soundManager;
        public MeleeAttack attackBox;
        private InputManager inputManager;

        public bool IsAttacking { get; private set; }

        public PlayerAbilities(MeleeAttack attackBox, InputManager inputManager,ISoundManager soundManager)
        {
            this.attackBox = attackBox;
            this.inputManager = inputManager;
            this.soundManager = soundManager;
        }

        public void Update(GameTime gameTime)
        {
            IsAttacking = false;
            if (inputManager.PressedAttack())
            {
                IsAttacking = true;
            }
            
            if (IsAttacking)
            {
                attackBox.AttackWithFrame();
                soundManager.PlaySoundEffect("swing");
            }

            if (attackBox.AttackFinished && IsAttacking)
            {
                IsAttacking = false;
                attackBox.ResetAttackAnimation();
            }

            if (!IsAttacking)
                attackBox.RemoveAttackFrame();
        }
    }
}
