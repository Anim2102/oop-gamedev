using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_darksouls.Entity.Behaviour
{
    public interface IPatrol
{
        Vector2 PatrolPointA { get; set; }
        Vector2 PatrolPointB { get; set; }

}
}
