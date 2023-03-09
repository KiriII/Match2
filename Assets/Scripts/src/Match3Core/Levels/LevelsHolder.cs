using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core.Levels
{
    public class LevelsHolder
    {
        private HashSet<Level> _levels;
        public int currentLevelID { get; set; }

        public LevelsHolder(HashSet<Level> levels, int id)
        {
            _levels = levels;
            currentLevelID = id;
        }

        public HashSet<int> GetLevelsID()
        {
            var ids = new HashSet<int>(_levels.ToList().Select(x => x.ID));
            return ids;
        }

        public Level GetCurrentLevel()
        {
            return _levels.Where(level => level.ID == currentLevelID).First();
        }

        // Не по id а по порядковому номеру
        public Level GetLevel(int levelNumber)
        {
            return _levels.ElementAt(levelNumber);
        }
    }
}