using UnityEngine;
using System;
using System.Collections.Generic;

namespace Match3Core
{
    public class Condition
    {
        public byte flags { get; set; }
        public CellsColor color { get; set; }
        public int colorCount { get; set; }
        public bool special { get; set; }
        public bool unblock { get; set; }
        public HashSet<Coordinate> shape { get; set; }

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