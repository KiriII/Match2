using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core.Levels
{
    public class LevelsHolder
    {
        private HashSet<Level> _levels;
        private int _lastCompleteLevelIndex;
        public string currentLevelID { get; set; }

        public LevelsHolder(HashSet<Level> levels, string id, int lastCompleteLevelIndex = 0)
        {
            _levels = levels;
            _lastCompleteLevelIndex = lastCompleteLevelIndex;
            currentLevelID = id;
        }

        public void CurrentLevelComplete()
        {
            if (_levels.ElementAt(_lastCompleteLevelIndex).Equals(GetCurrentLevel()))
            {
                _lastCompleteLevelIndex++;
                Debug.Log(_lastCompleteLevelIndex);
            }
        }

        public bool HaveNextLevel()
        {
            if (_lastCompleteLevelIndex == _levels.Count - 1) return false;
            return true;
        }

        #region Getters 
        public HashSet<string> GetLevelsId()
        {
            var ids = new HashSet<string>(_levels.ToList().Select(x => x.ID));
            return ids;
        }

        public HashSet<string> GetAwaibleLevelsId()
        {
            var result = new HashSet<string>();
            for (int i = 0; i <= _lastCompleteLevelIndex; i++)
            {
                result.Add(_levels.ElementAt(i).ID);
            }
            return result;
        }

        public Level GetCurrentLevel()
        {
            return _levels.Where(level => level.ID == currentLevelID).First();
        }

        public int GetCurrentLevelIndex()
        {
            return _levels.Select((Value, Index) => new { Value, Index })
                           .ToList().First().Index;
        }

        public Level GetNextLevel()
        {
            if (!HaveNextLevel()) return null;
            return GetLevel(GetCurrentLevelIndex() + 1);
        }

        public Level GetLevel(string levelId)
        {
            return _levels.First(level => string.Equals(level.ID, levelId));
        }

        public Level GetLevel(int levelNumber)
        {
            return _levels.ElementAt(levelNumber);
        }
        #endregion
    }
}