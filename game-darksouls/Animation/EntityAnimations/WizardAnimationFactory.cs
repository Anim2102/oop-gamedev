using game_darksouls.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_darksouls.Animation.EntityAnimations
{
    public class WizardAnimationFactory : AnimationFactory
{
        public override Dictionary<MovementState, ActionAnimation> LoadAnimations()
        {
            Dictionary<MovementState, ActionAnimation> animations = new();
            ActionAnimation idleAnimation = LoadAnimations("idle", amountFrames: 8, fps: 8, yas: 0, width: 160, height: 128);
            ActionAnimation attackAnimation = LoadAnimations("attack", amountFrames: 13, fps: 6, yas: 256, width: 160, height: 128);
            ActionAnimation dyingAnimation = LoadAnimations("dying", amountFrames: 10, fps: 9, yas: 768, width: 160, height: 128);
            dyingAnimation.Loop = false;

            animations.Add(MovementState.DEATH, dyingAnimation);
            animations.Add(MovementState.IDLE, idleAnimation);
            animations.Add(MovementState.ATTACK, attackAnimation);

            return animations;
        }
    }
}
