using Entity.Behaviour.Attack;
using game_darksouls.Entity.Behaviour.Attack;
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
        public IAttack attackBox;
        private IInput inputManager;

        public bool IsAttacking { get; private set; }

        private bool playedHitSound = false;

        public PlayerAbilities(IAttack attackBox, IInput inputManager,ISoundManager soundManager)
        {
            this.attackBox = attackBox;
            this.inputManager = inputManager;
            this.soundManager = soundManager;
        }

        public void Update(GameTime gameTime)
        {
            bool hitEntity;

            if (inputManager.PressedAttack() && !IsAttacking)
            {
                IsAttacking = true;
            }
            
            if (IsAttacking)
            {
                hitEntity =  attackBox.PerformAttack();
                PlaySound("swing");

                if (hitEntity)
                {
                    if (!playedHitSound) {
                        PlaySound("hit swing");
                        playedHitSound = true;
                    }
                }
            }

            if (attackBox.IsAttackFinished && IsAttacking)
            {
                IsAttacking = false;
                attackBox.ResetAttack();
                playedHitSound = false;
            }

            if (!IsAttacking)
            {
                attackBox.RemoveAttackFrame();
            }
        }

       
        private void PlaySound(string effectName)
        {
            soundManager.PlaySoundEffect(effectName);
        }
    }
}
