using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using UnityEngine;
using Match3Core;


namespace Match3Configs.Writer
{
    public static class XmlBoardsWriter
    {
        public static void AddNewLevel(int levelID)
        {
            XDocument xdoc = XDocument.Load("Assets/Configs/levels.xml");
            XElement? levels = xdoc.Element("levels");

            levels.Add(new XElement("level",
                new XAttribute("id", levelID),
                new XElement("slots",
                    new XAttribute("size", "5/5"))));

            var slots = GetSlots(xdoc, levelID);

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    slots.Add(new XElement("slot",
                        new XAttribute("coordinate", $"{i}/{j}")
                        ));
                }
            }

            xdoc.Save("Assets/Configs/levels.xml");
        }

        public static void ToggleHoldCell(int levelID, int posX, int posY)
        {
            XDocument xdoc = XDocument.Load("Assets/Configs/levels.xml");

            var slot = GetRequiredSlot(xdoc, levelID, posX, posY);

            if (slot == null) return;

            if (slot.Attribute("canHoldCell") != null)
            {
                slot.SetAttributeValue("canHoldCell", null);
                slot.SetAttributeValue("canPassCells", null);
            }
            else
            {
                slot.SetAttributeValue("canHoldCell", "false");
            }

            xdoc.Save("Assets/Configs/levels.xml");
        }

        public static void TogglePassCell(int levelID, int posX, int posY)
        {
            XDocument xdoc = XDocument.Load("Assets/Configs/levels.xml");

            var slot = GetRequiredSlot(xdoc, levelID, posX, posY);

            if (slot == null) return;

            if (slot.Attribute("canPassCells") != null)
            {
                slot.SetAttributeValue("canPassCells", null);
            }
            else
            {
                slot.SetAttributeValue("canPassCells", "false");
            }

            xdoc.Save("Assets/Configs/levels.xml");
        }

        private static XElement GetRequiredSlot(XDocument xDoc, int levelID, int posX, int posY)
        {

            var slots = GetSlots(xDoc, levelID);

            if (slots is not null)
            {
                var someRequiredSlot = slots.Elements()
                    .Where(e => e.Attribute("coordinate").Value == $"{posX}/{posY}");

                if (someRequiredSlot.Count() > 1)
                {
                    throw new System.Exception($"Duplication slot {posX}-{posY} in levels.xml");
                }

                var requiredSlot = someRequiredSlot.First();

                return requiredSlot;
            }

            return null;
        }

        private static XElement GetSlots(XDocument xDoc, int levelID)
        {
            XElement? levels = xDoc.Element("levels");

            if (levels is not null)
            {
                var SomeRequiredLevel = levels.Elements()
                    .Where(e => e.Attribute("id").Value == levelID.ToString());

                if (SomeRequiredLevel.Count() > 1)
                {
                    throw new System.Exception($"Duplication level id:{levelID} in levels.xml");
                }

                var requiredLevel = SomeRequiredLevel.First();

                var slots = requiredLevel.Element("slots");

                return slots;
            }

            return null;
        }
    }
}