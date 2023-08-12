using game_darksouls.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_darksouls.Level
{
    public interface ILevel
{
        public List<Tile> Tiles { get; }
        public List<AnimatedObject> Entitys { get; }


    }
}
