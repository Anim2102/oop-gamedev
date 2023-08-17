using game_darksouls.Enum;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_darksouls.Animation
{
    public interface IAnimationManager
{
        ActionAnimation CurrentAnimation { get; }
        bool FacingLeft { get; set; }
        SpriteEffects SpriteFlip { get; }

        void PlayAnimation(MovementState movementState) { }
        void AddAnimation(MovementState movementState, ActionAnimation actionAnimation) { }
        void UpdateAnimationOnState(MovementState state) { }
        ActionAnimation ReturnAnimationOnState(MovementState state);
        void ResetAnimationOnState(MovementState state) { }
        void Update(GameTime gameTime) { }

    }
}
