using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml;
using Match3Core;
using Match3Core.Levels;

namespace Match3Configs.Reader
{
    public static class XmlBoardsReader
    {
         public static List<Level> GetBoards()
         {
            var levels = new List<Level>();

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("Assets/Configs/levels.xml");
            XmlElement? xRoot = xDoc.DocumentElement;
            if (xRoot != null)
            {
                //Debug.Log($"finded {xRoot.Name}");
                foreach (XmlElement levelElement in xRoot)
                {
                    var level = new Level();

                    var idText = levelElement.GetAttribute("id");
                    //Debug.Log($"level {idText}");

                    var id = Int32.Parse(idText);

                    foreach (Level l in levels)
                    {
                        if (l.ID == id) throw new Exception($"Duplication level id in levels.xml");
                    }

                    level.ID = id;

                    foreach (XmlElement levelParam in levelElement)
                    {
                        if (levelParam.Name == "slots")
                        {
                            var sizeText = levelParam.Attributes.GetNamedItem("size").Value.Split('/');

                            var rows = Int32.Parse(sizeText[0]);
                            var collumns = Int32.Parse(sizeText[1]);

                            level.rows = rows;
                            level.collumns = collumns;

                            //Debug.Log($"rows {rows} collumns {collumns}");

                            level.slots = new Slot[rows, collumns];
                        }
                        foreach (XmlElement slot in levelParam)
                        {
                            var positionText = slot.GetAttribute("coordinate").Split('/');

                            var canHoldCell = true;
                            var canPassCell = true;

                            if (slot.HasAttribute("canHoldCell"))
                            {
                                var canHoldCellText = slot.GetAttribute("canHoldCell");
                                canHoldCell = bool.Parse(canHoldCellText);
                            }
                            if (slot.HasAttribute("canPassCells"))
                            {
                                var canPassCellText = slot.GetAttribute("canPassCells");
                                canPassCell = bool.Parse(canPassCellText);
                            }

                            var posX = Int32.Parse(positionText[0]);
                            var posY = Int32.Parse(positionText[1]);

                            //Debug.Log($"X = {posX} y = {posY} HC = {canHoldCell} PC = {canPassCell}");
                            if (level.slots[posX, posY] != null) throw new Exception($"Duplication slot in levels.xml");
                            level.slots[posX, posY] = new Slot(canHoldCell, canPassCell);
                        }
                    }
                    levels.Add(level);
                }
            }

            return levels;
         }
    }
}