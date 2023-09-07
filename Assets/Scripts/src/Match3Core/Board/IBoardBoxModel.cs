using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core.Board
{
    public interface IBoardBoxModel
    {
        public Cell GetCell(Coordinate coordinate);
    }
}