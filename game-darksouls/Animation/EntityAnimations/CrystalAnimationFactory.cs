using game_darksouls.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_darksouls.Animation.EntityAnimations
{
    public class CrystalAnimationFactory : AnimationFactory
{
        public override Dictionary<MovementState, ActionAnimation> LoadAnimations()
        {
            Dictionary<MovementState, ActionAnimation> animations = new();
            ActionAnimation staticAnimation = LoadAnimations("static", amountFrames: 24, fps: 12, yas: 0, width: 64, height: 64);

            animations.Add(MovementState.IDLE, staticAnimation);

            return animations;

        }
    }
}
