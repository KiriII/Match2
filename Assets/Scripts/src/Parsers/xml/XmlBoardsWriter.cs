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
                    new XAttribute("size", "3/3"),
                    new XElement("slot", new XAttribute("coordinate", "0/0")),
                    new XElement("slot", new XAttribute("coordinate", "0/1")),
                    new XElement("slot", new XAttribute("coordinate", "0/2")),

                    new XElement("slot", new XAttribute("coordinate", "1/0")),
                    new XElement("slot", new XAttribute("coordinate", "1/1")),
                    new XElement("slot", new XAttribute("coordinate", "1/2")),

                    new XElement("slot", new XAttribute("coordinate", "2/0")),
                    new XElement("slot", new XAttribute("coordinate", "2/1")),
                    new XElement("slot", new XAttribute("coordinate", "2/2"))
                )));

            xdoc.Save("Assets/Configs/levels.xml");
        }

        public static void ToggleHoldCell(int levelID, int posX, int posY)
        {
            XDocument xdoc = XDocument.Load("Assets/Configs/levels.xml");

            var slot = GetSlot(xdoc, levelID, posX, posY);

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

            var slot = GetSlot(xdoc, levelID, posX, posY);

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

        private static XElement GetSlot(XDocument xDoc, int levelID, int posX, int posY)
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
    }
}