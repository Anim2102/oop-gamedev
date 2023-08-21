using game_darksouls.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_darksouls.Animation.EntityAnimations
{
    public class BrainMobFactory : AnimationFactory
{
        public override Dictionary<MovementState, ActionAnimation> LoadAnimations()
        {
            Dictionary<MovementState, ActionAnimation> animations = new();
            ActionAnimation idleAnimation = LoadAnimations("idle", amountFrames: 4, fps: 4, yas: 64, width: 32, height: 32);
            ActionAnimation movingAnimation = LoadAnimations("moving", amountFrames: 4, fps: 4, yas: 0, width: 32, height: 32);
            ActionAnimation attackAnimation = LoadAnimations("attack", amountFrames: 4, fps: 4, yas: 32, width: 32, height: 32);
            ActionAnimation dyingAnimation = LoadAnimations("dying", amountFrames: 8, fps: 7, yas: 96, height: 32, width: 32);
            dyingAnimation.Loop = false;

            animations.Add(MovementState.DEATH, dyingAnimation);
            animations.Add(MovementState.ATTACK, attackAnimation);
            animations.Add(MovementState.MOVING, movingAnimation);
            animations.Add(MovementState.IDLE, idleAnimation);

            return animations;

        }
    }
}
