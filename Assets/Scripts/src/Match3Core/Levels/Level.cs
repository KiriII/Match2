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
    }
}