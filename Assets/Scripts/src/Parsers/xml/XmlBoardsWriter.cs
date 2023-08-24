using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using UnityEngine;
using Match3Core;

namespace Match3Configs.Levels
{
    public static class XmlBoardsWriter
    {
        private const string DEFAULT_BOARD_SIZE = "9/9";

        public static void AddNewLevel(int levelID)
        {
            var levels = XmlBoardsReader.GetRoot();

            levels.Add(new XElement(XmlFields.LEVEL_ELEMENT,
                new XAttribute(XmlFields.LEVEL_ID_ATTRIBUTE, levelID),
                new XElement(XmlFields.SLOTS_ELEMENT,
                    new XAttribute(XmlFields.SLOTS_SIZE_ATTRIBUTE, DEFAULT_BOARD_SIZE))));

            var requiredLevel = XmlBoardsReader.GetLevelByID(levelID, levels);

            var slots = XmlBoardsReader.GetSlotElement(requiredLevel);

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    slots.Add(new XElement(XmlFields.SLOT_ELEMENT,
                        new XAttribute(XmlFields.SLOT_COORDINATE_ATTRIBUTE, $"{i}/{j}"),
                        new XAttribute(XmlFields.SLOT_BLOCKED_ATTRIBUTE, $"false")));
                }
            }

            levels.Document.Save(XmlFields.PATH_TO_DOCUMENT);
        }

        public static void ToggleActive(int levelID, int posX, int posY)
        {
            ToggleCanSlot(levelID, posX, posY, XmlFields.SLOT_ACTIVE_ATTRIBUTE);
        }

        public static void ToggleHoldCell(int levelID, int posX, int posY)
        {
            ToggleCanSlot(levelID, posX, posY, XmlFields.SLOT_HOLD_CELL_ATTRIBUTE);
        }

        public static void TogglePassCell(int levelID, int posX, int posY)
        {
            ToggleCanSlot(levelID, posX, posY, XmlFields.SLOT_PASS_CELL_ATTRIBUTE);
        }

        public static void ToggleBlocked(int levelID, int posX, int posY)
        {
            ToggleCanSlot(levelID, posX, posY, XmlFields.SLOT_BLOCKED_ATTRIBUTE);
        }

        private static void ToggleCanSlot(int levelID, int posX, int posY, string attributeName)
        {
            var slot = XmlBoardsReader.GetSlotFromLevelIDByCoordinate(levelID, posX, posY);

            if (slot == null) return;

            if (slot.Attribute(attributeName) != null)
            {
                slot.SetAttributeValue(attributeName, null);
            }
            else
            {
                slot.SetAttributeValue(attributeName, "false");
            }

            slot.Document.Save(XmlFields.PATH_TO_DOCUMENT);
        }
    }
}