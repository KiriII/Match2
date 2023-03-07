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
        public static List<Level> GetBoards()
        {
            var levels = new List<Level>();

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

                GetFieldSize(out var rows, out var collumns, levelElement);
                level.rows = rows;
                level.collumns = collumns;
                level.slots = new Slot[rows, collumns];

                var slotsElements = GetSlotsFromLevel(levelElement);

                foreach (var slot in slotsElements)
                {
                    GetSlotCanAttribute(out var canHoldCell, slot, XmlFields.SLOT_HOLD_CELL_ATTRIBUTE);
                    GetSlotCanAttribute(out var canPassCell, slot, XmlFields.SLOT_PASS_CELL_ATTRIBUTE);
                    GetSlotCoordinate(out var coordinate, slot);

                    //Debug.Log($"coordinate = {coordinate} HC = {canHoldCell} PC = {canPassCell}");
                    if (level.GetSlot(coordinate) != null) throw new Exception($"Duplication slot {coordinate} in {XmlFields.PATH_TO_DOCUMENT}");
                    level.SetSlot(coordinate, new Slot(coordinate, canHoldCell, canPassCell));
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
    }
}