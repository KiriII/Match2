using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core.Box
{
    public interface IGUIBoxModel 
    {
        public string GetIdByCoordinate(Coordinate coordinate);
    }
}