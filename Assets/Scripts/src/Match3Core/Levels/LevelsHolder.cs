using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core.Levels
{
    public class LevelsHolder
    {
        private HashSet<Level> _levels;
        public string currentLevelID { get; set; }

        public LevelsHolder(HashSet<Level> levels, string id)
        {
            _levels = levels;
            currentLevelID = id;
        }

        public HashSet<string> GetLevelsID()
        {
            var ids = new HashSet<string>(_levels.ToList().Select(x => x.ID));
            return ids;
        }

        public Level GetCurrentLevel()
        {
            return _levels.Where(level => level.ID == currentLevelID).First();
        }

        public Level GetLevel(string levelNumber)
        {
            return _levels.First(level => string.Equals(level.ID, levelNumber));
        }

        public Level GetLevel(int levelNumber)
        {
            return _levels.ElementAt(levelNumber);
        }
    }
}