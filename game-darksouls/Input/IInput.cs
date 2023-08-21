using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_darksouls.Input
{
    internal interface IInput
{
        Vector2 GetInput();
        bool PressedAttack();
        bool IsJumpButtonPress();
    }
}
