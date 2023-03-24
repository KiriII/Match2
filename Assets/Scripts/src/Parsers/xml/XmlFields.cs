using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3Configs.Levels
{
    static class XmlFields
    {
        public const string PATH_TO_DOCUMENT = "Assets/Configs/levels.xml";
        public const string ROOT_ELEMENT = "levels";
        public const string LEVEL_ELEMENT = "level";
        public const string LEVEL_ID_ATTRIBUTE = "id";
        public const string SLOTS_ELEMENT = "slots";
        public const string SLOTS_SIZE_ATTRIBUTE = "size";
        public const string SPLITER = "/";
        public const string SLOT_ELEMENT = "slot";
        public const string SLOT_COORDINATE_ATTRIBUTE = "coordinate";
        public const string SLOT_HOLD_CELL_ATTRIBUTE = "canHoldCell";
        public const string SLOT_PASS_CELL_ATTRIBUTE = "canPassCells";
        public const string SLOT_BLOCKED_ATTRIBUTE = "isBlocked";
    }
}