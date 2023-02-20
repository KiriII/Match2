using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core.Levels
{
    public class LevelsHolder
    {
        private List<Level> _levels;

        public LevelsHolder(List<Level> levels)
        {
            _levels = levels;
        }

        public Level GetLevelById(int id)
        {
            foreach (Level l in _levels)
            {
                if (l.ID == id)
                {
                    return l;
                }
            }

            return null;
        }
    }
}