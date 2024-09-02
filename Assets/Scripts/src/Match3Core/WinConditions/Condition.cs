using UnityEngine;
using System;
using System.Collections.Generic;

namespace Match3Core
{
    public class Condition
    {
        public readonly byte flags;
        public readonly CellsColor color;
        public readonly int colorCount;
        public readonly bool special;
        public readonly bool unblock;
        public readonly HashSet<Coordinate> shape;

        public Condition(byte flags, CellsColor color = CellsColor.Empty, int colorCount = 0, bool special = false, bool unblock = false, HashSet<Coordinate> shape = null)
        {
            this.flags = flags; 
            this.color = color;
            this.colorCount = colorCount;
            this.special = special;
            this.unblock = unblock;
            this.shape = shape;
        }

        public override string ToString()
        {
            return $"flags : {(ConditionFlags)flags}\n" +
                $"Color : {color} {colorCount}\n" +
                $"special : {special}\n" +
                $"unblock : {unblock}\n" +
                $"shape : {(shape != null ? String.Join(',', shape) : "null")}";
        }
    }
}