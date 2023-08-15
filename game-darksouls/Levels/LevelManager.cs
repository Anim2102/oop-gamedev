using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_darksouls.Levels
{
    public class LevelManager
{
        private List<ILevel> levels = new();

        public void AddLevel(ILevel level)
        {
            levels.Add(level);
        }

        public bool CheckLastLevel(ILevel level)
        {
            int indexOfLevel = levels.IndexOf(level);


            //returns true when not last level
            return indexOfLevel == levels.Count - 1;
        }

        public ILevel GetNextLevel(ILevel level)
        {
            if (levels.Contains(level) && !CheckLastLevel(level))
            {
                int indexOfCurrentLevel = levels.IndexOf(level);
                return levels[indexOfCurrentLevel + 1];
            }

            return null;
        }

        public ILevel GetLevelByIndex(int index)
        {
            return levels[index];
        }


}
}
