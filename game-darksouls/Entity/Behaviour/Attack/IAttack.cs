using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_darksouls.Entity.Behaviour.Attack
{
    public interface IAttack
{
        bool IsAttackFinished { get; }
        bool PerformAttack();
        void ResetAttack();
        void RemoveAttackFrame();
}
}
