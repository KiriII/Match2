using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Core.Levels
{
    public class Level : IComparable<Level>, IEquatable<Level>
    {
        public string ID { get; set; }
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

        public int CompareTo(Level other)
        {
            return ID.CompareTo(other.ID);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var objAsPart = obj as Level;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }

        public bool Equals(Level level)
        {
            return (string.Equals(ID, level.ID));
        }

        public override string ToString()
        {
            return $"Id = {ID}\n" +
                $"cond = {condition}";
        }
    }
}