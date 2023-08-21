using game_darksouls.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_darksouls.Animation.EntityAnimations
{
    public class PlayerAnimationFactory : AnimationFactory
{
        public override Dictionary<MovementState, ActionAnimation> LoadAnimations()
        {
            Dictionary<MovementState, ActionAnimation> animations = new();
            ActionAnimation idleAnimation = LoadAnimations("idle", amountFrames: 4, fps: 8, yas: 0, width: 32, height: 32);
            ActionAnimation runningAnimation = LoadAnimations("running", amountFrames: 8, fps: 16, yas: 32, width: 32, height: 32);
            ActionAnimation fallingAnimation = LoadAnimations("falling", amountFrames: 3, fps: 2, yas: 192, width: 32, height: 32);
            ActionAnimation attackAnimation = LoadAnimations("attack", amountFrames: 5, fps: 8, yas: 64, width: 32, height: 32);
            ActionAnimation dyingAnimation = LoadAnimations("dying", amountFrames: 7, fps: 8, yas: 224, width: 32, height: 32);

            animations.Add(MovementState.DEATH, dyingAnimation);
            animations.Add(MovementState.FALLING, fallingAnimation);
            animations.Add(MovementState.IDLE, idleAnimation);
            animations.Add(MovementState.MOVING, runningAnimation);
            animations.Add(MovementState.ATTACK, attackAnimation);

            return animations;
        }
    }
}
