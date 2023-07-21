using game_darksouls.Animation;
using game_darksouls.Enum;
using System.Collections.Generic;

namespace game_darksouls.Component
{
    internal class PlayerAnimation
    {
        private Dictionary<MovementState, ActionAnimation> animations = new();
        private ActionAnimation currentAnimation;

        public PlayerAnimation()
        {
            
        }


        public void AddAnimation(MovementState state, ActionAnimation actionAnimation)
        {
            this.animations.Add(state, actionAnimation);
        }
    }
}
