using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Match3Core;
using Match3Core.Levels;

namespace Match3Configs.Levels
{
    public static class XmlBoardsReader
    {
        public static HashSet<Level> GetBoards()
        {
            var levels = new HashSet<Level>();

            var rootElement = GetRoot();

            foreach (var levelElement in rootElement.Elements())
            {
                var level = new Level();

                var id = GetID(levelElement);

                foreach (var l in levels)
                {
                    if (l.ID == id) throw new Exception($"Duplication level id:{level.ID} in {XmlFields.PATH_TO_DOCUMENT}");
                }

                level.ID = id;

                level.condition = GetWinConditions(levelElement);

                GetFieldSize(out var rows, out var collumns, levelElement);
                level.rows = rows;
                level.collumns = collumns;
                level.Slots = new Slot[rows, collumns];
                level.Boxes = new Dictionary<Coordinate, string>();

                var slotsElements = GetSlotsFromLevel(levelElement);

                foreach (var slot in slotsElements)
                {
                    GetSlotCanAttribute(out var canHoldCell, slot, XmlFields.SLOT_HOLD_CELL_ATTRIBUTE);
                    GetSlotCanAttribute(out var canPassCell, slot, XmlFields.SLOT_PASS_CELL_ATTRIBUTE);
                    GetSlotCanAttribute(out var isBlocked, slot, XmlFields.SLOT_BLOCKED_ATTRIBUTE);
                    GetSlotCanAttribute(out var isActive, slot, XmlFields.SLOT_ACTIVE_ATTRIBUTE);
                    GetSlotCoordinate(out var coordinate, slot);
                    GetSlotCell(out var cell, slot);

                    //Debug.Log($"coordinate = {coordinate} HC = {canHoldCell} PC = {canPassCell}");
                    if (level.GetSlot(coordinate) != null) throw new Exception($"Duplication slot {coordinate} in {XmlFields.PATH_TO_DOCUMENT}");
                    if (isActive)
                    {
                        level.SetSlot(coordinate, new Slot(coordinate, cell, canHoldCell, canPassCell, isBlocked, isActive));
                    }
                    else
                    {
                        level.SetSlot(coordinate, new Slot(coordinate, canPassCell, isActive));
                        var boxID = TryGetBoxIdFromSlot(slot);
                        if (boxID != null) level.AddBox(coordinate, boxID);
                    }
                }
                levels.Add(level);
            }
            return levels;
        }

        public static XDocument GetDocument()
        {
            var xDoc = XDocument.Load(XmlFields.PATH_TO_DOCUMENT);
            return xDoc;
        }

        public static XElement GetRoot()
        {
            var doc = GetDocument();
            var rootElement = doc.Element(XmlFields.ROOT_ELEMENT);
            if (rootElement == null) Debug.LogWarning($"Can't find root element in {XmlFields.PATH_TO_DOCUMENT}");
            return rootElement;
        }

        public static XElement GetLevelByID(int levelID, XElement root = null)
        {
            var rootElement = root is null ? GetRoot() : root;
            if (rootElement is null) throw new Exception($"Missing root element {XmlFields.ROOT_ELEMENT}");
            var SomeRequiredLevel = rootElement.Elements()
                .Where(e => e.Attribute("id").Value == levelID.ToString());

            if (SomeRequiredLevel.Count() > 1)
            {
                throw new System.Exception($"Duplication level id:{levelID} in {XmlFields.PATH_TO_DOCUMENT}");
            }

            return SomeRequiredLevel.First();
        }

        public static int GetID(XElement levelElement)
        {
            var idText = levelElement.Attribute(XmlFields.LEVEL_ID_ATTRIBUTE).Value;
            var id = Int32.Parse(idText);
            return id;
        }

        public static Condition GetWinConditions(XElement levelElement)
        {
            XElement conditions = levelElement.Element(XmlFields.WIN_ELEMENT);
            byte flags = 0;
            if (TryGetColorCountCondition(conditions, out byte color, out byte count).Length > 0)
            {
                flags += (byte)ConditionFlags.ColorCounter;
            }

            return new Condition(flags, (CellsColor)color, count);
        }

        public static XElement GetSlotElement(XElement levelElement)
        {
            return levelElement.Element(XmlFields.SLOTS_ELEMENT);
        }

        public static void GetFieldSize(out int rows, out int collumns, XElement levelElement)
        {
            var slot = GetSlotElement(levelElement);
            var sizeText = slot.Attribute(XmlFields.SLOTS_SIZE_ATTRIBUTE).Value.Split(XmlFields.SPLITER);

            rows = Int32.Parse(sizeText[0]);
            collumns = Int32.Parse(sizeText[1]);
        }

        public static IEnumerable<XElement> GetSlots(XElement slotElement)
        {
            return slotElement.Elements();
        }

        public static IEnumerable<XElement> GetSlotsFromLevel(XElement levelElement)
        {
            var slot = GetSlotElement(levelElement);
            return slot.Elements();
        }

        public static XElement GetSlotFromLevelIDByCoordinate(int levelID, int posX, int posY)
        {
            var level = GetLevelByID(levelID);
            var slots = GetSlotElement(level);

            if (slots is null) return null;
            var someRequiredSlot = slots.Elements()
                    .Where(e => e.Attribute(XmlFields.SLOT_COORDINATE_ATTRIBUTE).Value == $"{posX}/{posY}");

            if (someRequiredSlot.Count() > 1)
            {
                throw new System.Exception($"Duplication slot {posX}-{posY} in levels.xml");
            }

            var requiredSlot = someRequiredSlot.First();

            return requiredSlot;

        }

        public static void GetSlotCoordinate(out Coordinate coordinate, XElement slotElement)
        {
            var positionText = slotElement.Attribute(XmlFields.SLOT_COORDINATE_ATTRIBUTE).Value.Split(XmlFields.SPLITER);
            var posX = Int32.Parse(positionText[0]);
            var posY = Int32.Parse(positionText[1]);
            coordinate = new Coordinate(posX, posY);
        }

        public static void GetSlotCanAttribute(out bool can, XElement slotElement, string attributeName)
        {
            if (slotElement.Attribute(attributeName) != null)
            {
                var canHoldCellText = slotElement.Attribute(attributeName).Value;
                can = bool.Parse(canHoldCellText);
            }
            else
            {
                can = true;
            }
        }

        public static Cell GetSlotCell(out Cell cell, XElement slotElement)
        {
            cell = null;
            var colorAttribute = slotElement.Attribute(XmlFields.SLOT_CELL_ATTRIBUTE);
            if (colorAttribute is null)
            {
                return cell;
            }
            var color = colorAttribute.Value;

            switch (color)
            {
                case "special":
                    cell = new Cell(CellsColor.Special, TryGetCellIdFromCell(slotElement));
                    break;
                case "red":
                    cell = new Cell(CellsColor.Red, TryGetCellIdFromCell(slotElement));
                    break;
                case "green":
                    cell = new Cell(CellsColor.Green, TryGetCellIdFromCell(slotElement));
                    break;
                case "blue":
                    cell = new Cell(CellsColor.Blue, TryGetCellIdFromCell(slotElement));
                    break;
                case "yellow":
                    cell = new Cell(CellsColor.Yellow, TryGetCellIdFromCell(slotElement));
                    break;
                default:
                    Debug.LogWarning($"Unknown color {color} in level.xml");
                    break;
            }

            return cell;
        }

        public static string TryGetColorCountCondition(XElement conditionElement, out byte color, out byte count)
        {
            XAttribute colorCount = conditionElement.Attribute(XmlFields.CELL_COUNT_CONDITION);
            color = 0;
            count = 0;
            if (colorCount != null)
            {
                string colorCountText = colorCount.Value;
                string[] colorCountSplit = colorCountText.Split(XmlFields.SPLITER);
                color = Byte.Parse(colorCountSplit[0]);
                count = Byte.Parse(colorCountSplit[1]);
                return colorCountText;
            }
            return "";
        }

        public static string TryGetCellIdFromCell(XElement slotElement)
        {
            var celIdAttribute = slotElement.Attribute(XmlFields.SLOT_CELL_ID);
            if (celIdAttribute != null)
            {
                var cellID = celIdAttribute.Value;
                return cellID;
            }
            return "";
        }

        public static string TryGetCellIdFromCell(Coordinate coordinate, int levelId)
        {
            var slotElement = GetSlotFromLevelIDByCoordinate(levelId, coordinate.x, coordinate.y);
            return TryGetCellIdFromCell(slotElement);
        }

        public static string TryGetBoxIdFromSlot(XElement slotElement)
        {
            var boxAttribute = slotElement.Attribute(XmlFields.SLOT_BOX_ATTRIBUTE);
            if (boxAttribute != null)
            {
                var boxID = boxAttribute.Value;
                return boxID;
            }
            return null;
        }

        public static string TryGetBoxIdFromSlot(Coordinate coordinate, int levelId)
        {
            var slotElement = GetSlotFromLevelIDByCoordinate(levelId, coordinate.x, coordinate.y);
            return TryGetBoxIdFromSlot(slotElement);
        }
    }
}