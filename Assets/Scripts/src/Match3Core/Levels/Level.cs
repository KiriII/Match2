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
        public Slot[,] Slots { get; set; }
        public Condition condition { get; set; }
        public Dictionary<Coordinate, string> Boxes { get; set; }

        public Slot GetSlot(Coordinate coordinate)
        {
            return Slots[coordinate.x, coordinate.y];
        }

        public void SetSlot(Coordinate coordinate, Slot slot)
        {
            Slots[coordinate.x, coordinate.y] = slot;
        }

        public void AddBox(Coordinate coordinate, string id)
        {
            Boxes.Add(coordinate, id);
        }
    }
}