using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core.Levels
{
    public class Level
    {
        public int ID { get; set; }
        public int rows { get; set; }
        public int collumns { get; set; }
        public Slot[,]? slots { get; set; }

        public Slot GetSlot(Coordinate coordinate)
        {
            return slots[coordinate.x, coordinate.y];
        }

        public void SetSlot(Coordinate coordinate, Slot slot)
        {
            slots[coordinate.x, coordinate.y] = slot;
        }
    }
}