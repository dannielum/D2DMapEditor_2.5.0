using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace D2DMapEditor
{
    class Clipboard
    {
        public int Width;
        public int Height;
        public ArrayList Data;

        public Clipboard()
        {
            ClearClipboard();
        }

        public void ClearClipboard()
        {
            Width = 0;
            Height = 0;
            Data = new ArrayList();
        }

        public Boolean CopyToClipboard(Map map, MapInfo mapInfo, History history, int layerIndex, bool isCut)
        {   // return true if 
            if (mapInfo.Selection.StartDrag.X != -1 && mapInfo.Selection.StartDrag.Y != -1 && mapInfo.Selection.StopDrag.X != -1 && mapInfo.Selection.StopDrag.Y != -1)
            {
                ClearClipboard();

                if (mapInfo.Selection.BottomRightX > map.Layers[layerIndex].Width)
                    mapInfo.Selection.BottomRightX = map.Layers[layerIndex].Width;
                if (mapInfo.Selection.BottomRightY > map.Layers[layerIndex].Height)
                    mapInfo.Selection.BottomRightY = map.Layers[layerIndex].Height;

                int newX = 0;
                int newY = 0;

                int id = -1;
                if (isCut)
                {
                    history.ClearRedo();
                    id = history.UndoNextId;
                }

                for (int i = mapInfo.Selection.TopLeftX; i < mapInfo.Selection.BottomRightX; i++)
                {   // copy selected tiles
                    for (int j = mapInfo.Selection.TopLeftY; j < mapInfo.Selection.BottomRightY; j++)
                    {
                        Data.Add(new ClipboardNode(map.Layers[layerIndex].LayerId, newX, newY, map.Layers[layerIndex].LayerData[i, j]));
                        
                        if (isCut)
                        {   // cut tile
                            history.PushUndo(id, map.Layers[layerIndex].LayerId, i, j, map.Layers[layerIndex].LayerData[i, j], ActionType.Cut);
                            map.Layers[layerIndex].LayerData[i, j] = -1;
                        }

                        newY++;
                    }
                    newX++;
                }

                Width = mapInfo.Selection.BottomRightX - mapInfo.Selection.TopLeftX;
                Height = mapInfo.Selection.BottomRightY - mapInfo.Selection.TopLeftY;

                return true;
            }

            return false;
        }

        public Boolean PasteClipboard(Map map, MapInfo mapInfo, History history, int layerIndex)
        {
            if (mapInfo.Selection.StartDrag.X != -1 && mapInfo.Selection.StartDrag.Y != -1 && 
                mapInfo.Selection.StopDrag.X != -1 && mapInfo.Selection.StopDrag.Y != -1)
            {
                if (Data.Count < 1)
                    return false;

                if (mapInfo.Selection.BottomRightX > map.Layers[layerIndex].Width)
                    mapInfo.Selection.BottomRightX = map.Layers[layerIndex].Width;
                if (mapInfo.Selection.BottomRightY > map.Layers[layerIndex].Height)
                    mapInfo.Selection.BottomRightY = map.Layers[layerIndex].Height;

                history.Redo.Clear();
                int id = history.UndoNextId;

                int pasteEndX = mapInfo.Selection.TopLeftX + Width;
                int pasteEndY = mapInfo.Selection.TopLeftY + Height;
                //if (mapInfo.Selection.BottomRightX > pasteEndX)
                //    pasteEndX = mapInfo.Selection.BottomRightX - ((mapInfo.Selection.BottomRightX - mapInfo.Selection.TopLeftX) % Width);
                //if (mapInfo.Selection.BottomRightY > pasteEndY)
                //    pasteEndY = mapInfo.Selection.BottomRightY - ((mapInfo.Selection.BottomRightY - mapInfo.Selection.TopLeftY) % Height);

                if (pasteEndX > map.Layers[layerIndex].Width)
                    return false;
                if (pasteEndY > map.Layers[layerIndex].Height)
                    return false;

                int pos = 0;
                for (int i = mapInfo.Selection.TopLeftX; i < pasteEndX; i++)
                {   // paste copied tiles
                    for (int j = mapInfo.Selection.TopLeftY; j < pasteEndY; j++)
                    {
                        if (map.Layers[layerIndex].LayerData[i, j] != ((ClipboardNode)Data[pos]).Value)
                        {
                            history.PushUndo(id, map.Layers[layerIndex].LayerId, i, j, 
                                             map.Layers[layerIndex].LayerData[i, j], ActionType.Paste);
                            map.Layers[layerIndex].LayerData[i, j] = ((ClipboardNode)Data[pos]).Value;
                        }
                        pos++;
                        pos = pos % Data.Count;
                        //pos = pos % ((pasteEndX - i) * Height);
                    }
                }

                return true;
            }
            else
            {
                MessageBox.Show("No selection", "Paste", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            return false;
        }
    }
}
