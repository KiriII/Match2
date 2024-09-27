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

        public static void AddNewLevel(string levelID)
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

        public static void SetCellColor(string levelID, int posX, int posY, CellsColor color)
        {
            var slot = XmlBoardsReader.GetSlotFromLevelIDByCoordinate(levelID, posX, posY);

            if (slot == null) return;

            switch (color)
            {
                case CellsColor.Special:
                    slot.SetAttributeValue(XmlFields.SLOT_CELL_ATTRIBUTE, "special");
                    break;
                case CellsColor.Red:
                    slot.SetAttributeValue(XmlFields.SLOT_CELL_ATTRIBUTE, "red");
                    break;
                case CellsColor.Green:
                    slot.SetAttributeValue(XmlFields.SLOT_CELL_ATTRIBUTE, "green");
                    break;
                case CellsColor.Blue:
                    slot.SetAttributeValue(XmlFields.SLOT_CELL_ATTRIBUTE, "blue");
                    break;
                case CellsColor.Yellow:
                    slot.SetAttributeValue(XmlFields.SLOT_CELL_ATTRIBUTE, "yellow");
                    break;
                case CellsColor.Empty:
                    slot.SetAttributeValue(XmlFields.SLOT_CELL_ATTRIBUTE, null);
                    break;
                default:
                    Debug.Log("NON");
                    break;
            }

            slot.Document.Save(XmlFields.PATH_TO_DOCUMENT);
        }

        public static void SetCellId(string levelID, int posX, int posY, string id)
        {
            var slot = XmlBoardsReader.GetSlotFromLevelIDByCoordinate(levelID, posX, posY);

            if (slot == null) return;

            slot.SetAttributeValue(XmlFields.SLOT_CELL_ID, id);

            slot.Document.Save(XmlFields.PATH_TO_DOCUMENT);
        }

        public static void ToggleActive(string levelID, int posX, int posY)
        {
            ToggleCanSlot(levelID, posX, posY, XmlFields.SLOT_ACTIVE_ATTRIBUTE);
        }

        public static void ToggleHoldCell(string levelID, int posX, int posY)
        {
            ToggleCanSlot(levelID, posX, posY, XmlFields.SLOT_HOLD_CELL_ATTRIBUTE);
        }

        public static void TogglePassCell(string levelID, int posX, int posY)
        {
            ToggleCanSlot(levelID, posX, posY, XmlFields.SLOT_PASS_CELL_ATTRIBUTE);
        }

        public static void ToggleBlocked(string levelID, int posX, int posY)
        {
            ToggleCanSlot(levelID, posX, posY, XmlFields.SLOT_BLOCKED_ATTRIBUTE);
        }

        public static void ToggleBox(string levelID, int posX, int posY, string boxId = "0")
        {
            var slot = XmlBoardsReader.GetSlotFromLevelIDByCoordinate(levelID, posX, posY);
            var boxAttribute = slot.Attribute(XmlFields.SLOT_BOX_ATTRIBUTE);
            if (boxAttribute != null)
            {
                boxAttribute.Remove();
            }
            else
            {
                slot.SetAttributeValue(XmlFields.SLOT_BOX_ATTRIBUTE, boxId);
            }

            slot.Document.Save(XmlFields.PATH_TO_DOCUMENT);
        }

        public static void SetBoxId(string levelID, int posX, int posY, string boxId)
        {
            var slot = XmlBoardsReader.GetSlotFromLevelIDByCoordinate(levelID, posX, posY);
            slot.SetAttributeValue(XmlFields.SLOT_BOX_ATTRIBUTE, boxId);
            slot.Document.Save(XmlFields.PATH_TO_DOCUMENT);
        }

        public static void SetWinConditionColor(string levelId, CellsColor cellsColor)
        {
            XElement conditions = GetWinConditionElement(levelId);

            XmlBoardsReader.TryGetColorCountCondition(conditions, out byte color, out byte count);

            conditions.SetAttributeValue(XmlFields.CELL_COUNT_CONDITION, $"{(byte)cellsColor}/{count}");

            conditions.Document.Save(XmlFields.PATH_TO_DOCUMENT);
        }

        public static void SetWinConditionColorCount(string levelId, string condCount)
        {
            XElement conditions = GetWinConditionElement(levelId);

            XmlBoardsReader.TryGetColorCountCondition(conditions, out byte color, out byte count);

            conditions.SetAttributeValue(XmlFields.CELL_COUNT_CONDITION, $"{color}/{condCount}");

            conditions.Document.Save(XmlFields.PATH_TO_DOCUMENT);
        }

        public static void ToggleShapeByPosition(string levelId, int posX, int posY)
        {
            ToggleShapeByCoordinate(levelId, new Coordinate(posX, posY));
        }

        public static void ToggleShapeByCoordinate(string levelId, Coordinate coordinate)
        {
            XElement conditions = GetWinConditionElement(levelId);

            XmlBoardsReader.TryGetShapeCondition(conditions, out HashSet<Coordinate> shape);

            if (shape != null)
            {
                if (shape.Contains(coordinate))
                {
                    shape.Remove(coordinate);
                }
                else
                {
                    shape.Add(coordinate);
                }
            }

            if (shape.Count == 0)
            {
                ToggleWinConditionShape(levelId);
                return;
            }
            else if (conditions != null)
            {
                conditions.SetAttributeValue(XmlFields.SHAPE_CONDITION, string.Join(", ", shape));
            }

            conditions.Document.Save(XmlFields.PATH_TO_DOCUMENT);
        }

        public static void ToggleWinConditionColorCount(string levelId)
        {
            XElement conditions = GetWinConditionElement(levelId);

            if (XmlBoardsReader.TryGetColorCountCondition(conditions, out byte color, out byte count) != "")
            {
                conditions.SetAttributeValue(XmlFields.CELL_COUNT_CONDITION, null);
            }
            else
            {
                conditions.SetAttributeValue(XmlFields.CELL_COUNT_CONDITION, "2/5");
            }

            conditions.Document.Save(XmlFields.PATH_TO_DOCUMENT);
        }

        public static void ToggleWinConditionSpecial(string levelId)
        {
            XElement conditions = GetWinConditionElement(levelId);
            XmlBoardsReader.TryGetSpecialCondition(conditions, out bool special);
            if (special)
            {
                conditions.SetAttributeValue(XmlFields.SPECIAL_CONDITION, "false");
            }
            else
            {
                conditions.SetAttributeValue(XmlFields.SPECIAL_CONDITION, "true");
            }

            conditions.Document.Save(XmlFields.PATH_TO_DOCUMENT);
        }

        public static void ToggleWinConditionUnblock(string levelId)
        {
            XElement conditions = GetWinConditionElement(levelId);
            XmlBoardsReader.TryGetUnblockCondition(conditions, out bool unblock);
            if (unblock)
            {
                conditions.SetAttributeValue(XmlFields.UNBLOCK_CONDITION, "false");
            }
            else
            {
                conditions.SetAttributeValue(XmlFields.UNBLOCK_CONDITION, "true");
            }

            conditions.Document.Save(XmlFields.PATH_TO_DOCUMENT);
        }

        public static void ToggleWinConditionShape(string levelId)
        {
            XElement conditions = GetWinConditionElement(levelId);
            XmlBoardsReader.TryGetShapeCondition(conditions, out HashSet<Coordinate> shape);
            if (shape.Count > 0)
            {
                conditions.SetAttributeValue(XmlFields.SHAPE_CONDITION, null);
            }
            else
            {
                conditions.SetAttributeValue(XmlFields.SHAPE_CONDITION, "4/4");
            }

            conditions.Document.Save(XmlFields.PATH_TO_DOCUMENT);
        }

        private static XElement GetWinConditionElement(string levelId)
        {
            XElement level = XmlBoardsReader.GetLevelByID(levelId, XmlBoardsReader.GetRoot());
            XElement conditions = level.Element(XmlFields.WIN_ELEMENT);

            if (conditions == null) return null;
            return conditions;
        }

        private static void ToggleCanSlot(string levelID, int posX, int posY, string attributeName)
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