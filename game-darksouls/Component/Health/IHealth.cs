using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_darksouls.Component.Health
{
    public interface IHealth
{
        int HealthPoints { get; }
        bool Alive { get; }
        State CurrentState { get; }

        void Update(GameTime gameTime);
        void TakeDamage() { }
        void Destroy() { }
}
}
