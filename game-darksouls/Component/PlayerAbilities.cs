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
        public CloseAttack attackBox;
        private InputManager inputManager;

        public bool IsAttacking { get; private set; }

        public PlayerAbilities(CloseAttack attackBox, InputManager inputManager,ISoundManager soundManager)
        {
            this.attackBox = attackBox;
            this.inputManager = inputManager;
            this.soundManager = soundManager;
        }

        public void Update(GameTime gameTime)
        {
            bool hitEntity = false;

            if (inputManager.PressedAttack() && !IsAttacking)
            {
                IsAttacking = true;
            }
            
            if (IsAttacking)
            {
                hitEntity =  attackBox.AttackWithFrame();
                soundManager.PlaySoundEffect("swing");

                if (hitEntity)
                {
                    soundManager.PlaySoundEffect("hit swing");
                }
            }

            if (attackBox.AttackFinished && IsAttacking)
            {
                IsAttacking = false;
                attackBox.ResetAttack();
            }

            if (!IsAttacking)
            {
                attackBox.RemoveAttackFrame();
            }
        }
    }
}
